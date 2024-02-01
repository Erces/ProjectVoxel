using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    //Singleton
    public static Inventory i;
    
    

    // 5 Slot inventory
    public List<Item> inventory = new List<Item>(5);



    private void Start()
    {
        i = this;
    }
// Adds an item to inventory

    public void AddItem(Item _item)
    {
        inventory.Add(_item);
    }

    public void AddWeapon(Item _weapon){
        if(_weapon == null)
        Debug.Log("Weapon null!");
        WeaponItem weapon = (WeaponItem)_weapon;
        WeaponManager.i.EquipWeapon(weapon,true);
    }

    public void RemoveItem(Item _item){
        inventory.Remove(_item);
    }
}
