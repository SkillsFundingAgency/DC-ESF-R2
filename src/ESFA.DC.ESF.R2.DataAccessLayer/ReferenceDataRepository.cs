using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ReferenceData.LARS.Model.Interface;
using ESFA.DC.ReferenceData.Organisations.Model.Interface;
using ESFA.DC.ReferenceData.Postcodes.Model.Interface;
using ESFA.DC.ReferenceData.ULN.Model.Interface;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class ReferenceDataRepository : IReferenceDataRepository
    {
        private readonly Func<IPostcodesContext> _postcodes;
        private readonly Func<ILARSContext> _larsContext;
        private readonly Func<IOrganisationsContext> _organisations;
        private readonly Func<IUlnContext> _ulnContext;

        private readonly object _ulnLock = new object();
        private readonly object _larsDeliveryLock = new object();

        public ReferenceDataRepository(
            Func<IPostcodesContext> postcodes,
            Func<ILARSContext> lars,
            Func<IOrganisationsContext> organisations,
            Func<IUlnContext> ulnContext)
        {
            _postcodes = postcodes;
            _larsContext = lars;
            _organisations = organisations;
            _ulnContext = ulnContext;
        }

        public string GetPostcodeVersion(CancellationToken cancellationToken)
        {
            string version;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _postcodes())
            {
                version = context.VersionInfos
                    .OrderByDescending(v => v.VersionNumber)
                    .Select(v => v.VersionNumber)
                    .FirstOrDefault();
            }

            return version;
        }

        public string GetLarsVersion(CancellationToken cancellationToken)
        {
            string version;

            cancellationToken.ThrowIfCancellationRequested();

            lock (_larsDeliveryLock)
            {
                using (var context = _larsContext())
                {
                    version = context.LARS_Versions
                        .OrderByDescending(v => v.MainDataSchemaName)
                        .Select(lv => lv.MainDataSchemaName)
                        .FirstOrDefault();
                }
            }

            return version;
        }

        public IDictionary<string, LarsLearningDeliveryModel> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken)
        {
            var learningDeliveries = new Dictionary<string, LarsLearningDeliveryModel>();

            cancellationToken.ThrowIfCancellationRequested();

            lock (_larsDeliveryLock)
            {
                using (var context = _larsContext())
                {
                    var deliveries = context.LARS_LearningDeliveries
                        .Where(x => learnAimRefs.Contains(x.LearnAimRef))
                        .Select(x => new LarsLearningDeliveryModel
                        {
                            LearnAimRef = x.LearnAimRef,
                            LearningDeliveryGenre = x.LearningDeliveryGenre,
                            LearnAimRefTitle = x.LearnAimRefTitle,
                            NotionalNVQLevelv2 = x.NotionalNvqlevelv2,
                            SectorSubjectAreaTier2 = x.SectorSubjectAreaTier2,
                            ValidityPeriods = x.LarsValidities.Select(lv => new LarsValidityPeriod
                            {
                                ValidityStartDate = lv.StartDate,
                                ValidityEndDate = lv.EndDate
                            })
                        });

                    foreach (var delivery in deliveries)
                    {
                        learningDeliveries.Add(delivery.LearnAimRef, delivery);
                    }
                }
            }

            return learningDeliveries;
        }

        public string GetOrganisationVersion(CancellationToken cancellationToken)
        {
            string version;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _organisations())
            {
                version = context.OrgVersions
                    .OrderByDescending(v => v.MainDataSchemaName)
                    .Select(lv => lv.MainDataSchemaName)
                    .FirstOrDefault();
            }

            return version;
        }

        public string GetProviderName(int ukPrn, CancellationToken cancellationToken)
        {
            string providerName;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _organisations())
            {
                providerName = context.OrgDetails
                    .FirstOrDefault(o => o.Ukprn == ukPrn)
                    ?.Name;
            }

            return providerName;
        }

        public IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken)
        {
            var ulns = new HashSet<long>();

            cancellationToken.ThrowIfCancellationRequested();

            lock (_ulnLock)
            {
                var result = new List<long>();
                var ulnShards = SplitList(searchUlns, 5000);
                using (var context = _ulnContext())
                {
                    foreach (var shard in ulnShards)
                    {
                        result.AddRange(context.UniqueLearnerNumbers
                            .Where(u => shard.Contains(u.Uln))
                            .Select(u => u.Uln));
                    }

                    ulns.UnionWith(result);
                }
            }

            return ulns;
        }

        private IEnumerable<List<long?>> SplitList(IEnumerable<long?> ulns, int nSize = 30)
        {
            var ulnList = ulns.ToList();

            for (var i = 0; i < ulnList.Count; i += nSize)
            {
                yield return ulnList.GetRange(i, Math.Min(nSize, ulnList.Count - i));
            }
        }
    }
}
