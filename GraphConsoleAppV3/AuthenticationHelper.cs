using System;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;

namespace GraphConsoleAppV3
{
    internal class AuthenticationHelper
    {
        public static string TokenForUser;
        public static string TokenForApplication;

        /// <summary>
        /// Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsApplication()
        {
            Uri servicePointUri = new Uri(GlobalConstants.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, AppModeConstants.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForApplication());
            return activeDirectoryClient;
        }

        /// <summary>
        /// Async task to acquire token for Application.
        /// </summary>
        /// <returns>Async Token for application.</returns>
        public static async Task<string> AcquireTokenAsyncForApplication()
        {
            return await GetTokenForApplication();
        }

        /// <summary>
        /// Get Token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        public static async Task<string> GetTokenForApplication()
        {
            if (TokenForApplication == null)
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(AppModeConstants.AuthString, false);
                // Config for OAuth client credentials 
                ClientCredential clientCred = new ClientCredential(AppModeConstants.ClientId,
                    AppModeConstants.ClientSecret);
                AuthenticationResult authenticationResult =
                    await authenticationContext.AcquireTokenAsync(GlobalConstants.ResourceUrl,
                        clientCred);
                TokenForApplication = authenticationResult.AccessToken;
            }
            return TokenForApplication;
        }

        /// <summary>
        /// Get Active Directory Client for User.
        /// </summary>
        /// <returns>ActiveDirectoryClient for User.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsUser()
        {
            Uri servicePointUri = new Uri(GlobalConstants.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, UserModeConstants.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForUser());
            return activeDirectoryClient;
        }

        /// <summary>
        /// Async task to acquire token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> AcquireTokenAsyncForUser()
        {
            return await GetTokenForUser();
        }

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> GetTokenForUser()
        {
            if (TokenForUser == null)
            {
                string AuthString = "https://login.microsoftonline.com/";
                string ResourceUrl = "https://graph.windows.net";
                string ClientId = "***";
                var redirectUri = new Uri("https://localhost");
                string  TenantId = "e4162ad0-e9e3-4a16-bf40-0d8a906a06d4";
                AuthenticationContext authenticationContext = new AuthenticationContext(AuthString, false);
                AuthenticationResult userAuthnResult = await authenticationContext.AcquireTokenAsync(ResourceUrl,
                    ClientId, redirectUri, new PlatformParameters(PromptBehavior.RefreshSession));
                TokenForUser = userAuthnResult.AccessToken;
                var client = new HttpClient();
                var uri = $"https://graph.windows.net/{TenantId}/users?api-version=1.6";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", TokenForUser);
                var response = await client.GetAsync(uri);
                if (response.Content != null)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }

                Console.WriteLine("\n Welcome " + userAuthnResult.UserInfo.GivenName + " " +
                                              userAuthnResult.UserInfo.FamilyName);
            }
            return TokenForUser;
        }

    }
}
