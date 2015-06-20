using UnityEngine;

public class RandomMatchmaker : Photon.MonoBehaviour
{
    private PhotonView myPhotonView;
    Color col;
    string PlayerName ="";
    bool inGame = false;
    // Use this for initialization
   public void Connect(Color Col)
    {
        col = Col;
        PhotonNetwork.ConnectUsingSettings("0.1");
        inGame = true;
    }
    void Start()
    {
      //  PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
        
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
       
    }

    void OnJoinedRoom()
    {
        myPhotonView=GameManager.addPlayer("test", col).GetComponent<PhotonView>();
  
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        if (!inGame)
        {
            PlayerName = GUI.TextField(new Rect(Screen.width / 2.0f - 100, Screen.height / 2.0f, 200, 20), PlayerName, 25);
        }
        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

            if (shoutMarco && GUILayout.Button("Marco!"))
            {
                myPhotonView.RPC("Marco", PhotonTargets.All);
            }
            if (!shoutMarco && GUILayout.Button("Polo!"))
            {
                myPhotonView.RPC("Polo", PhotonTargets.All);
            }
        }
    }
}
