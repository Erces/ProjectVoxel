using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{

    public Pet equippedPet = null;

    void Start()
    {
        if(!PlayerPrefs.HasKey("PetID")){
                    PlayerPrefs.SetInt("PetID",0);

        }
    }
    void EquipPet(Pet _pet)
    {
        if(equippedPet){
            RemovePet();
            EquipPet(_pet);
        }
        PlayerPrefs.SetInt("PetID",_pet.petID);
        
        
    }
    void RemovePet()
    {
        equippedPet = null;
    }
}
