namespace ESFA.DC.ESF.R2.DataStore.Constants
{
    public static class DataStoreConstants
    {
        public static class TableNameConstants
        {
            public const string EsfSuppData = "dbo.SupplementaryData";
            public const string EsfSuppDataUnitCost = "dbo.SupplementaryDataUnitCost";
            public const string EsfSuppDataValidationError = "dbo.ValidationError";
        }

        public static class StoredProcedureNameConstants
        {
            public const string DeleteExistingRecords = "[dbo].[DeleteExistingRecords]";
        }

        public static class ErrorSeverity
        {
            public const string Error = "E";
            public const string Warning = "W";
        }
    }
}