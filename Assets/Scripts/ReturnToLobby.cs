using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLobby : MonoBehaviour
{
    public void returnToLobby()
    {
        SceneManager.LoadScene("Game");
    }
}
