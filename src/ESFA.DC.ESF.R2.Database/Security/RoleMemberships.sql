
GO
ALTER ROLE [DataProcessing] ADD MEMBER [ESF_R2_RW_User];
GO
ALTER Role [DataViewing] ADD MEMBER [MatchClaim_RO_User];
GO
ALTER ROLE [DataViewing] ADD MEMBER [ESF_R2_RO_User];
GO
ALTER ROLE [DataViewing] ADD MEMBER [User_DSCI];
GO