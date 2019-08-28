CREATE USER [EsfFunding_RW_User]
    WITH PASSWORD = N'$(RWUserPassword)';
GO
	GRANT CONNECT TO [EsfFunding_RW_User]
GO


