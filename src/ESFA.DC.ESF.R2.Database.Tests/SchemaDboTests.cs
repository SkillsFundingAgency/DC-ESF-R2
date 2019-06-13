using System.Collections.Generic;
using ESFA.DC.DatabaseTesting.Model;
using Xunit;

namespace ESFA.DC.ESF.R2.Database.Tests
{
    [Trait("Category", "SchemaDboTests")]

    public sealed class SchemaDboTests : IClassFixture<DatabaseConnectionFixture>
    {
        private readonly DatabaseConnectionFixture _fixture;

        public SchemaDboTests(DatabaseConnectionFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckColumnSourceFile()
        {
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateInt("SourceFileId", 1, false),
                ExpectedColumn.CreateNvarChar("FileName", 2, false, 200),
                ExpectedColumn.CreateDateTime("FilePreparationDate", 3, false),
                ExpectedColumn.CreateVarChar("ConRefNumber", 4, false, 20),
                ExpectedColumn.CreateNvarChar("UKPRN", 5, false, 20),
                ExpectedColumn.CreateDateTime("DateTime", 6, true)
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "SourceFile", expectedColumns, true);
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckColumnSupplementaryDataModel()
        {
            var index = 0;
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateInt("SupplementaryDataId", ++index, false),
                ExpectedColumn.CreateVarChar("ConRefNumber", ++index, false, 20),
                ExpectedColumn.CreateVarChar("DeliverableCode", ++index, false, 10),
                ExpectedColumn.CreateInt("CalendarYear", ++index, false),
                ExpectedColumn.CreateInt("CalendarMonth", ++index, false),
                ExpectedColumn.CreateVarChar("CostType", ++index, false, 20),
                ExpectedColumn.CreateVarChar("ReferenceType", ++index, false, 20),
                ExpectedColumn.CreateVarChar("Reference", ++index, false, 100),
                ExpectedColumn.CreateBigInt("ULN", ++index, true),
                ExpectedColumn.CreateVarChar("ProviderSpecifiedReference", ++index, true, 200),
                ExpectedColumn.CreateDecimal("Value", ++index, true, 8, 2),
                ExpectedColumn.CreateVarChar("LearnAimRef", ++index, true, 8),
                ExpectedColumn.CreateDate("SupplementaryDataPanelDate", ++index, true),
                ExpectedColumn.CreateInt("SourceFileId", ++index, false)
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "SupplementaryData", expectedColumns, true);
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckColumnSupplementaryDataModelUnitCost()
        {
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateVarChar("ConRefNumber", 1, false, 20),
                ExpectedColumn.CreateVarChar("DeliverableCode", 2, false, 10),
                ExpectedColumn.CreateInt("CalendarYear", 3, false),
                ExpectedColumn.CreateInt("CalendarMonth", 4, false),
                ExpectedColumn.CreateVarChar("CostType", 5, false, 20),
                ExpectedColumn.CreateVarChar("ReferenceType", 6, false, 20),
                ExpectedColumn.CreateVarChar("Reference", 7, false, 100),
                ExpectedColumn.CreateDecimal("Value", 8, true, 8, 2),
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "SupplementaryDataUnitCost", expectedColumns, true);
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckColumnValidationError()
        {
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateInt("SourceFileId", 1, false),
                ExpectedColumn.CreateInt("ValidationError_Id", 2, false),
                ExpectedColumn.CreateUniqueIdentifier("RowId", 3, true),
                ExpectedColumn.CreateVarChar("RuleId", 4, true, 50),
                ExpectedColumn.CreateVarChar("ConRefNumber", 5, true),
                ExpectedColumn.CreateVarChar("DeliverableCode", 6, true),
                ExpectedColumn.CreateVarChar("CalendarYear", 7, true),
                ExpectedColumn.CreateVarChar("CalendarMonth", 8, true),
                ExpectedColumn.CreateVarChar("CostType", 9, true),
                ExpectedColumn.CreateVarChar("ReferenceType", 10, true),
                ExpectedColumn.CreateVarChar("Reference", 11, true),
                ExpectedColumn.CreateVarChar("StaffName", 12, true),
                ExpectedColumn.CreateVarChar("ULN", 13, true),
                ExpectedColumn.CreateVarChar("Severity", 14, true, 2),
                ExpectedColumn.CreateVarChar("ErrorMessage", 15, true),
                ExpectedColumn.CreateVarChar("ProviderSpecifiedReference", 16, true),
                ExpectedColumn.CreateVarChar("Value", 17, true),
                ExpectedColumn.CreateVarChar("LearnAimRef", 18, true),
                ExpectedColumn.CreateVarChar("SupplementaryDataPanelDate", 19, true),
                ExpectedColumn.CreateDateTime("CreatedOn", 20, true)
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "ValidationError", expectedColumns, true);
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckValidationErrorMessage()
        {
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateInt("ValidationErrorMessageId", 1, false),
                ExpectedColumn.CreateVarChar("RuleName", 2, true),
                ExpectedColumn.CreateVarChar("ErrorMessage", 3, true)
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "ValidationErrorMessage", expectedColumns, true);
        }

        [Fact]
        [Trait("Category", "SchemaDboTests")]
        public void CheckColumnVersionInfo()
        {
            var expectedColumns = new List<ExpectedColumn>
            {
                ExpectedColumn.CreateInt("Version", 1, false),
                ExpectedColumn.CreateDate("Date", 2, false)
            };
            _fixture.SchemaTests.AssertTableColumnsExist("dbo", "VersionInfo", expectedColumns, true);
        }
    }
}
