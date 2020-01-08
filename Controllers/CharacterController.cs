using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        [HttpGet]
        public async Task<Character> Get()
        {
            var rng = new Random();
            var dbUserName = await GetKey("dbUserName");
            var dbUserPassword = await GetKey("dbUserPassword");
            var heroes = new List<Hero>();
            using (var db = new DatabaseContext(dbUserName, dbUserPassword))
            {
                heroes = db.Hero.ToList();
            }
            var highestLevelHero = heroes.OrderByDescending(x => x.Level).First();
            return new Character()
            {
                Name = highestLevelHero.Name,
                Level = highestLevelHero.Level,
                Gear = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Axe", Damage = rng.Next(35, 50), Defence = 0
                    },
                    new Item()
                    {
                        Name = "Shield", Damage = 0, Defence = rng.Next(10, 30)
                    }
                }
            };
        }

        //TODO: Getting the db credentials should be done somewhere else, refactor this.
        private async Task<string> GetKey(string name)
        {
            try
            {
                // This is the ID which can be found as "Application (client) ID" when selecting the registered app under "Azure Active Directory" -> "App registrations".
                const string APP_CLIENT_ID = "22bc5a4a-93de-4e95-b0cf-11a4c6225631";

                // This is the client secret from the app registration process.
                const string APP_CLIENT_SECRET = "bNy50Pm?B8@?Xdn_t7N@fZSGpby5I5[t";

                // Use the client SDK to get access to the key vault. To authenticate we use the identity app we registered and
                // use the client ID and the client secret to make our claim.
                var kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                    async (string authority, string resource, string scope) => {
                        var authContext = new AuthenticationContext(authority);
                        var credential = new ClientCredential(APP_CLIENT_ID, APP_CLIENT_SECRET);
                        AuthenticationResult result = await authContext.AcquireTokenAsync(resource, credential);
                        if (result == null)
                        {
                            throw new InvalidOperationException("Failed to retrieve JWT token");
                        }
                        return result.AccessToken;
                    }
                ));
                // Calling GetSecretAsync will trigger the authentication code above and eventually
                // retrieve the secret which we can then read.
                var secretBundle = await kvc.GetSecretAsync("https://tuomaskeyvault.vault.azure.net/", name);
                return secretBundle.Value;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
        }
    }
}
