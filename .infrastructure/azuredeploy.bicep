param location string = resourceGroup().location
param sqlServerName string
param sqlAzureAdLogin string
param sqlAzureAdPrincipalType string = 'Group'
param sqlAzureAdPrincipalId string
param sqlDatabaseName string
param storageAccountName string
param blobContainerName string = 'images'
param appServicePlanName string
param webAppName string
param containerRegistryName string

resource sqlServer 'Microsoft.Sql/servers@2021-05-01-preview' = {
  name: sqlServerName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administrators: {
      administratorType: 'ActiveDirectory'
      azureADOnlyAuthentication: true
      login: sqlAzureAdLogin
      principalType: sqlAzureAdPrincipalType
      sid: sqlAzureAdPrincipalId
      tenantId: subscription().tenantId
    }
    minimalTlsVersion: '1.2'
  }

  resource firewallRules 'firewallRules' = {
    name: 'AllowAllWindowsAzureIps'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }

  resource sqlDatabase 'databases' = {
    name: sqlDatabaseName
    location: location
    sku: {
      name: 'GP_S_Gen5_1'
    }
    properties: {}
  }
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: storageAccountName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  sku: {
    name: 'Standard_GRS'
  }
  kind: 'StorageV2'
  properties: {
    allowBlobPublicAccess: true
    allowSharedKeyAccess: false
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
  }

  resource blobServices 'blobServices' = {
    name: 'default'

    resource blobContainer 'containers' = {
      name: blobContainerName
      properties: {
        publicAccess: 'Blob'
      }
    }
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  kind: 'linux'
  sku: {
    name: 'P1V3'
  }
  properties: {
    elasticScaleEnabled: false
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2021-02-01' = {
  name: webAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  kind: 'app,linux'
  properties: {
    httpsOnly: true
    reserved: true
    serverFarmId: appServicePlan.id
  }

  resource web 'config' = {
    name: 'web'
    properties: {
      ftpsState: 'Disabled'
      http20Enabled: true
      minTlsVersion: '1.2'
      scmMinTlsVersion: '1.2'
      netFrameworkVersion: 'v6.0'
    }
  }

  resource appSettings 'config' = {
    name: 'appsettings'
    properties: {}
  }
}

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2021-06-01-preview' = {
  name: containerRegistryName
  location: location
  sku: {
    name: 'Basic'
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    adminUserEnabled: false
    anonymousPullEnabled: false
  }
}

resource webAppTocontainerRegistryRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-08-01-preview' = {
  name: guid(webApp.id, containerRegistry.id)
  scope: containerRegistry
  properties: {
    principalId: webApp.identity.principalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d')
  }
}
