
CREATE USER [MatchClaim_RO_User] WITH PASSWORD = N'$(MatchClaimROPassword)';
GO
GRANT CONNECT TO [MatchClaim_RO_User];
GO
