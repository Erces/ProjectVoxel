﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeWeapon()
    {
        GetComponentInParent<WpnHolder>().DoChangeWeapon();
    }
}
