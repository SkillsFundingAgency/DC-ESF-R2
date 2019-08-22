CREATE TABLE [Import].[LatestProviderSubmission]
(
	[UKPRN] INT NOT NULL,
	[CollectionType] VARCHAR(20) NOT NULL,
	[CollectionReturnCode] VARCHAR(10) NOT NULL,
	CONSTRAINT [PK_LatestProviderSubmission] PRIMARY KEY CLUSTERED
	(
		[UKPRN] ASC,
		[CollectionType] ASC,
		[CollectionReturnCode] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
