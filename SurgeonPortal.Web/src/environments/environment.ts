declare const require: any;

const env = {
  agGridLicenseKey: 'CompanyName=Kultera,LicensedApplication=VGIS,LicenseType=SingleApplication,LicensedConcurrentDeveloperCount=4,LicensedProductionInstancesCount=1,AssetReference=AG-010023,ExpiryDate=25_August_2021_[v2]_MTYyOTg0NjAwMDAwMA==c6f544da8b4ccc18839aff2738767197',
  apiBaseUrl: 'http://apps.kultera.com/Rest/OdnTracker.Api',
  appTitle: 'ODN Tracker',
  bypassLogin: true,
  environmentStatus: 'DEV',
  production: false,
  supportUrl: '',
  vzCommonApiBaseUrl: 'http://apps.kultera.com/Rest/Verizon.Common.Api',
  vzCommonAuthConfig: 'odn'
};

export const environment = Object.freeze(env);
