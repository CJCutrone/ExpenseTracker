CREATE TABLE [Expenses].[Credential.Type] (
	[Id]			UNIQUEIDENTIFIER	NOT NULL	DEFAULT		NEWID(),
	[Name]			VARCHAR(64)			NOT NULL,
	[Code]			VARCHAR(64)			NOT NULL,
	[Description]	VARCHAR(255)		NOT NULL,

	CONSTRAINT [PK_Expenses.CredentialType]
		PRIMARY KEY	CLUSTERED ([Id]),
	CONSTRAINT [UQ_Expenses.CredentialType]
		UNIQUE ([Name], [Code])
)
GO