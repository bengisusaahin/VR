using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviourPunCallbacks
{
    GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Servera bağlanıldı");
        Debug.Log("Lobiye bağlanılıyor");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobiye bağlanıldı");
        Debug.Log("Odaya bağlanılıyor");
        PhotonNetwork.JoinOrCreateRoom("Odaismi", new RoomOptions { MaxPlayers = 5, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Odaya bağlanıldı");
        Debug.Log("Karakter oluşturuluyor...");
        if (Change.manSelected) 
        {
            myPlayer =(GameObject) PhotonNetwork.Instantiate("man", new Vector3(4, 0, 4), Quaternion.identity, 0, null);

        }
        else
        {
            myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector3(4, 0, 4), Quaternion.identity, 0, null);


        }
        myPlayer.GetComponent<PlayerWalk>().enabled = true;
        myPlayer.transform.Find("Head").gameObject.SetActive(true);
        myPlayer.transform.Find("playerName").gameObject.GetComponent<PlayFabManager>().enabled=true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
