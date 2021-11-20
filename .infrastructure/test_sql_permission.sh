#!/bin/sh

RESOURCE_GROUP_NAME=$1
SQL_SERVER_NAME=$2

MANAGED_IDENTITY=$(az sql server show --resource-group $RESOURCE_GROUP_NAME --name $SQL_SERVER_NAME --query identity.principalId --output tsv)
MICROSOFT_GRAPH=$(az rest --method GET --uri "https://graph.microsoft.com/v1.0/servicePrincipals?\$filter=displayName+eq+'Microsoft+Graph'" --query "value[].id" --output tsv)
APP_ROLE_ID=$(az rest --method GET --uri "https://graph.microsoft.com/v1.0/servicePrincipals?\$filter=displayName+eq+'Microsoft+Graph'" --query "value[].appRoles[?value=='Directory.Read.All'].id" --output tsv)
CURRENT=$(az rest --method GET --uri "https://graph.microsoft.com/v1.0/servicePrincipals/$MANAGED_IDENTITY/appRoleAssignments" --query "value[?appRoleId=='$APP_ROLE_ID'].id" --output tsv)

if [ -z $CURRENT ]; then
    echo "::warning:: SQL Server must be granted Microsoft Graph/Directory.Read.All to continue"
    echo "::warning:: Execute the following in a Cloud Shell:"
    set +B
    echo "::notice:: az rest --method POST --uri 'https://graph.microsoft.com/v1.0/servicePrincipals/$MANAGED_IDENTITY/appRoleAssignments' --body '{\"principalId\":\"$MANAGED_IDENTITY\",\"resourceId\":\"$MICROSOFT_GRAPH\",\"appRoleId\":\"$APP_ROLE_ID\"}'"
    set -B
    exit 1
fi
