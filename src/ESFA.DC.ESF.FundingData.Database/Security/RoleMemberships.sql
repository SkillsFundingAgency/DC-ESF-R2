
GO
ALTER ROLE [DataProcessing] ADD MEMBER [EsfFunding_RW_User];
GO
ALTER Role [DataViewing] ADD MEMBER [MatchClaim_RO_User];
GO
ALTER ROLE [DataViewing] ADD MEMBER [EsfFunding_RO_User];
GO
ALTER ROLE [DataViewing] ADD MEMBER [User_DSCI];
GO