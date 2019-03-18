using System;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataStore.Tests.Builders
{
    public class SupplementaryDataModelBuilder
    {
        public static SupplementaryDataModel BuildSupplementaryData()
        {
            return new SupplementaryDataModel
            {
                ConRefNumber = "123456789abcdefghij",
                DeliverableCode = "1234567890",
                CalendarYear = 2018,
                CalendarMonth = 9,
                CostType = "foo",
                Reference = "asasdadad asdadadada asdadsasdad",
                ReferenceType = "thingie",
                ProviderSpecifiedReference = "123131312ae qq12123",
                ULN = 3456789012,
                LearnAimRef = "12345678",
                SupplementaryDataPanelDate = new DateTime(2019, 2, 19),
                Value = 75432.87M
            };
        }
    }
}
