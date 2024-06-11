using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Dungeon.Data;

namespace Dungeon.Managers.Menu
{
    public class AuthenticationManager : MonoBehaviour
    {
        async void Start()
        {
            await UnityServices.InitializeAsync();
            Debug.Log(UnityServices.State);
            SetupEvents();

            await SignInAnonymouslyAsync();
        }

        #region Events
        void SetupEvents()
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                // Shows how to get a playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

                // Shows how to get an access token
                Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

                //should use game manager action "SaveGameData" and change props get to private at game manager
                GameManager.Instance.PlayerId = AuthenticationService.Instance.PlayerId;
                GameManager.Instance.PlayerAccessToken = AuthenticationService.Instance.AccessToken;

            };

            AuthenticationService.Instance.SignInFailed += (err) =>
            {
                Debug.LogError(err);
            };

            AuthenticationService.Instance.SignedOut += () =>
            {
                Debug.Log("Player signed out.");
            };

            AuthenticationService.Instance.Expired += () =>
              {
                  Debug.Log("Player session could not be refreshed and expired.");
              };
        }
        #endregion

        public async Task SignInAnonymouslyAsync()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");

                //this is probably unsafe
                MenuManager menuManager = FindObjectOfType<MenuManager>();
                if (menuManager != null)
                {
                    menuManager.LogPlayerIn();
                }

                // Shows how to get the playerID
                //Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.Log(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

    }
}
