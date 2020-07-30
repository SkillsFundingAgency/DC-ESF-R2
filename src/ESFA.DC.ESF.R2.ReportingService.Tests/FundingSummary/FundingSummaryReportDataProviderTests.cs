using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.FundingSummary
{
    public class FundingSummaryReportDataProviderTests
    {
        [Fact]
        public async Task ProvideOrganisationReferenceDataAsync()
        {
            var ukprn = 12345678;
            var cancellationToken = CancellationToken.None;
            var conRefNumbers = new List<string> { "Ref1", "Ref2" };

            var referenceDataServiceMock = new Mock<IReferenceDataService>();
            referenceDataServiceMock.Setup(x => x.GetProviderName(ukprn, cancellationToken)).Returns("Org Name");
            referenceDataServiceMock.Setup(x => x.GetContractAllocationsForUkprn(ukprn, cancellationToken)).ReturnsAsync(conRefNumbers);

            var expectedResult = new OrganisationReferenceData
            {
                ConRefNumbers = conRefNumbers,
                Name = "Org Name",
                Ukprn = 12345678
            };

            var orgData = await NewProvider(referenceDataServiceMock.Object).ProvideOrganisationReferenceDataAsync(ukprn, cancellationToken);

            orgData.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ProvideReferenceDataVersionsAsync()
        {
            var ukprn = 12345678;
            var cancellationToken = CancellationToken.None;
            var conRefNumbers = new List<string> { "Ref1", "Ref2" };

            var referenceDataServiceMock = new Mock<IReferenceDataService>();
            referenceDataServiceMock.Setup(x => x.GetLarsVersion(cancellationToken)).ReturnsAsync("Lars Version");
            referenceDataServiceMock.Setup(x => x.GetPostcodeVersion(cancellationToken)).ReturnsAsync("Postcodes Version");
            referenceDataServiceMock.Setup(x => x.GetOrganisationVersion(cancellationToken)).ReturnsAsync("Org Version");

            var expectedResult = new ReferenceDataVersions
            {
                LarsVersion = "Lars Version",
                PostcodeVersion = "Postcodes Version",
                OrganisationVersion = "Org Version"
            };

            var refDataVersions = await NewProvider(referenceDataServiceMock.Object).ProvideReferenceDataVersionsAsync(cancellationToken);

            refDataVersions.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetImportFilesAsync()
        {
            var cancellationToken = CancellationToken.None;

            var esfSourceFIles = new List<SourceFileModel>
            {
                new SourceFileModel { SourceFileId = 1, ConRefNumber = "ConRef1" },
                new SourceFileModel { SourceFileId = 2, ConRefNumber = "ConRef1" },
                new SourceFileModel { SourceFileId = 3, ConRefNumber = "ConRef2" },
            };

            var suppDataDataServiceMock = new Mock<ISupplementaryDataService>();
            suppDataDataServiceMock.Setup(x => x.GetImportFiles("12345678", cancellationToken)).ReturnsAsync(esfSourceFIles);

            var files = await NewProvider(supplementaryDataService: suppDataDataServiceMock.Object).GetImportFilesAsync(12345678, cancellationToken);

            files.Should().BeEquivalentTo(esfSourceFIles);
        }

        [Fact]
        public async Task GetImportFilesAsync_UkprnMismatch()
        {
            var cancellationToken = CancellationToken.None;

            var esfSourceFIles = new List<SourceFileModel>();

            var suppDataDataServiceMock = new Mock<ISupplementaryDataService>();
            suppDataDataServiceMock.Setup(x => x.GetImportFiles("12345678", cancellationToken)).ReturnsAsync(esfSourceFIles);

            var files = await NewProvider(supplementaryDataService: suppDataDataServiceMock.Object).GetImportFilesAsync(12345678, cancellationToken);

            files.Should().BeEquivalentTo(esfSourceFIles);
        }

        [Fact]
        public async Task GetSupplementaryDataAsync()
        {
            int endYear = 2020;
            var cancellationToken = CancellationToken.None;

            var suppData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "ConRef1", new List<SupplementaryDataYearlyModel>()
                }
            };

            var suppDataDataServiceMock = new Mock<ISupplementaryDataService>();
            suppDataDataServiceMock.Setup(x => x.GetSupplementaryData(endYear, It.IsAny<IEnumerable<SourceFileModel>>(), cancellationToken)).ReturnsAsync(suppData);

            var refDataVersions = await NewProvider(supplementaryDataService: suppDataDataServiceMock.Object).GetSupplementaryDataAsync(endYear, It.IsAny<IEnumerable<SourceFileModel>>(), cancellationToken);

            refDataVersions.Should().BeEquivalentTo(suppData);
        }

        [Fact]
        public async Task GetSupplementaryDataAsync_Empty()
        {
            int endYear = 2020;
            var cancellationToken = CancellationToken.None;
            var sourceFiles = new List<SourceFileModel>();

            var suppData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>(StringComparer.OrdinalIgnoreCase);

            var suppDataDataServiceMock = new Mock<ISupplementaryDataService>();
            suppDataDataServiceMock.Setup(x => x.GetSupplementaryData(endYear, sourceFiles, cancellationToken)).ReturnsAsync(suppData);

            var refDataVersions = await NewProvider(supplementaryDataService: suppDataDataServiceMock.Object).GetSupplementaryDataAsync(endYear, sourceFiles, cancellationToken);

            refDataVersions.Should().BeEquivalentTo(suppData);
        }

        private FundingSummaryReportDataProvider NewProvider(IReferenceDataService referenceDataService = null, ISupplementaryDataService supplementaryDataService = null, IIlrDataProvider ilrDataProvider = null)
        {
            return new FundingSummaryReportDataProvider(
                referenceDataService ?? Mock.Of<IReferenceDataService>(),
                supplementaryDataService ?? Mock.Of<ISupplementaryDataService>(),
                ilrDataProvider ?? Mock.Of<IIlrDataProvider>());
        }
    }
}
