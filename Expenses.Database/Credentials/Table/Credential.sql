CREATE TABLE [Expenses].[Credential] (
	[Id]				UNIQUEIDENTIFIER	NOT NULL	DEFAULT		NEWID(),
	[UserId]			UNIQUEIDENTIFIER	NOT NULL,
	[CreatedByUserId]	UNIQUEIDENTIFIER	NOT NULL,
	[CredentialTypeId]	UNIQUEIDENTIFIER	NOT NULL,
	[Salt]				VARCHAR(255)		NOT NULL,
	[Hash]				VARCHAR(255)		NOT NULL,
	[CreatedOn]			DATETIMEOFFSET		NOT NULL	CONSTRAINT	[DF_Expenses.CreatedOn]	DEFAULT	SYSDATETIMEOFFSET(),
	[ExpiresOn]			DATETIMEOFFSET		NULL,

	CONSTRAINT [PK_Expenses.Credential]
		PRIMARY KEY CLUSTERED	([Id]),
	CONSTRAINT [FK_Expenses.Credential_Expenses.User_User]
		FOREIGN KEY	([UserId])	REFERENCES [Expenses].[User] ([Id]),
	CONSTRAINT [FK_Expenses.Credential_Expenses.User_CreatedBy]
		FOREIGN KEY	([CreatedByUserId])	REFERENCES [Expenses].[User] ([Id]),
	CONSTRAINT [FK_Expenses.Credential_Expenses.Credential.Type]
		FOREIGN KEY	([CredentialTypeId])	REFERENCES [Expenses].[Credential.Type] ([Id]),
)
GO