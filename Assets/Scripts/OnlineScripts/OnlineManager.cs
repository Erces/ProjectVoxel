using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OnlineManager : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        view = GetComponent<PhotonView>();
        EUActions.OnNextTileSpawned += TeleportPlayersToNextTile;
        SpawnPlayersOnLogin();
    }


    //Spawns its own player on Login
    void SpawnPlayersOnLogin()
    {
        switch (PlayerPrefs.GetInt("Character"))
        {
            case 0:
                PhotonNetwork.Instantiate("Characters/PlayerArcher", spawnPoint.position, Quaternion.identity, 0);
                break;
            case 1:
                PhotonNetwork.Instantiate("Characters/PlayerDwarf", spawnPoint.position, Quaternion.identity, 0);

                break;
            case 2:
                PhotonNetwork.Instantiate("Characters/PlayerMage", spawnPoint.position, Quaternion.identity, 0);

                break;
            default:
                PhotonNetwork.Instantiate("Characters/PlayerDwarf", spawnPoint.position, Quaternion.identity, 0);
                break;
        }
        EUActions.OnGameStart?.Invoke();



    }
    public void TeleportPlayersToNextTile()
    {
        view.RPC("TeleportPlayersToNextTileRPC", RpcTarget.All);
    }
    [PunRPC]
    public void TeleportPlayersToNextTileRPC()
    {
        foreach (var item in GameManager.i.playerList)
        {
            if(item.view.IsMine)
            item.transform.position = RoomManager.i.currentRoom.playerSpawnPoint.position;
        }
    }
}

