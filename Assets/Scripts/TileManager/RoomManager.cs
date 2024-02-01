using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using ExitGames.Client.Photon.StructWrapping;
public class RoomManager : MonoBehaviour
{
    [HideInInspector] public static RoomManager i;
    [Header("ChapterSettings")]
    public Chapter chapter;
    public Room currentRoom;
    public List<Room> allRooms;
    public Room combatRoom;
    public Transform tileTransform;
    public Transform trashTransform;
    private int tileIndex = 0;
    private void Awake()
    {
        if(i != null){
            Debug.Log(EULog.exc + " RoomManager Error");
        }
        i = this;
    }

    
    private void OnEnable()
    {
        EUActions.OnNextRoom += LoadNextRoom;
    }
    private void OnDisable()
    {
        EUActions.OnNextRoom -= LoadNextRoom;
    }
    void Start()
    {
        //LoadNextRoom();
        SpawnRoom();
    }

    public void SpawnRoom()
    {
        if(PhotonNetwork.IsMasterClient){
            var room = PhotonNetwork.Instantiate("Tiles/RoomCombat",transform.position,transform.rotation);
            currentRoom = room.GetComponent<Room>();
            room.GetComponent<Room>().Activate();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DestroyCurrentRoom();
            Debug.Log(EULog.star + " Current Tile Name => " + allRooms[tileIndex].name);
            var go = PhotonNetwork.Instantiate("Tiles/" + allRooms[tileIndex].name, tileTransform.position, tileTransform.rotation);
            currentRoom = go.GetComponent<Room>();
            tileIndex++;
            currentRoom.GetComponent<Room>().Activate();
            EUActions.OnNextTileSpawned?.Invoke();
        }
    }

    public void DestroyCurrentRoom()
    {
        if(currentRoom)
        PhotonNetwork.Destroy(currentRoom.gameObject);
    }
}
