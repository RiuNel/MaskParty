using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using Photon.Pun.Demo.Cockpit;

public class Networkmanager : MonoBehaviourPunCallbacks, IOnEventCallback
{

    // Start is called before the first frame update
    public Text[] NicknameTexts;
    int Players_num = 0;
    public string value1, value2;
    [Header("DisconnectPanel")]
    public GameObject DisconnectPanel;
    public InputField NicknameInput;
    PhotonView PV;
    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public GameObject InitGameBtn;
    public dice dice;
    public Mapmanager mapmanager;
    void Start()
    {
        Screen.SetResolution(540, 960, false);
    }

    public void Init()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2) return;
        InitGameBtn.SetActive(false);
        PV.RPC("InitGames", RpcTarget.AllViaServer);
    }
    [PunRPC]
    void InitGames()
    {
        print("Game Play");
        for(int i = 0; i< PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            NicknameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;
            
        }
        mapmanager.GetMapPosition();
    }
    public void Roll()
    {
        PV.RPC("RollRPC", RpcTarget.MasterClient);
    }
    void RollRPC()
    {
        StartCoroutine(RollCo());
    }
    IEnumerator RollCo()
    {
        yield return null;
    }
    public void OnPhotonSerializedView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(value1);
            stream.SendNext(value2);
        }
        else
        {
            value1 = (string)stream.ReceiveNext();
            value2 = (string)stream.ReceiveNext();
        }
    }
    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 },null);
    }
    public void ShowPanel(GameObject curPanel)
    {
        DisconnectPanel.SetActive(false);
        RoomPanel.SetActive(false);

        curPanel.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        ShowPanel(RoomPanel);
        if (Master()) { InitGameBtn.SetActive(true); }
        PhotonNetwork.Instantiate("Players" + (++Players_num).ToString(), Vector3.zero,Quaternion.identity);
    }
    public void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == 0)
        {
            object[] data = (object[])photonEvent.CustomData;
            for (int i = 0; i < data.Length; i++) print(data[i]);
        }
    }
    bool Master()
    {
        return PhotonNetwork.LocalPlayer.IsMasterClient;
    }
}
