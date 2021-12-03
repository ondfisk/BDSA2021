[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [String]
    $Server,

    [Parameter(Mandatory = $true)]
    [String]
    $Database,

    [Parameter(Mandatory = $true)]
    [String]
    $Identity
)

begin {}

process {
    $accessToken = (Get-AzAccessToken -ResourceUrl "https://database.windows.net").Token
    $app = (az rest --method GET --uri "https://graph.microsoft.com/v1.0/servicePrincipals?`$filter=displayName+eq+'$Identity'") | ConvertFrom-Json
    $appId = $app.value.appId

    $query = @"
        DECLARE @sid UNIQUEIDENTIFIER = '$appId'
        IF EXISTS (
            SELECT * FROM sys.database_principals
            WHERE [name] = '$Identity' AND [type] = 'E AND [sid] != @sid'
        )
        BEGIN
            DROP USER [$Identity]
        END
        IF NOT EXISTS (
            SELECT * FROM sys.database_principals
            WHERE [name] = '$Identity' AND [type] = 'E'
        )
        BEGIN
            CREATE USER [$Identity] FROM EXTERNAL PROVIDER
        END

        IF NOT EXISTS (
            SELECT p.name, o.name FROM sys.database_role_members AS r
            JOIN sys.database_principals AS p ON r.member_principal_id = p.principal_id
            JOIN sys.sysusers AS o ON r.role_principal_id = o.uid
            WHERE p.name = '$Identity' AND o.name = 'db_owner'
        )
        BEGIN
            ALTER ROLE db_owner ADD MEMBER [$Identity]
        END
"@
    Invoke-Sqlcmd -ServerInstance "$Server.database.windows.net" `
        -Database $Database `
        -Query $query `
        -AccessToken $accessToken
}

end {}
