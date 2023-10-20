using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to the server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to the server.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined the lobby.");

        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true}, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("A player has entered the room.");

        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

        player.GetComponent<PhotonView>().Owner.NickName = "Player " + Random.Range(1, 100);
    }

}
