using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.Builders
{
    public class SupplementaryDataModelBuilder
    {
        public static List<SupplementaryDataModel> GetModels()
        {
            return new List<SupplementaryDataModel>
            {
                new SupplementaryDataModel
                {
                    ConRefNumber = "ESF-2108",
                    ULN = 9900000004,
                    DeliverableCode = "ST01",
                    CostType = "test",
                    Reference = "test",
                    ReferenceType = "test",
                    CalendarYear = 2018,
                    CalendarMonth = 10,
                    ProviderSpecifiedReference = "test",
                    StaffName = "test",
                    LearnAimRef = "12345678",
                    SupplementaryDataPanelDate = new DateTime(2019, 4, 1),
                    Value = 35.00M
                }
            };
        }
    }
}