using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;


public enum PlayerType { VR = 0, Mobile = 1}

public class NetworkManagerAsymmetric : NetworkLobbyManager
{
    public NetworkPlayerSetup[] playerTypes;

    public Dictionary<int, int> currentPlayers;

    //public int count;

    void Start()
    {
        currentPlayers = new Dictionary<int, int>();
    }

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers.Add(conn.connectionId, (int)playerTypes[0].type);

        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public void SetPlayerTypeLobby(NetworkConnection conn, PlayerType _type)
    {
        if (currentPlayers == null)
        {
            currentPlayers = new Dictionary<int, int>();
        }

        Debug.Log("Setting player ... : " + _type);
        if (currentPlayers.ContainsKey(conn.connectionId))
        {
            Debug.Log("P Connection : " + conn.connectionId);
            currentPlayers[conn.connectionId] = (int)_type;
        }
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        if (sceneName == lobbyScene)
        {
            Debug.LogWarning("Lobby Scene loaded");

            //count = 0;
        }
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("CreateGamePlayer : " + conn.connectionId + " : " + playerControllerId);
        int type = currentPlayers[conn.connectionId];

        var startPosition = GetStartPosition();
        var prefab = GetNetworkPlayer(type);
        GameObject _temp = (GameObject)GameObject.Instantiate(prefab, startPosition.position, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);

        return _temp;
    }

    public GameObject GetNetworkPlayer(int type)
    {
        foreach (var setup in playerTypes)
        {
            if ((int)setup.type == type)
            {
                Debug.Log("GetNetworkPlayer : " + setup.type);
                return setup.prefab;
            }
        }

        Debug.LogError("No Player Setup Found for : [" + type + "]");
        return null;
    }
}

[System.Serializable]
public class NetworkPlayerSetup
{
    public PlayerType type;
    public GameObject prefab;

}
