using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI code;
    public bool isHost = true;

    private void Start()
    {
        code.text = GetNewCode();
    }

    public void OnlinePlay()
    {
        if (isHost)
            CreateRoom();
        else
            JoinRoom();
    }

    private void CreateRoom()
    {
        PhotonNetwork.CreateRoom(code.text);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputField.text);
    }

    private string GetNewCode()
    {
        string codeValue = "";
        for (int i = 0; i < 8; i++)
        {
            char c = (char)Random.Range(65, 90);
            codeValue += c;
        }

        return codeValue;
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            //PhotonNetwork.LoadLevel("OnlineMatch");
            Debug.Log("Ready to match");
        }
        else
        {
            Debug.Log("Wainting for connection");
        }
    }
}
