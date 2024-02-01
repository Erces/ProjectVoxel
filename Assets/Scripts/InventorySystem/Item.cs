using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public enum RARITY { COMMON,RARE,LEGENDARY}
    public RARITY itemRarity;
    public bool isWeapon;





}
