﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient,Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully and is now player {_fromClient}");
            if(_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player {_username} ID: {_fromClient}");
            }
        }
        public static void UDPTestReceived(int _fromClient,Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine("Received packet via UDP, contains message: " + _msg);
        }
    }
}
