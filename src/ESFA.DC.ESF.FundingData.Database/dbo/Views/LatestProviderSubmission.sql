CREATE VIEW [dbo].[LatestProviderSubmission]
	AS
	SELECT
		[UKPRN],
		[CollectionType],
		[CollectionReturnCode]
	FROM [Import].[LatestProviderSubmission]
	UNION ALL
	SELECT
		[UKPRN],
		[CollectionType],
		[CollectionReturnCode]
	FROM [Current].[LatestProviderSubmission]