﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient,Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.maxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAllExcept(int _exceptclient,Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.maxPlayers; i++)
            {
                if(i != _exceptclient)             
                Server.clients[i].tcp.SendData(_packet);
                
            }
        }

        public static void Welcome(int _toClient,string _msg)
        {
            using(Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }
    }
}
