CREATE TABLE [Expenses].[User]	(
	[Id]		UNIQUEIDENTIFIER	NOT NULL	DEFAULT		NEWID(),
	[RoleId]	UNIQUEIDENTIFIER	NOT NULL,
	[Username]	VARCHAR(255)		NOT NULL,
	[Email]		VARCHAR(255)		NOT NULL,

	CONSTRAINT [PK_Expenses.User]
		PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [UQ_Expenses.User]
		UNIQUE CLUSTERED ([Username], [Email]),
	CONSTRAINT [FK_Expenses.User_Expenses.Role]
		FOREIGN KEY ([RoleId]) REFERENCES [Expenses].[Role]([Id])
)
GO