﻿CREATE TABLE [dbo].[ValidationError] (
    [SourceFileId]               INT              NOT NULL,
    [ValidationError_Id]         INT              IDENTITY (1, 1) NOT NULL,
    [RowId]                      UNIQUEIDENTIFIER NULL,
    [RuleId]                     VARCHAR (50)     NULL,
    [ConRefNumber]               VARCHAR (MAX)    NULL,
    [DeliverableCode]            VARCHAR (MAX)    NULL,
    [CalendarYear]               VARCHAR (MAX)    NULL,
    [CalendarMonth]              VARCHAR (MAX)    NULL,
    [CostType]                   VARCHAR (MAX)    NULL,
    [ReferenceType]              VARCHAR (MAX)    NULL,
    [Reference]                  VARCHAR (MAX)    NULL,
    [StaffName]                  VARCHAR (MAX)    NULL,
    [ULN]                        VARCHAR (MAX)    NULL,
    [Severity]                   VARCHAR (2)      NULL,
    [ErrorMessage]               VARCHAR (MAX)    NULL,
    [ProviderSpecifiedReference] VARCHAR (MAX)    NULL,
    [Value]                      VARCHAR (MAX)    NULL,
    [LearnAimRef]                VARCHAR (MAX)    NULL,
    [SupplementaryDataPanelDate] VARCHAR (MAX)    NULL,
    [CreatedOn]                  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([SourceFileId] ASC, [ValidationError_Id] ASC)
);

