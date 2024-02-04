using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    void Awake()
    {
        if(i == null){
            i = this;
        }
        else{
            
            Debug.Log("Instance already exist");
            Destroy(this);
        }
         
    }

    public void ConnectToServer(){
        Actions.OnPlayerConnect?.Invoke();
        Client.i.ConnectToServer();
    }
}
