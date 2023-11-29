using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class launcher : MonoBehaviourPunCallbacks
{
    public static launcher instance;
    public GameObject LoadingScene;
    public TMP_Text loadingtext;

    public GameObject CreateRoomSCreen;
    public TMP_InputField roomNameInput;

    public GameObject createdRoomScreen;
    public TMP_Text roomNameText;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        LoadingScene.SetActive(true);
        loadingtext.text = "Connecting to Server....";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        loadingtext.text = "Joining Lobby";
    }

    public override void OnJoinedLobby()
    {
        LoadingScene.SetActive(false);
    }

    public void OpenCreateRoomScreen()
    {
        CreateRoomSCreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            PhotonNetwork.CreateRoom(roomNameInput.text);

            LoadingScene.SetActive(true) ;
            loadingtext.text = " Creating Room"; 

        }
    }


    public override void OnCreatedRoom()
    {
       LoadingScene.SetActive(false) ;
        createdRoomScreen.SetActive(true) ;
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }


    public void LeaveRoom()
    {
        createdRoomScreen.SetActive(false);
        LoadingScene.SetActive(true ) ;
        loadingtext.text = " Leaving Room....";

        PhotonNetwork.LeaveRoom();
    }
}