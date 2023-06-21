using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTab : MonoBehaviour
{
    public GameObject cLogin;
    public GameObject cRegister;

    public void Login()
    {
        cLogin.SetActive(true);
        cRegister.SetActive(false);
    }

    public void Register()
    {
        cLogin.SetActive(false);
        cRegister.SetActive(true);
    }
}
