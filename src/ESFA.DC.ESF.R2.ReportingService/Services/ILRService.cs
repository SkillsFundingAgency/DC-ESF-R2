using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ILRService : IILRService
    {
        private const int StartYear = 2015;
        private const int EndYear = 2019;

        private readonly IFM70Repository _repository;
        private readonly ILegacyILRService _legacyIlrService;

        public ILRService(
            IFM70Repository repository,
            ILegacyILRService legacyILRService)
        {
            _repository = repository;
            _legacyIlrService = legacyILRService;
        }

        public async Task<IEnumerable<ILRFileDetailsModel>> GetIlrFileDetails(int ukPrn, CancellationToken cancellationToken)
        {
            ILRFileDetailsModel ilrFileData = await _repository.GetFileDetails(ukPrn, cancellationToken);

            IEnumerable<ILRFileDetailsModel> previousYearsFiles = await _legacyIlrService.GetPreviousYearsILRFileDetails(ukPrn, cancellationToken);

            var ilrYearlyFileData = new List<ILRFileDetailsModel>();
            if (previousYearsFiles != null)
            {
                ilrYearlyFileData.AddRange(previousYearsFiles.OrderBy(fd => fd.Year));
            }

            ilrYearlyFileData.Add(ilrFileData);

            return ilrYearlyFileData;
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearlyModel>> GetYearlyIlrData(int ukPrn, CancellationToken cancellationToken)
        {
            IList<FM70PeriodisedValuesModel> ilrData = await _repository.GetPeriodisedValues(ukPrn, cancellationToken);
            var previousYearsILRData = await _legacyIlrService.GetPreviousYearsFM70Data(ukPrn, cancellationToken);
            var allILRData = new List<FM70PeriodisedValuesModel>();
            if (previousYearsILRData != null)
            {
                allILRData.AddRange(previousYearsILRData);
            }

            allILRData.AddRange(ilrData);
            var fm70YearlyData = GroupFm70DataIntoYears(allILRData);

            return fm70YearlyData;
        }

        private IEnumerable<FM70PeriodisedValuesYearlyModel> GroupFm70DataIntoYears(IList<FM70PeriodisedValuesModel> fm70Data)
        {
            var yearlyFm70Data = new List<FM70PeriodisedValuesYearlyModel>();
            if (fm70Data == null)
            {
                return yearlyFm70Data;
            }

            for (var i = StartYear; i < EndYear; i++)
            {
                yearlyFm70Data.Add(new FM70PeriodisedValuesYearlyModel
                {
                    FundingYear = i,
                    Fm70PeriodisedValues = new List<FM70PeriodisedValuesModel>()
                });
            }

            var fm70DataModels = fm70Data.ToList();

            if (!fm70DataModels.Any())
            {
                return yearlyFm70Data;
            }

            foreach (var yearlyModel in yearlyFm70Data)
            {
                yearlyModel.Fm70PeriodisedValues = fm70DataModels
                    .Where(sd => sd.FundingYear == yearlyModel.FundingYear)
                    .ToList();
            }

            return yearlyFm70Data;
        }
    }
}