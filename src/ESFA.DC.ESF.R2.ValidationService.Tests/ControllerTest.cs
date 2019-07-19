using System;
using System.Collections.Generic;
using System.Threading;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.DataAccessLayer.Mappers;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using ESFA.DC.ESF.R2.ValidationService.Commands.CrossRecord;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests
{
    public class ControllerTest : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void TestController()
        {
            Mock<IReferenceDataService> cacheService = new Mock<IReferenceDataService>();
            cacheService.Setup(m => m.GetUlnLookup(It.IsAny<IList<long?>>(), It.IsAny<CancellationToken>())).Returns(new HashSet<long>());
            cacheService.Setup(m => m.CurrentPeriod).Returns(10);

            Mock<IFcsCodeMappingHelper> mapperMock = new Mock<IFcsCodeMappingHelper>();
            mapperMock.Setup(m =>
                m.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>())).Returns(1);

            Mock<IPopulationService> popMock = new Mock<IPopulationService>();
            popMock.Setup(m => m.PrePopulateUlnCache(It.IsAny<IList<long?>>(), It.IsAny<CancellationToken>()));

            SupplementaryDataModelMapper mapper = new SupplementaryDataModelMapper();

            var looseValidation = GetLooseValidators();
            var validators = GetValidators(cacheService, mapperMock);
            var controller = new ValidationController(looseValidation, validators, popMock.Object, mapper, null);

            var wrapper = new SupplementaryDataWrapper
            {
                SupplementaryDataLooseModels = GetSupplementaryDataList()
            };

            controller.ValidateData(wrapper, GetEsfSourceFileModel(), CancellationToken.None);

            //Assert.True(controller.Errors.Any());
        }

        private IList<SupplementaryDataLooseModel> GetSupplementaryDataList()
        {
            return new List<SupplementaryDataLooseModel>
            {
                GetSupplementaryData()
            };
        }

        private SourceFileModel GetEsfSourceFileModel()
        {
            return new SourceFileModel
            {
                UKPRN = "10005752",
                JobId = 1,
                ConRefNumber = "ESF-2108",
                FileName = "SUPPDATA-10005752-ESF-2108-20180909-090911.CSV",
                SuppliedDate = DateTime.Now,
                PreparationDate = DateTime.Now.AddDays(-1)
            };
        }

        private SupplementaryDataLooseModel GetSupplementaryData()
        {
            return new SupplementaryDataLooseModel
            {
                ConRefNumber = "ESF - 2270",
                DeliverableCode = "ST01",
                CalendarYear = "2016",
                CalendarMonth = "5",
                CostType = "Unit Cost",
                Reference = "|",
                ReferenceType = "LearnRefNumber",
                ULN = "1000000019",
                ProviderSpecifiedReference = "DelCode 01A"
            };
        }

        private ILooseValidatorCommand GetLooseValidators()
        {
            return new FieldDefinitionCommand(
                new List<IFieldDefinitionValidator>
                {
                    new FDCalendarMonthAL(_messageServiceMock.Object),
                    new FDCalendarMonthDT(_messageServiceMock.Object),
                    new FDCalendarMonthMA(_messageServiceMock.Object),
                    new FDCalendarYearAL(_messageServiceMock.Object),
                    new FDCalendarYearDT(_messageServiceMock.Object),
                    new FDCalendarYearMA(_messageServiceMock.Object),
                    new FDConRefNumberAL(_messageServiceMock.Object),
                    new FDConRefNumberMA(_messageServiceMock.Object),
                    new FDCostTypeAL(_messageServiceMock.Object),
                    new FDCostTypeMA(_messageServiceMock.Object),
                    new FDDeliverableCodeAL(_messageServiceMock.Object),
                    new FDDeliverableCodeMA(_messageServiceMock.Object),
                    new FDProviderSpecifiedReferenceAL(_messageServiceMock.Object),
                    new FDReferenceAL(_messageServiceMock.Object),
                    new FDReferenceMA(_messageServiceMock.Object),
                    new FDReferenceTypeAL(_messageServiceMock.Object),
                    new FDReferenceTypeMA(_messageServiceMock.Object),
                    new FDULNAL(_messageServiceMock.Object),
                    new FDULNDT(_messageServiceMock.Object),
                    new FDValueAL(_messageServiceMock.Object)
                });
        }

        private IList<IValidatorCommand> GetValidators(Mock<IReferenceDataService> serviceMock, Mock<IFcsCodeMappingHelper> mapperMock)
        {
            var date = DateTime.Now;
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(date);
            dateTimeProvider.Setup(m => m.ConvertUtcToUk(date)).Returns(date);

            var monthYearHelperMock = new Mock<IMonthYearHelper>();
            monthYearHelperMock
                .Setup(m => m.GetCalendarDateTime(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(DateTime.Now);

            return new List<IValidatorCommand>
            {
                new BusinessRuleCommands(
                    new List<IBusinessRuleValidator>
                    {
                        new CalendarMonthRule01(_messageServiceMock.Object),
                        new CalendarYearCalendarMonthRule01(_messageServiceMock.Object, dateTimeProvider.Object, serviceMock.Object),
                        new CalendarYearCalendarMonthRule02(_messageServiceMock.Object, serviceMock.Object, mapperMock.Object),
                        new CalendarYearCalendarMonthRule03(_messageServiceMock.Object, serviceMock.Object, mapperMock.Object),
                        new CalendarYearRule01(_messageServiceMock.Object),
                        new CostTypeRule01(_messageServiceMock.Object),
                        new CostTypeRule02(_messageServiceMock.Object),
                        new DeliverableCodeRule01(_messageServiceMock.Object),
                        new DeliverableCodeRule02(_messageServiceMock.Object, serviceMock.Object, mapperMock.Object),
                        new ProviderSpecifiedReferenceRule01(_messageServiceMock.Object),
                        new ReferenceRule01(_messageServiceMock.Object),
                        new ReferenceTypeRule02(_messageServiceMock.Object),
                        new ReferenceTypeRule01(_messageServiceMock.Object),
                        new ULNRule01(_messageServiceMock.Object),
                        new ULNRule02(_messageServiceMock.Object, serviceMock.Object),
                        new ULNRule03(_messageServiceMock.Object, dateTimeProvider.Object, monthYearHelperMock.Object),
                        new ULNRule04(_messageServiceMock.Object),
                        new ValueRule01(_messageServiceMock.Object),
                        new ValueRule02(_messageServiceMock.Object)
                    }),
                new CrossRecordCommands(
                    new List<ICrossRecordValidator>
                    {
                        new Duplicate01(_messageServiceMock.Object)
                    })
            };
        }
    }
}
