using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class Client : MonoBehaviour
{
    public static Client i;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 4545;
    public int myId = 0;
    public TCP tcp;

    public delegate void PacketHandler(Packet _packet);
    private static Dictionary<int,PacketHandler> packetHandlers;
    void Awake()
    {
        if(i == null){
            i = this;
        }
        else{
            Destroy(this);
        }
    }

    void Start()
    {
        tcp = new TCP();
        
        
    }

    public void ConnectToServer(){
        InitializeClientData();
        tcp.Connect();
    }
    public class TCP{
        public TcpClient socket;

        private Packet receivedData;
        private NetworkStream stream;
        
        private byte[] receiveBuffer;
        public void Connect(){
            socket = new TcpClient{
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(i.ip,i.port,ConnectCallback,socket);
        }

        private void ConnectCallback(IAsyncResult _result){
            socket.EndConnect(_result);
            if(!socket.Connected){
                return;
            }
            stream = socket.GetStream();
            receivedData = new Packet();
            stream.BeginRead(receiveBuffer,0,dataBufferSize,ReceiveCallback,null);
        }
        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log("Error " + _ex);
            }
        }
        private void ReceiveCallback(IAsyncResult _result){
            try
                {
                    int _byteLength = stream.EndRead(_result);
                    if(_byteLength <= 0)
                    {
                        return;
                    }
                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);
                    receivedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback,null);
                }
            catch{

            }
        }

        private bool HandleData(byte[] _data){
            int _packetLenght = 0;
            receivedData.SetBytes(_data);

            if(receivedData.UnreadLength() >= 4){
                _packetLenght = receivedData.ReadInt();
                if(_packetLenght <= 0){
                    return true;
                }
            }

            while(_packetLenght > 0 && _packetLenght <= receivedData.UnreadLength()){
                byte[] _packetBytes = receivedData.ReadBytes(_packetLenght);
                ThreadManager.ExecuteOnMainThread(() => 
                {
                    using(Packet _packet = new Packet(_packetBytes)){
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });

                _packetLenght = 0;
                  if(receivedData.UnreadLength() >= 4){
                _packetLenght = receivedData.ReadInt();
                if(_packetLenght <= 0){
                    return true;
                }
            }
            }
            if(_packetLenght <= 1){
                return true;
            }
            return false;

            
        }
        
    }
    private void InitializeClientData(){
            packetHandlers = new Dictionary<int, PacketHandler>(){
                {(int)ServerPackets.welcome,ClientHandle.Welcome}
            };
            Debug.Log("Initialized packets.");
        }
}
