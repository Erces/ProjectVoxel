using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PetSpawner : MonoBehaviour
{
    public PhotonView view;

    void OnEnable()
    {
        EUActions.OnEnemiesPlaced += SpawnPet;
    }
    void OnDisable()
    {
        EUActions.OnEnemiesPlaced -= SpawnPet;
    }
    // Update is called once per frame
    private void SpawnPet(){
        Debug.Log(EULog.star + "Spawn Pet Function");
        if (!view.IsMine)
            return;
            Debug.Log(EULog.star + "Spawning pet");
        var pet = PhotonNetwork.Instantiate("Pets/Pet"+ PlayerPrefs.GetInt("PetID"), transform.position, transform.rotation);
            pet.GetComponent<PetMovementAI>().target = Player.i.transform;
            pet.GetComponent<Pet>().ownerPlayer = Player.i.view;
    }
}
