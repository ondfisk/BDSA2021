#!/bin/sh

RESOURCE_GROUP_NAME=$1
SQL_SERVER_NAME=$2

MANAGED_IDENTITY=$(az sql server show --resource-group $RESOURCE_GROUP_NAME --name $SQL_SERVER_NAME --query identity.principalId --output tsv)
MICROSOFT_GRAPH=$(az ad sp list --filter "displayName eq 'Microsoft Graph'" --query "[].objectId" --output tsv)
APP_ROLE_ID=$(az ad sp list --filter "displayName eq 'Microsoft Graph'" --query "[].appRoles[?value=='Directory.Read.All'].id" --output tsv)
CURRENT=$(az rest --method GET --uri "https://graph.microsoft.com/v1.0/servicePrincipals/$MANAGED_IDENTITY/appRoleAssignments" --query "value[?appRoleId=='$APP_ROLE_ID'].id" --output tsv)

if [ -z $CURRENT ]; then
    echo "::warning:: SQL Server must be granted Microsoft Graph/Directory.Read.All to continue"
    echo "::warning:: Execute the following in a Cloud Shell:"
    echo "::warning:: az rest --method POST --uri 'https://graph.microsoft.com/v1.0/servicePrincipals/$MANAGED_IDENTITY/appRoleAssignments' --body '{\"principalId\":\"$MANAGED_IDENTITY\",\"resourceId\":\"$MICROSOFT_GRAPH\",\"appRoleId\":\"$APP_ROLE_ID\"}'"
    exit 1
fi
