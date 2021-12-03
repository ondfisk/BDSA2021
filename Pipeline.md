# Continuous Deployment with GitHub Actions

## Prerequisites

1. Azure AD Service Principal (SPN).

    ```bash
    az login
    az ad sp create-for-rbac --role Owner --sdk-auth
    ```

    Output format:

    ```json
    {
        "clientId": "...",
        "clientSecret": "...",
        "subscriptionId": "...",
        "tenantId": "...",
        "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
        "resourceManagerEndpointUrl": "https://management.azure.com/",
        "activeDirectoryGraphResourceId": "https://graph.windows.net/",
        "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
        "galleryEndpointUrl": "https://gallery.azure.com/",
        "managementEndpointUrl": "https://management.core.windows.net/"
    }
    ```

    Create GitHub secret `AZURE_CREDENTIALS` in `Production` environment.

1. Create Azure AD Group: `SQL Administrators` - record `Object ID`.
1. Make SPN member of SQL Administrators group.
1. Grant SPN `Microsoft.Graph/Directory.Read.All`

## Steps

Connection String: `"Server:{serverURL}; Authentication=Active Directory Managed Identity; Initial Catalog={db};"`
