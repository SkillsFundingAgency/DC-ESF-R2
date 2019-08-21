CREATE TABLE [Import].[ESFFundingData]
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
