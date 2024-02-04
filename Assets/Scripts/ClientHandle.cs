using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet){
        string _msg = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.i.myId = _myID;
        CliendSend.WelcomeReceived();
    }
}
