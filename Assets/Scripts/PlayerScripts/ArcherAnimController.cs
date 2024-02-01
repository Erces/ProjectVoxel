using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimController : MonoBehaviour
{
    public Archer archer;

    public void ShootStart()
    {
        archer.canLMB = false;
    }
    public void ShootEnd()
    {
        archer.canLMB = true;

    }
    public void Shoot()
    {
        archer.mmf.PlayFeedbacks();
        archer.ShootArrow();
    }
    public void LegHit()
    {
        archer.legHitCol.enabled = true;
        archer.mmfRMB.PlayFeedbacks();
    }
    public void LegHitEnd()
    {
        archer.legHitCol.enabled = false;
        
    }
}
