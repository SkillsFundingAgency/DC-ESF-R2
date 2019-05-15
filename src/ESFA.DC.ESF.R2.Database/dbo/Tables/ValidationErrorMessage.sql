CREATE TABLE [dbo].[ValidationErrorMessage]
(
	[ValidationErrorMessageId]		INT				IDENTITY (1, 1) NOT NULL,
	[RuleName]						VARCHAR (100),
	[ErrorMessage]					VARCHAR (MAX),
    CONSTRAINT [PK_ValidationErrorMessage] PRIMARY KEY CLUSTERED ([ValidationErrorMessageId] ASC)
)
