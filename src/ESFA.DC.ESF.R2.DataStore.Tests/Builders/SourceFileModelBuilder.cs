using System;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataStore.Tests.Builders
{
    public class SourceFileModelBuilder
    {
        public static SourceFileModel BuildSourceFileModel()
        {
            return new SourceFileModel
            {
                ConRefNumber = "1234567890abcdefghij",
                UKPRN = "12345678",
                FileName = "foo.csv",
                PreparationDate = DateTime.Now.AddDays(-1),
                SuppliedDate = DateTime.Now
            };
        }
    }
}
