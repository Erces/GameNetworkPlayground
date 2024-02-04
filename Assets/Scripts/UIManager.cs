using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject usernameField;
    public TMP_InputField input;
    public static UIManager i;
    
    void OnEnable()
    {
        i = this;
        Actions.OnPlayerConnect += ConnectEvent;
    }
    void OnDisable()
    {
        Actions.OnPlayerConnect -= ConnectEvent;
    }

    void ConnectEvent(){
        startMenu.SetActive(false);
        usernameField.SetActive(false);
    }
}
