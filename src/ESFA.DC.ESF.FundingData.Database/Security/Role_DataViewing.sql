
CREATE ROLE [DataViewing] AUTHORIZATION [dbo]
GO

-- Grant access rights to a specific schema in the database
GRANT 
	SELECT, 
	VIEW DEFINITION 
ON SCHEMA::[dbo]
	TO [DataViewing];
GO

GRANT 
	SELECT, 
	VIEW DEFINITION 
ON SCHEMA::[Current]
	TO [DataViewing];
GO
