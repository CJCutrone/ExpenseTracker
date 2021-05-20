CREATE VIEW [Expenses].[MostRecentCredentialTypes]
AS
SELECT 
	latest.[UserId],
	latest.[CredentialTypeId]
FROM (
	SELECT
		cred.[UserId],
		cred.[CredentialTypeId],
		MAX(cred.[CreatedOn]) [CreatedOn]
	FROM [Expenses].[Credential] cred
	WHERE cred.[ExpiresOn] IS NULL
		OR cred.[ExpiresOn] >= SYSDATETIMEOFFSET()
	GROUP BY cred.[UserId], cred.[CredentialTypeId]
) latest;