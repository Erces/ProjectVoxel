using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Berserker : Player
{
    
    public bool canLMB;
    public bool canRMB;
    public float lmbTimer;
    public float lmbTime;
    public float rmbTimer;
    public float rmbTime;
    public WeaponTrigger trigger =null;
    public float basicAttackAnimCount;

    private float zCam;
    private Quaternion targetRotation;
  

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine)
            return;
        HandleTick();
        if (Input.GetKeyDown(KeyCode.Mouse0) && canLMB)
        {
            BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canRMB)
        {
            SpecialAttack();
        }
        CameraFacing();
    }
    public void CameraFacing()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var enter))
        {
            var hitPoint = ray.GetPoint(enter);
            var playerPositionOnPlane = plane.ClosestPointOnPlane(this.transform.position);
            this.transform.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }
    }

    void BasicAttack()
    {
        lmbTimer = lmbTime;
        var rndm = Random.Range(0, basicAttackAnimCount);
        anim.SetTrigger("Attack" + rndm.ToString("0"));
        Debug.Log("Attack" + rndm.ToString("0"));
        canLMB = false;

    }

    void SpecialAttack()
    {
        rmbTimer = rmbTime;
        anim.SetTrigger("AttackSpecial");
        canRMB = false;
    }
    void HandleTick()
    {
        lmbTimer -= Time.deltaTime;
        rmbTimer -= Time.deltaTime;

        if (lmbTimer <= 0)
        {
            lmbTimer = 0;
            canLMB = true;
        }
        if (rmbTimer <= 0)
        {
            rmbTimer = 0 ;
            canRMB = true;
        }
    }
}
