using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliendSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.i.tcp.SendData(_packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.i.myId);
            _packet.Write("Welcome");

            SendTCPData(_packet);
        }
    }
}