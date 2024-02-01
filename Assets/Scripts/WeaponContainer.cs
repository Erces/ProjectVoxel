using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    public enum Container{INVLEFT,INVRIGHT,GROUND}
    public Container ContainerType;
    public TMP_Text weaponName;
    public TMP_Text weaponType;
    public TMP_Text weaponDamage;
    


    public void ManageVariables(WeaponItem weapon){
        if(ContainerType == Container.GROUND){
            Debug.Log("GROUND");
        weaponName.text = weapon.itemName;
        weaponType.text = weapon.type.ToString();
        weaponDamage.text = weapon.damage.ToString("0");
        }
        else if(ContainerType == Container.INVLEFT){
            if(!WeaponManager.i.leftHandWeapon)
            return;
        WeaponItem weaponLeft = WeaponManager.i.leftHandWeapon;
        PaintVariables(weaponLeft.itemName,weaponLeft.type.ToString(),weaponLeft.damage.ToString("0"));
        }
        else{
            if(!WeaponManager.i.rightHandWeapon)
            return;
        WeaponItem weaponRight = WeaponManager.i.rightHandWeapon;
        PaintVariables(weaponRight.itemName,weaponRight.type.ToString(),weaponRight.damage.ToString("0"));
        }

        
        
    }
    void PaintVariables(string name,string type,string damage){
        weaponName.text = name;
        weaponType.text = type;
        weaponDamage.text = damage;
    }
}
