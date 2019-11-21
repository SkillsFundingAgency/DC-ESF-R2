CREATE VIEW [dbo].[ESFFundingData] AS
	SELECT
		[AcademicYear],
		[UKPRN],
		[CollectionType],
		[CollectionReturnCode],
		[LearnRefNumber],
		[AimSeqNumber],
		[ConRefNumber],
		[DeliverableCode],
		[AttributeName],
		[Period_1],
		[Period_2],
		[Period_3],
		[Period_4],
		[Period_5],
		[Period_6],
		[Period_7],
		[Period_8],
		[Period_9],
		[Period_10],
		[Period_11],
		[Period_12]
	FROM [Current].[ESFFundingData]