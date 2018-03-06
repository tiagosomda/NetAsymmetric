using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkBehaviour {


    
    private void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            CmdSetPlayerType(PlayerType.Mobile);
            StartCoroutine(FindReadyBtn());
        }
    }
    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer)
            return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdSetPlayerType(PlayerType.VR);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdSetPlayerType(PlayerType.Mobile);
        }
    }

    [Command]
    public void CmdSetPlayerType(PlayerType type)
    {
        var lobby = GameObject.FindObjectOfType<NetworkManagerAsymmetric>();
        lobby.SetPlayerTypeLobby(connectionToClient, type);
    }

    [Command]
    public void CmdPlayerIsReady()
    {
        var lobbyPlayer = GetComponent<NetworkLobbyPlayer>();
        lobbyPlayer.readyToBegin = true;
    }

    IEnumerator FindReadyBtn()
    {
        MobileNetManagerHelper mobileHelper = null;

        while (mobileHelper == null)
        {
            mobileHelper = GameObject.FindObjectOfType<MobileNetManagerHelper>();
            yield return new WaitForSeconds(0.5f);
        }

        mobileHelper.playerReadyBtn.onClick.AddListener(CmdPlayerIsReady);
    }
}
