# Continuous Deployment with GitHub Actions

## Prerequisites

1. Azure AD Service Principal (SPN).

    ```bash
    az login
    az ad sp create-for-rbac --role Owner --sdk-auth
    ```

    Create GitHub secret `AZURE_CREDENTIALS` in `Production` environment.

1. Make SPN `owner` of Azure Resource Group.
1. Create Azure AD Group: `SQL Administrators` - record `Object ID`.
1. Make SPN member of SQL Administrators group.

## Steps
