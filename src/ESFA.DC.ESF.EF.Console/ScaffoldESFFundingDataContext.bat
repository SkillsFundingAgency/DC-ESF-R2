dotnet.exe ef dbcontext scaffold "Server=.;Database=ESFFundingData;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -c ESFFundingDataContext --schema Current --schema dbo --force --output-dir ..\ESFA.DC.ESF.FundingData.Database.EF --verbose