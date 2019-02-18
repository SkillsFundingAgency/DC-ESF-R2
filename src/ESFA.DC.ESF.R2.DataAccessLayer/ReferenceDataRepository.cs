using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using ESFA.DC.ReferenceData.LARS.Model.Interface;
using ESFA.DC.ReferenceData.Organisations.Model.Interface;
using ESFA.DC.ReferenceData.ULN.Model;
using ESFA.DC.ReferenceData.ULN.Model.Interface;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class ReferenceDataRepository : IReferenceDataRepository
    {
        private readonly Func<IPostcodes> _postcodes;
        private readonly Func<ILARSContext> _larsContext;
        private readonly Func<IOrganisationsContext> _organisations;
        private readonly Func<IUlnContext> _ulnContext;
        private readonly ILogger _logger;

        private readonly object _ulnLock = new object();
        private readonly object _larsDeliveryLock = new object();

        public ReferenceDataRepository(
            ILogger logger,
            Func<IPostcodes> postcodes,
            Func<ILARSContext> lars,
            Func<IOrganisationsContext> organisations,
            Func<IUlnContext> ulnContext)
        {
            _logger = logger;
            _postcodes = postcodes;
            _larsContext = lars;
            _organisations = organisations;
            _ulnContext = ulnContext;
        }

        public string GetPostcodeVersion(CancellationToken cancellationToken)
        {
            var version = string.Empty;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _postcodes())
                {
                    version = context.VersionInfos
                        .OrderByDescending(v => v.VersionNumber)
                        .Select(v => v.VersionNumber)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get postcode version", ex);
            }

            return version;
        }

        public string GetLarsVersion(CancellationToken cancellationToken)
        {
            var version = string.Empty;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _larsContext())
                {
                    version = context.LARS_Versions
                        .OrderByDescending(v => v.MainDataSchemaName)
                        .Select(lv => lv.MainDataSchemaName)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get lars version", ex);
            }

            return version;
        }

        public IList<LarsLearningDelivery> GetLarsLearningDelivery(IList<string> learnAimRefs, CancellationToken cancellationToken)
        {
            List<LarsLearningDelivery> learningDelivery = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                lock (_larsDeliveryLock)
                {
                    using (var context = _larsContext())
                    {
                        learningDelivery = context.LARS_LearningDeliveries
                            .Where(x => learnAimRefs.Contains(x.LearnAimRef))
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get lars learning delivery", ex);
            }

            return learningDelivery;
        }

        public string GetOrganisationVersion(CancellationToken cancellationToken)
        {
            var version = string.Empty;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _organisations())
                {
                    version = context.OrgVersions
                        .OrderByDescending(v => v.MainDataSchemaName)
                        .Select(lv => lv.MainDataSchemaName)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get org version", ex);
            }

            return version;
        }

        public string GetProviderName(int ukPrn, CancellationToken cancellationToken)
        {
            var providerName = string.Empty;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _organisations())
                {
                    providerName = context.OrgDetails
                        .FirstOrDefault(o => o.Ukprn == ukPrn)
                        ?.Name;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get org provider name", ex);
            }

            return providerName;
        }

        public IList<UniqueLearnerNumber> GetUlnLookup(IList<long?> searchUlns, CancellationToken cancellationToken)
        {
            List<UniqueLearnerNumber> ulns = new List<UniqueLearnerNumber>();
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                lock (_ulnLock)
                {
                    var result = new List<UniqueLearnerNumber>();
                    var ulnShards = SplitList(searchUlns, 5000);
                    using (var context = _ulnContext())
                    {
                        foreach (var shard in ulnShards)
                        {
                            result.AddRange(context.UniqueLearnerNumbers
                                .Where(u => shard.Contains(u.Uln))
                                .ToList());
                        }

                        ulns.AddRange(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get uln data", ex);
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
