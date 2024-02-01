using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Weapon", order = 1)]

public class EUWeapon : ScriptableObject
{
    public string weaponName;
    public string description;
    public GameObject vfxOnHit;
    public enum TYPE { STR,DEX,INT}
    public TYPE weaponType;
    public float baseDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
