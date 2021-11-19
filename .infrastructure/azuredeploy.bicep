param location string = resourceGroup().location
param sqlServerName string
param sqlAzureAdLogin string
param sqlAzureAdPrincipalType string
param sqlAzureAdPrincipalId string
param sqlDatabaseName string
param storageAccountName string
param blobContainerName string
param appServicePlanName string
param webAppName string

resource sqlServer 'Microsoft.Sql/servers@2021-05-01-preview' = {
  name: sqlServerName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    administratorLogin: 'sysadm'
    administrators: {
      administratorType: 'ActiveDirectory'
      azureADOnlyAuthentication: true
      login: sqlAzureAdLogin
      principalType: sqlAzureAdPrincipalType
      sid: sqlAzureAdPrincipalId
      tenantId: subscription().tenantId
    }
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Disabled'
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

  resource sqlDatabaseStaging 'databases' = {
    name: '{sqlDatabaseName}-staging'
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
    allowBlobPublicAccess: false
    allowSharedKeyAccess: true
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
  }

  resource blobServices 'blobServices' = {
    name: 'default'

    resource blobContainer 'containers' = {
      name: blobContainerName
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
  kind: 'app,linux'
  identity: {
    type: 'SystemAssigned'
  }
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
    }
  }

  resource appSettings 'config' = {
    name: 'appsettings'
    properties: {}
  }
}

resource stagingDeploymentSlot 'Microsoft.Web/sites/slots@2021-02-01' = {
  name: '${webAppName}/staging'
  location: location
  kind: 'app,linux'
  identity: {
    type: 'SystemAssigned'
  }
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
    }
  }

  resource appSettings 'config' = {
    name: 'appsettings'
    properties: {}
  }
}

output webAppManagedIdentity string = webApp.identity.principalId
output webAppStagingManagedIdentity string = stagingDeploymentSlot.identity.principalId
