﻿CREATE USER [ESF_R2_RW_User]
    WITH PASSWORD = N'$(RWUserPassword)';
GO
	GRANT CONNECT TO [ESF_R2_RW_User]
GO


