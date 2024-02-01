using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]

public class WeaponItem : Item
{
    public enum WeaponType {STR,DEX,INT}
    public WeaponType type;
    public Weapon weaponID;
    public float damage;
    public bool isTwoHanded;
}
