CREATE TABLE [Expenses].[Role] (
	[Id]		UNIQUEIDENTIFIER		NOT NULL		DEFAULT		NEWID(),
	[Code]		VARCHAR(64)				NOT NULL,
	CONSTRAINT [PK_Expenses.Role]
		PRIMARY KEY CLUSTERED ([Id])
)
GO