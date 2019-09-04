CREATE VIEW [dbo].[ESFFundingDataSummarised]
	AS SELECT 
      [UKPRN]
      ,[ConRefNumber]
      ,[DeliverableCode]
      ,[AttributeName]
      ,Sum([Period_1]) AS Period_1
      ,Sum([Period_2]) AS Period_2
      ,Sum([Period_3]) AS Period_3
	  ,Sum([Period_4]) AS Period_4
	  ,Sum([Period_5]) AS Period_5
	  ,Sum([Period_6]) AS Period_6
	  ,Sum([Period_7]) AS Period_7
	  ,Sum([Period_8]) AS Period_8
	  ,Sum([Period_9]) AS Period_9
	  ,Sum([Period_10]) AS Period_10
	  ,Sum([Period_11]) AS Period_11
	  ,Sum([Period_12]) AS Period_12
      ,Cast(RIGHT([CollectionType], 4) as int) AS [CollectionYear]
      ,Cast(RIGHT(case when CollectionReturnCode = '' then 'R00' else CollectionReturnCode end, 2) as int)  as [CollectionPeriod]
  FROM [dbo].[ESFFundingData]
  group by [UKPRN]
      ,[ConRefNumber]
      ,[DeliverableCode]
      ,[AttributeName]
	  ,[CollectionType]
      ,[CollectionReturnCode]
