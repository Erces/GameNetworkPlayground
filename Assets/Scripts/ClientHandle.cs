using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet){
        string _msg = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.i.myId = _myID;
        ClientSend.WelcomeReceived();

        Client.i.udp.Connect(((IPEndPoint)Client.i.tcp.socket.Client.LocalEndPoint).Port);
    }
    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log("Received packet via UDP. Message: " + _msg);
        ClientSend.UDPTestReceived();
    }
}
