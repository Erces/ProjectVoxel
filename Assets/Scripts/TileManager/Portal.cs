using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Portal : MonoBehaviour
{
    
    
    void  OnTriggerStay(Collider other)
    {
        if(PhotonNetwork.IsMasterClient && Input.GetKeyDown(KeyCode.F)){
            RoomManager.i.DestroyCurrentRoom();
            RoomManager.i.SpawnRoom();
            PhotonNetwork.Destroy(this.gameObject);
        }
        
    }
}
