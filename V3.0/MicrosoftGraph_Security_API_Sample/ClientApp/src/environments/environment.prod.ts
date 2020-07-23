import { IEnvironment } from './environment.interface';

export const environment: IEnvironment = {
  production: false,
  baseUrl: 'https://kloudsecure.azurewebsites.net/api',
  MSAL: {
    clientID: '28535f1e-7b9d-4a93-9be2-e11d2d6412f0',
    redirectUri: 'https://kloudsecure.azurewebsites.net',
    cacheLocation: 'localStorage',
    piiLoggingEnabled: true,
    authority: 'https://login.microsoftonline.com/common',
    validateAuthority: true,
    protectedResourceMap: [['https://kloudsecure.azurewebsites.net/api', ['28535f1e-7b9d-4a93-9be2-e11d2d6412f0']]]
  }
};
