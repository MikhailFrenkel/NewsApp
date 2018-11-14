using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace NewsApp.Helpers
{
    public class AuthorizationService
    {
        private const string PasswordResetError = "AADB2C90118";
        private readonly PublicClientApplication _publicClientApplication;

        public string ApplicationId { get; }
        public string AuthorityUrl { get; }
        public string EditUrl { get; }
        public string ResetUrl { get; }
        public string RedirectUrl { get; }
        public string[] Scopes { get; }
        public UIParent UiParent { get; }

        public AuthorizationService(string appId, string authorityUrl, string editUrl, 
            string resetUrl, string redirectUrl, string[] scopes, UIParent uiParent)
        {
            ApplicationId = appId;
            AuthorityUrl = authorityUrl;
            EditUrl = editUrl;
            ResetUrl = resetUrl;
            RedirectUrl = redirectUrl;
            Scopes = scopes;
            UiParent = uiParent;
            _publicClientApplication = new PublicClientApplication(ApplicationId, AuthorityUrl)
            {
                RedirectUri = RedirectUrl
            };
        }

        public JObject ParseIdToken(string idToken)
        {
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        public async Task<AuthenticationResult> Login()
        {
            try
            {
                IEnumerable<IAccount> accounts = await _publicClientApplication.GetAccountsAsync();
                AuthenticationResult ar;
                if (accounts.ToList().Count == 0)
                {
                    ar = await _publicClientApplication.AcquireTokenAsync(Scopes, UiParent);
                }
                else
                {
                    var account = accounts.FirstOrDefault();
                    ar = await _publicClientApplication.AcquireTokenSilentAsync(Scopes, account);
                }

                return ar;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(PasswordResetError))
                {
                    return await PasswordReset();
                }

                throw;
            }
        }

        public async Task Logout()
        {
            IEnumerable<IAccount> accounts = await _publicClientApplication.GetAccountsAsync();

            foreach (var account in accounts)
            {
                await _publicClientApplication.RemoveAsync(account);
            }
        }

        public async Task<AuthenticationResult> SilentLogin()
        {
            IEnumerable<IAccount> accounts = await _publicClientApplication.GetAccountsAsync();
            return await _publicClientApplication.AcquireTokenSilentAsync(Scopes, accounts.FirstOrDefault());
        }

        private async Task<AuthenticationResult> PasswordReset()
        {
            return await _publicClientApplication.AcquireTokenAsync(Scopes, (IAccount)null,
                UIBehavior.SelectAccount, string.Empty, null, ResetUrl, UiParent);
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
