using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkBehaviour {

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
}
