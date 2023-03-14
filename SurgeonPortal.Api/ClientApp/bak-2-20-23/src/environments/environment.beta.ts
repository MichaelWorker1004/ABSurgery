declare const require: any;

const env = {
  agGridLicenseKey:
    'CompanyName=Kultera,LicensedApplication=VGIS,LicenseType=SingleApplication,LicensedConcurrentDeveloperCount=4,LicensedProductionInstancesCount=1,AssetReference=AG-010023,ExpiryDate=25_August_2021_[v2]_MTYyOTg0NjAwMDAwMA==c6f544da8b4ccc18839aff2738767197',
  apiBaseUrl: '//artotxvgis02.vzh.ent.verizon.com/OdnTracker.Api',
  appTitle: 'ODN Tracker',
  bypassLogin: true,
  environmentStatus: 'BETA',
  production: true,
  supportUrl: '',
  vzCommonApiBaseUrl: '//artotxvgis02.vzh.ent.verizon.com/Verizon.Common.Api',
  vzCommonAuthConfig: 'odn',
};

export const environment = Object.freeze(env);
