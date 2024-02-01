using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnPointTile : MonoBehaviour
{

    private Room room;
    void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        return;
        room = RoomManager.i.currentRoom;
        var rndm = Random.Range(1f,2f);
        Invoke("SpawnTile", rndm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTile(){
        if(room.tileCount >= room.maxTileCount){
        EUActions.OnAllTilesSpawn?.Invoke();
        return;
        }
        var tile = PhotonNetwork.Instantiate("Tiles/" + room.tiles[0].tileID.ToString(),transform.position,transform.rotation);
        tile.GetComponent<Tile>().room = RoomManager.i.currentRoom;
        tile.transform.SetParent(RoomManager.i.currentRoom.transform);
        EUActions.OnTileSpawn?.Invoke();
        RoomManager.i.currentRoom.tiles.RemoveAt(0);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint")){
            Destroy(this.gameObject);
        }
    }
}
