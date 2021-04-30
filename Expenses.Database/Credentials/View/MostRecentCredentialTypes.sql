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
	WHERE cred.[ExpiredOn] IS NULL
		OR cred.[ExpiredOn] >= SYSDATETIMEOFFSET()
	GROUP BY cred.[UserId], cred.[CredentialTypeId]
) latest;