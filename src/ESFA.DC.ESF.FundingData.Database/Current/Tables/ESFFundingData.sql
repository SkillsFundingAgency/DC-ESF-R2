CREATE TABLE [Current].[ESFFundingData]
(
	[AcademicYear] VARCHAR(10) NOT NULL,
	[UKPRN] INT NOT NULL,
	[CollectionType] VARCHAR(20) NOT NULL,
	[CollectionReturnCode] VARCHAR(10) NOT NULL,
	[LearnRefNumber] VARCHAR(12) NOT NULL,
	[AimSeqNumber] INT NOT NULL,
	[ConRefNumber] VARCHAR(20) NOT NULL,
	[DeliverableCode] VARCHAR(5) NOT NULL,
	[AttributeName] VARCHAR(100) NOT NULL,
	[Period_1] DECIMAL(15,5),
	[Period_2] DECIMAL(15,5),
	[Period_3] DECIMAL(15,5),
	[Period_4] DECIMAL(15,5),
	[Period_5] DECIMAL(15,5),
	[Period_6] DECIMAL(15,5),
	[Period_7] DECIMAL(15,5),
	[Period_8] DECIMAL(15,5),
	[Period_9] DECIMAL(15,5),
	[Period_10] DECIMAL(15,5),
	[Period_11] DECIMAL(15,5),
	[Period_12] DECIMAL(15,5), 
    CONSTRAINT [PK_ESFFundingData] PRIMARY KEY
	(
		[AcademicYear],
		[AttributeName],
		[UKPRN],
		[CollectionType],
		[CollectionReturnCode],
		[LearnRefNumber],
		[AimSeqNumber],
		[ConRefNumber],
		[DeliverableCode]
	)
)

GO

CREATE INDEX [IX_ESFFundingData_UKPRN_CollectionType_CollectionReturnCode] ON [Current].[ESFFundingData] ([UKPRN] ASC, [CollectionType] ASC, [CollectionReturnCode] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_ESFFundingData_All] ON [Current].[ESFFundingData]
(
	[UKPRN] ASC,
	[ConRefNumber] ASC,
	[DeliverableCode] ASC,
	[AttributeName] ASC,
	[CollectionType] ASC,
	[CollectionReturnCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO