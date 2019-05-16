BEGIN

	DECLARE @SummaryOfChanges_Collection TABLE ([ValidationErrorMessageId] INT, [Action] VARCHAR(100));

	MERGE INTO [dbo].[ValidationErrorMessage] AS Target
	USING (
			SELECT NewRecords.[RuleName], NewRecords.[ErrorMessage]
			FROM 
			(
				SELECT 'CalendarMonth_01' AS RuleName, 'The CalendarMonth is not valid.' AS ErrorMessage
					UNION ALL SELECT 'CalendarYearCalendarMonth_01' AS RuleName, 'The CalendarMonth you have submitted data for cannot be in the future for the current collection period.' AS ErrorMessage
					UNION ALL SELECT 'CalendarYearCalendarMonth_02' AS RuleName, 'The CalendarMonth and CalendarYear is prior to the contract allocation start date.' AS ErrorMessage
					UNION ALL SELECT 'CalendarYearCalendarMonth_03' AS RuleName, 'The CalendarMonth and CalendarYear is after the contract allocation end date.' AS ErrorMessage
					UNION ALL SELECT 'CalendarYear_01' AS RuleName, 'The CalendarYear is not valid' AS ErrorMessage
					UNION ALL SELECT 'ConRefNumber_02' AS RuleName, 'The ConRefNumber is not a valid ConRefNumber for ESF Round 2.' AS ErrorMessage
					UNION ALL SELECT 'CostType_01' AS RuleName, 'The CostType is not valid' AS ErrorMessage
					UNION ALL SELECT 'CostType_02' AS RuleName, 'The CostType is not valid for the DeliverableCode. Please refer to the ESF Supplementary Data supporting documentation for further information.' AS ErrorMessage
					UNION ALL SELECT 'DeliverableCode_01' AS RuleName, 'The DeliverableCode is not valid.' AS ErrorMessage
					UNION ALL SELECT 'DeliverableCode_02' AS RuleName, 'The DeliverableCode is not valid for the approved contract allocation.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_01' AS RuleName, 'LearnAimRef must be returned for the selected DeliverableCode.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_02' AS RuleName, 'LearnAimRef is not required for the selected DeliverableCode.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_03' AS RuleName, 'LearnAimRef does not exist on LARS.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_04' AS RuleName, 'The CalendarMonth/CalendarYear is not within the LARS funding validity dates for the LearnAimRef.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_05' AS RuleName, 'LearnAimRef must be a regulated aim on LARS for the selected DeliverableCode.' AS ErrorMessage
					UNION ALL SELECT 'LearnAimRef_06' AS RuleName, 'LearnAimRef must be a non-regulated aim on LARS for the selected DeliverableCode.' AS ErrorMessage
					UNION ALL SELECT 'ProviderSpecifiedReference_01' AS RuleName, 'The ProviderSpecifiedReference contains invalid characters.' AS ErrorMessage
					UNION ALL SELECT 'Reference_01' AS RuleName, 'The Reference contains invalid characters.' AS ErrorMessage
					UNION ALL SELECT 'ReferenceType_01' AS RuleName, 'The ReferenceType is not valid.' AS ErrorMessage
					UNION ALL SELECT 'ReferenceType_02' AS RuleName, 'The ReferenceType is not valid for the selected CostType. Please refer to the ESF Supplementary Data supporting documentation for further information.' AS ErrorMessage
					UNION ALL SELECT 'SupplementaryDataPanelDate_01' AS RuleName, 'The SupplementaryDataPanelDate cannot be in the future.' AS ErrorMessage
					UNION ALL SELECT 'SupplementaryDataPanelDate_02' AS RuleName, 'The SupplementaryDataPanelDate cannot be before 01/04/2019.' AS ErrorMessage
					UNION ALL SELECT 'SupplementaryDataPanelDate_03' AS RuleName, 'SupplementaryDataPanelDate must be returned for the selected DeliverableCode.' AS ErrorMessage
					UNION ALL SELECT 'ULN_01' AS RuleName, 'The ULN must be returned.' AS ErrorMessage
					UNION ALL SELECT 'ULN_02' AS RuleName, 'The ULN is not a valid ULN.' AS ErrorMessage
					UNION ALL SELECT 'ULN_03' AS RuleName, 'This ULN should not be used for months that are more than two months older than the current month.' AS ErrorMessage
					UNION ALL SELECT 'ULN_04' AS RuleName, 'The ULN is not required for the selected ReferenceType.' AS ErrorMessage
					UNION ALL SELECT 'Value_01' AS RuleName, 'The Value must be returned for the selected CostType.' AS ErrorMessage
					UNION ALL SELECT 'Value_02' AS RuleName, 'The Value is not required for the selected CostType' AS ErrorMessage

					UNION ALL SELECT 'FD_CalendarMonth_AL' AS RuleName, 'The CalendarMonth must not exceed 2 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_CalendarMonth_DT' AS RuleName, 'CalendarMonth must be an integer (whole number). Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_CalendarMonth_MA' AS RuleName, 'The CalendarMonth is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_CalendarYear_DT' AS RuleName, 'CalendarYear must be an integer (whole number). Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_CalendarYear_MA' AS RuleName, 'The CalendarYear is mandatory.  Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_CalendarYear_AL' AS RuleName, 'The CalendarYear must not exceed 4 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_ConRefNumber_AL' AS RuleName, 'The ConRefNumber must not exceed 20 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_ConRefNumber_MA' AS RuleName, 'The ConRefNumber is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_CostType_AL' AS RuleName, 'The CostType must not exceed 20 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_CostType_MA' AS RuleName, 'The CostType is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_DeliverableCode_AL' AS RuleName, 'The DeliverableCode must not exceed 10 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_DeliverableCode_MA' AS RuleName, 'The DeliverableCode is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_LearnAimRef_AL' AS RuleName, 'The LearnAimRef must not exceed 8 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					
					UNION ALL SELECT 'FD_ProviderSpecifiedReference_AL' AS RuleName, 'The ProviderSpecifiedReference must not exceed 200 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_Reference_AL' AS RuleName, 'The Reference must not exceed 100 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_Reference_MA' AS RuleName, 'The Reference is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage
					UNION ALL SELECT 'FD_ReferenceType_AL' AS RuleName, 'The ReferenceType must not exceed 20 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_ReferenceType_MA' AS RuleName, 'The ReferenceType is mandatory. Please resubmit the file including the appropriate value.' AS ErrorMessage

					UNION ALL SELECT 'FD_SupplementaryDataPanelDate_DT' AS RuleName, 'SupplementaryDataPanelDate must be a date in the format DD/MM/YYYY.  Please adjust the value and resubmit the file.' AS ErrorMessage
					
					UNION ALL SELECT 'FD_ULN_AL' AS RuleName, 'The ULN must not exceed 10 characters in length. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_ULN_DT' AS RuleName, 'ULN must be an integer between 1000000000 and 9999999999. Please adjust the value and resubmit the file.' AS ErrorMessage
					UNION ALL SELECT 'FD_Value_AL' AS RuleName, 'The Value does not conform to the decimal (8, 2) field type. Please adjust the value and resubmit the file.' AS ErrorMessage

					UNION ALL SELECT 'ConRefNumber_01' AS RuleName, 'There is a discrepency between the filename ConRefNumber and ConRefNumbers within the file.' AS ErrorMessage
					UNION ALL SELECT 'Filename_08' AS RuleName, 'The date/time of the file is not greater than a previous transmission with the same ConRefNumber and UKPRN.' AS ErrorMessage

					UNION ALL SELECT 'Duplicate_01' AS RuleName, 'This record is a duplicate.' AS ErrorMessage
			) AS NewRecords
		  )
		AS Source([RuleName], [ErrorMessage])
			ON Target.[RuleName] = Source.[RuleName]
		WHEN MATCHED 
				AND EXISTS 
					(		SELECT Target.[RuleName]
								  ,Target.[ErrorMessage]
						EXCEPT 
							SELECT Source.[RuleName]
								  ,Source.[ErrorMessage]
					)
			  THEN UPDATE SET Target.[RuleName] = Source.[RuleName],
							  Target.[ErrorMessage] = Source.[ErrorMessage]
		WHEN NOT MATCHED BY TARGET THEN INSERT([RuleName], [ErrorMessage]) 
									   VALUES ([RuleName], [ErrorMessage])
		WHEN NOT MATCHED BY SOURCE THEN DELETE
		OUTPUT Inserted.[ValidationErrorMessageId],$action INTO @SummaryOfChanges_Collection([ValidationErrorMessageId],[Action])
	;

		DECLARE @AddCount_C INT, @UpdateCount_C INT, @DeleteCount_C INT
		SET @AddCount_C  = ISNULL((SELECT Count(*) FROM @SummaryOfChanges_Collection WHERE [Action] = 'Insert' GROUP BY Action),0);
		SET @UpdateCount_C = ISNULL((SELECT Count(*) FROM @SummaryOfChanges_Collection WHERE [Action] = 'Update' GROUP BY Action),0);
		SET @DeleteCount_C = ISNULL((SELECT Count(*) FROM @SummaryOfChanges_Collection WHERE [Action] = 'Delete' GROUP BY Action),0);

		RAISERROR('		         %s - Added %i - Update %i - Delete %i',10,1,'        Collection', @AddCount_C, @UpdateCount_C, @DeleteCount_C) WITH NOWAIT;
END