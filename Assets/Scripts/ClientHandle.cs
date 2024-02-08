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

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        System.Numerics.Vector3 position = _packet.ReadVector3();
        System.Numerics.Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.i.SpawnPlayer(_id, _username, position.ToUnity(), _rotation.ToUnityQuaternion());
    }
    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3().ToUnity();

        GameManager.players[_id].transform.position = _position;
    }
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion().ToUnityQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

}
public static class VectorExtensions
{
    public static UnityEngine.Vector2 ToUnity(this System.Numerics.Vector2 vec) => new UnityEngine.Vector2(vec.X, vec.Y);
    public static System.Numerics.Vector2 ToNumerics(this UnityEngine.Vector2 vec) => new System.Numerics.Vector2(vec.x, vec.y);

    public static UnityEngine.Vector3 ToUnity(this System.Numerics.Vector3 vec) => new UnityEngine.Vector3(vec.X, vec.Y, vec.Z);
    public static UnityEngine.Quaternion ToUnityQuaternion(this System.Numerics.Quaternion vec) => new UnityEngine.Quaternion(vec.X, vec.Y, vec.Z,vec.W);

    public static System.Numerics.Vector3 ToNumerics(this UnityEngine.Vector3 vec) => new System.Numerics.Vector3(vec.x, vec.y, vec.z);
    public static System.Numerics.Quaternion ToNumericsQuaternion(this UnityEngine.Quaternion vec) => new System.Numerics.Quaternion(vec.x, vec.y, vec.z,vec.w);

    // Add others as required.
}
