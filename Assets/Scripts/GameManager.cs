using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

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
    public void SpawnPlayer(int _id,string _username,Vector3 _position,Quaternion _rotation)
    {
        GameObject _player;
        if(_id == Client.i.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;

        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
}
