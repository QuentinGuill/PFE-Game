using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;

public class PlayfabAuthPanelViewRegister : MonoBehaviour
{
    [Header("Login View")]
    [SerializeField] protected InputField inputFieldUsername = null;
    [SerializeField] protected InputField inputFieldEmail = null;
    [SerializeField] protected InputField inputFieldPassword = null;
    [SerializeField] protected Text outputError = null;

    public void OnRegisterButtonClicked()
    {
        this.TryRegister();
    }

    private void TryRegister()
    {
        // Check setup
        if (this.inputFieldUsername == null || this.inputFieldEmail == null || this.inputFieldPassword == null)
            return;

        // Get input
        string username = this.inputFieldUsername.text;
        string email = this.inputFieldEmail.text;
        string password = this.inputFieldPassword.text;

        // Check input
        if (string.IsNullOrWhiteSpace(email) == false && string.IsNullOrWhiteSpace(password) == false)
        {
            var request = new RegisterPlayFabUserRequest { Email = email, Password = password, Username = username };

            PlayFabClientAPI.RegisterPlayFabUser(request, this.OnRegisterSuccess, this.OnRegisterError);
        }
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        string id = result.PlayFabId;
        PlayerPrefs.GetString("id", id);
        SceneManager.LoadScene("Game");
    }

    private void OnRegisterError(PlayFabError error)
    {
        // Log
        Debug.LogError("PlayfabAuthPanelViewRegister.OnRegisterError() - " + error);
        outputError.text = error.ToString();
    }
}