using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;

public class PlayfabAuthPanelViewLogin : MonoBehaviour
{
    [Header("Login View")]
    [SerializeField] protected InputField inputFieldEmail = null;
    [SerializeField] protected InputField inputFieldPassword = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
            this.TryLogin();
    }

    public void OnLoginButtonClicked()
    {
        this.TryLogin();
    }

    private void TryLogin()
    {
        // Check setup
        if (this.inputFieldEmail == null || this.inputFieldPassword == null)
            return;

        // Get input
        string email = this.inputFieldEmail.text;
        string password = this.inputFieldPassword.text;

        // Check input
        if (string.IsNullOrWhiteSpace(email) == false && string.IsNullOrWhiteSpace(password) == false)
        {
            var request = new LoginWithEmailAddressRequest { Email = email, Password = password, InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetUserAccountInfo = true } };

            PlayFabClientAPI.LoginWithEmailAddress(request, this.OnLoginSuccess, this.OnLoginError);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = result.InfoResultPayload.AccountInfo.Username };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, this.OnDisplayNameSuccess, this.OnDisplayNameError);
        string id = result.PlayFabId;
        PlayerPrefs.GetString("id", id);
        SceneManager.LoadScene("Game");
    }

    private void OnLoginError(PlayFabError error)
    {

    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {

    }

    private void OnDisplayNameError(PlayFabError error)
    {

    }
}