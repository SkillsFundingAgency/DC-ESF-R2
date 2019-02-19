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
using ESFA.DC.ReferenceData.ULN.Model;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests
{
    public class ControllerTest
    {
        [Fact]
        public void TestController()
        {
            Mock<IReferenceDataCache> cacheMock = new Mock<IReferenceDataCache>();
            cacheMock.Setup(m => m.GetUlnLookup(It.IsAny<IList<long?>>(), It.IsAny<CancellationToken>())).Returns(new List<UniqueLearnerNumber>());
            cacheMock.Setup(m => m.CurrentPeriod).Returns(10);

            Mock<IFcsCodeMappingHelper> mapperMock = new Mock<IFcsCodeMappingHelper>();
            mapperMock.Setup(m =>
                m.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>())).Returns(1);

            Mock<IPopulationService> popMock = new Mock<IPopulationService>();
            popMock.Setup(m => m.PrePopulateUlnCache(It.IsAny<IList<long?>>(), It.IsAny<CancellationToken>()));

            SupplementaryDataModelMapper mapper = new SupplementaryDataModelMapper();

            var looseValidation = GetLooseValidators();
            var validators = GetValidators(cacheMock, mapperMock);
            var controller = new ValidationController(looseValidation, validators, popMock.Object, mapper);

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
                    new FDCalendarMonthAL(),
                    new FDCalendarMonthDT(),
                    new FDCalendarMonthMA(),
                    new FDCalendarYearAL(),
                    new FDCalendarYearDT(),
                    new FDCalendarYearMA(),
                    new FDConRefNumberAL(),
                    new FDConRefNumberMA(),
                    new FDCostTypeAL(),
                    new FDCostTypeMA(),
                    new FDDeliverableCodeAL(),
                    new FDDeliverableCodeMA(),
                    new FDProviderSpecifiedReferenceAL(),
                    new FDReferenceAL(),
                    new FDReferenceMA(),
                    new FDReferenceTypeAL(),
                    new FDReferenceTypeMA(),
                    new FDULNAL(),
                    new FDULNDT(),
                    new FDValueAL()
                });
        }

        private IList<IValidatorCommand> GetValidators(Mock<IReferenceDataCache> cacheMock, Mock<IFcsCodeMappingHelper> mapperMock)
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(DateTime.Now);

            return new List<IValidatorCommand>
            {
                new BusinessRuleCommands(
                    new List<IBusinessRuleValidator>
                    {
                        new CalendarMonthRule01(),
                        new CalendarYearCalendarMonthRule01(dateTimeProvider.Object, cacheMock.Object),
                        new CalendarYearCalendarMonthRule02(cacheMock.Object, mapperMock.Object),
                        new CalendarYearCalendarMonthRule03(cacheMock.Object, mapperMock.Object),
                        new CalendarYearRule01(),
                        new CostTypeRule01(),
                        new CostTypeRule02(),
                        new DeliverableCodeRule01(),
                        new DeliverableCodeRule02(cacheMock.Object, mapperMock.Object),
                        new ProviderSpecifiedReferenceRule01(),
                        new ReferenceRule01(),
                        new ReferenceTypeRule02(),
                        new ReferenceTypeRule01(),
                        new ULNRule01(),
                        new ULNRule02(cacheMock.Object),
                        new ULNRule03(dateTimeProvider.Object),
                        new ULNRule04(),
                        new ValueRule01(),
                        new ValueRule02()
                    }),
                new CrossRecordCommands(
                    new List<ICrossRecordValidator>
                    {
                        new Duplicate01()
                    })
            };
        }
    }
}
