using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public bool isConnected = false;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        this.isConnected = true;
        PhotonNetwork.JoinLobby();
        Debug.Log("has connected to master");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError(cause);
    }



    public override void OnJoinedLobby()
    {
        Debug.Log("has joined lobby");
    }
}
