using System.Configuration;
using ESFA.DC.DatabaseTesting;
using ESFA.DC.DatabaseTesting.Interface;

namespace ESFA.DC.ESF.R2.Database.Tests
{
    public sealed class DatabaseConnectionFixture
    {
        public DatabaseConnectionFixture()
        {
            string connectionString = ConfigurationManager.AppSettings["TestConnectionString"];
            IDbConnectorConfiguration dbConnectorConfiguration = new DbConnectorConfiguration(connectionString);
            IDbConnector dbConnector = new DbConnector(dbConnectorConfiguration);
            SchemaTests = new SchemaTests(dbConnector);
        }

        public SchemaTests SchemaTests { get; }
    }
}
