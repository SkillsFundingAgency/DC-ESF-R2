using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Service.Mappers;
using ESFA.DC.ESF.R2.Service.Services;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.Service.Tests
{
    public class ESFProviderServiceTests
    {
        [Fact]
        public async Task GetESFRecordsFromFile()
        {
            ICollection<SupplementaryDataLooseModel> esfModels = new List<SupplementaryDataLooseModel> 
            {
                new SupplementaryDataLooseModel
                {
                    CalendarMonth = "1",
                    ULN = "2"
                },
                new SupplementaryDataLooseModel
                {
                    CalendarMonth = "1",
                    ULN = "3"
                }
            };

            var cancellationToken = CancellationToken.None;

            var contextMock = new Mock<IEsfJobContext>();
            contextMock.Setup(x => x.FileName).Returns("FileName");
            contextMock.Setup(x => x.BlobContainerName).Returns("Blob");

            var csvServiceMock = new Mock<ICsvFileService>();
            csvServiceMock.Setup(x => x.BuildDefaultConfiguration()).Returns(new Configuration());
            csvServiceMock.Setup(x => x.ReadAllAsync<SupplementaryDataLooseModel, ESFMapper>(
                contextMock.Object.FileName,
                contextMock.Object.BlobContainerName,
                cancellationToken,
                It.IsAny<Configuration>(),
                null)).ReturnsAsync(esfModels);

            var models = await NewService(csvServiceMock.Object).GetESFRecordsFromFile(contextMock.Object, cancellationToken);

            esfModels.Should().BeEquivalentTo(models);
        }

        private ESFProviderService NewService(ICsvFileService csvFileService = null)
        {
            return new ESFProviderService(Mock.Of<ILogger>(), csvFileService);
        }
    }
}
