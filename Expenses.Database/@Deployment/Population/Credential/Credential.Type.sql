DECLARE @json VARCHAR(MAX) = 
'
[
  {
    "Code": "PASSWORD",
	"Name": "Password",
	"Description": "A password that allows a user to log in"
  },
  {
    "Code": "RESETTOKEN",
	"Name": "Reset Token",
	"Description": "A reset token that allows a user to reset their password"
  }
]
';
WITH SourceData AS (
	SELECT 
		*
	FROM OPENJSON(@json, '$')
	WITH (
		[Code]			VARCHAR(64) '$.Code',
		[Name]			VARCHAR(64) '$.Name',
		[Description]	VARCHAR(255) '$.Description'
	)
)

MERGE [Expenses].[Credential.Type] AS target
USING SourceData source
	ON target.Code = source.Code
WHEN NOT MATCHED BY TARGET THEN
INSERT 
	(
		[Code],
		[Name],
		[Description]
	) 
	VALUES 
	(
		[Code],
		[Name],
		[Description]
	)
;
GO