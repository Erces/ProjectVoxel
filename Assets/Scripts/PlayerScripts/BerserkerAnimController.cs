using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerAnimController : MonoBehaviour
{

    public Berserker berserker;
    public void Hit()
    {
        //berserker.trigger.col.enabled = true;

    }
    public void Attack0Start()
    {
        //berserker.controller.movementLock = true;
    }
    public void Attack0End()
    {
       // berserker.controller.movementLock = false;
        berserker.trigger.col.enabled = false;


    }
    public void SpecialStart()
    {
       // berserker.controller.movementLock = true;


    }
    public void SpecialEnd()
    {
       // berserker.controller.movementLock = false;
        berserker.trigger.col.enabled = false;


    }
}
