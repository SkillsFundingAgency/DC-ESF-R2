using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ILRService : IILRService
    {
        private readonly IFm70DataService _fm70DataService;
        private readonly IFileDetailsDataService _fileDetailsDataService;
        private readonly IESFFundingService _esfFundingService;
        private readonly IReturnPeriodLookup _returnPeriodLookup;
        private readonly ILogger _logger;

        public ILRService(
            IFm70DataService fm70DataService,
            IFileDetailsDataService fileDetailsDataService,
            IESFFundingService esfFundingService,
            IReturnPeriodLookup returnPeriodLookup,
            ILogger logger)
        {
            _fm70DataService = fm70DataService;
            _fileDetailsDataService = fileDetailsDataService;
            _esfFundingService = esfFundingService;
            _returnPeriodLookup = returnPeriodLookup;
            _logger = logger;
        }

        public async Task<IEnumerable<ILRFileDetails>> GetIlrFileDetails(int ukPrn, int collectionYear, CancellationToken cancellationToken)
        {
            var round2Flag = collectionYear >= 2019;
            IEnumerable<ILRFileDetails> ilrFileData = (await _fileDetailsDataService.GetFileDetailsForUkPrnAllYears(ukPrn, cancellationToken, round2Flag)).ToList();
            return ilrFileData;
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrData(int ukprn, string collectionName, int collectionYear, string collectionReturnCode, CancellationToken cancellationToken)
        {
            IEnumerable<FM70PeriodisedValues> ilrData;
            if (collectionName == ReportingConstants.ILR1819)
            {
                ilrData = await _esfFundingService.GetLatestFundingDataForProvider(ukprn, collectionYear, collectionName, collectionReturnCode, cancellationToken);
            }
            else
            {
                var previousYearReturnPeriod = _returnPeriodLookup.GetReturnPeriodForPreviousCollectionYear(collectionReturnCode);

                var fm701819Data = await GetAcademicYearIlrData(ukprn, collectionYear - 1, ReportingConstants.ILR1819, previousYearReturnPeriod, cancellationToken);

                var fm701920Data = await GetAcademicYearIlrData(ukprn, collectionYear, ReportingConstants.ILR1920, collectionReturnCode, cancellationToken);

                ilrData = fm701819Data.Concat(fm701920Data);
            }

            var fm70YearlyData = GroupFm70DataIntoYears(collectionYear, ilrData);

            return fm70YearlyData;
        }

        private async Task<IEnumerable<FM70PeriodisedValues>> GetAcademicYearIlrData(int ukprn, int collectionYear, string collectionType, string currentReturnCode, CancellationToken cancellationToken)
        {
            _logger.LogInfo($"Retrieving Latest return code for UKPRN {ukprn} for {collectionType}");
            var returnCode = await _esfFundingService.GetLatestReturnCodeSubmittedForProvider(ukprn, collectionType, currentReturnCode, cancellationToken);

            if (returnCode != null)
            {
                _logger.LogInfo($"ReturnCode {returnCode} found for UKPRN {ukprn} for {collectionType}");
                var providerData = await _esfFundingService.GetLatestFundingDataForProvider(ukprn, collectionYear, collectionType, returnCode, cancellationToken);
                _logger.LogInfo($"Retrieved provider data for UKPRN {ukprn} for {collectionType}");

                return providerData;
            }

            _logger.LogInfo($"No Return code found for UKPRN {ukprn} for {collectionType}");
            return Enumerable.Empty<FM70PeriodisedValues>();
        }

        private IEnumerable<FM70PeriodisedValuesYearly> GroupFm70DataIntoYears(
            int endYear,
            IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            var yearlyFm70Data = new List<FM70PeriodisedValuesYearly>();
            if (fm70Data == null)
            {
                return yearlyFm70Data;
            }

            for (var i = ReportingConstants.StartYear; i <= endYear; i++)
            {
                yearlyFm70Data.Add(new FM70PeriodisedValuesYearly
                {
                    FundingYear = i,
                    Fm70PeriodisedValues = new List<FM70PeriodisedValues>()
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