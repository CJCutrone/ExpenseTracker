CREATE PROCEDURE [Expenses].[GenerateResetToken] (
	@generationType		VARCHAR(255),
	@createdByUserId	VARCHAR(255),
	@createdForUserId	VARCHAR(255)
)
AS
BEGIN
	BEGIN TRANSACTION
	
	BEGIN TRY
		
		DECLARE @creator TABLE (
			[Id]		UNIQUEIDENTIFIER,
			[RoleCode]	VARCHAR(255)
		);

		INSERT INTO @creator ([Id], [RoleCode])
		SELECT 
			u.[Id] as [Id],
			r.[Code] as [RoleCode]
		FROM [Expenses].[User] u
		LEFT JOIN [Expenses].[Role] r
			ON r.Id = u.RoleId
		WHERE u.[Id] = @createdByUserId;

		IF NOT EXISTS(
			SELECT 1 FROM [Expenses].[User] WHERE [Id] = @createdByUserId
		) RAISERROR ('Could not find createdByUserID', 1, 1);

		IF NOT EXISTS(
			SELECT 1 FROM [Expenses].[User] WHERE [Id] = @createdForUserId
		) RAISERROR ('Could not find createdForUserId', 1, 1);

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		RAISERROR ('Could not completed transaction', 1, 1);

	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END