using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ILRService : IILRService
    {
        private const int StartYear = 2015;
        private const int EndYear = 2019;

        private readonly IFm70DataService _fm70DataService;
        private readonly IFileDetailsDataService _fileDetailsDataService;

        public ILRService(
            IFm70DataService fm70DataService,
            IFileDetailsDataService fileDetailsDataService)
        {
            _fm70DataService = fm70DataService;
            _fileDetailsDataService = fileDetailsDataService;
        }

        public async Task<IEnumerable<ILRFileDetails>> GetIlrFileDetails(int ukPrn, CancellationToken cancellationToken)
        {
            IEnumerable<ILRFileDetails> ilrFileData = (await _fileDetailsDataService.GetFileDetailsForUkPrnAllYears(ukPrn, cancellationToken)).ToList();

            return ilrFileData;
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrData(int ukPrn, CancellationToken cancellationToken)
        {
            IEnumerable<FM70PeriodisedValues> ilrData = await _fm70DataService.GetPeriodisedValuesAllYears(ukPrn, cancellationToken);

            var fm70YearlyData = GroupFm70DataIntoYears(ilrData);

            return fm70YearlyData;
        }

        private IEnumerable<FM70PeriodisedValuesYearly> GroupFm70DataIntoYears(IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            var yearlyFm70Data = new List<FM70PeriodisedValuesYearly>();
            if (fm70Data == null)
            {
                return yearlyFm70Data;
            }

            for (var i = StartYear; i < EndYear; i++)
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