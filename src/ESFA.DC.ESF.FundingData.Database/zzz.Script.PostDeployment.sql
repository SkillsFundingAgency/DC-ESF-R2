/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO
-- Set ExtendedProperties fro DB.
	:r .\z.ExtendedProperties.sql
GO
RAISERROR('		   Update User Account Passwords',10,1) WITH NOWAIT;
GO
ALTER ROLE [db_datareader] DROP MEMBER [EsfFunding_RO_User];
GO
ALTER ROLE [db_datareader] DROP MEMBER [EsfFunding_RW_User];
GO
ALTER ROLE [db_datawriter] DROP MEMBER [EsfFunding_RW_User];
GO

RAISERROR('		       RO User',10,1) WITH NOWAIT;
ALTER USER [EsfFunding_RO_User] WITH PASSWORD = N'$(ROUserPassword)';
GO
RAISERROR('		       RW User',10,1) WITH NOWAIT;
ALTER USER [EsfFunding_RW_User] WITH PASSWORD = N'$(RWUserPassword)';
GO
RAISERROR('		       DSCI User',10,1) WITH NOWAIT;
ALTER USER [User_DSCI] WITH PASSWORD = N'$(DsciUserPassword)';
GO
RAISERROR('		       Match Claim User',10,1) WITH NOWAIT;
ALTER USER [MatchClaim_RO_User] WITH PASSWORD = N'$(MatchClaimROPassword)';
GO
RAISERROR('Completed',10,1) WITH NOWAIT;
GO
