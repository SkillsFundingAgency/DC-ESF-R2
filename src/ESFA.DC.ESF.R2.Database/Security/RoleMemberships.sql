﻿
GO
ALTER ROLE [DataProcessor] ADD MEMBER [ESF_R2_RW_User];
GO
ALTER ROLE [DataViewer] ADD MEMBER [ESF_R2_RO_User];
GO
ALTER ROLE [DataViewer] ADD MEMBER [User_DSCI];
GO