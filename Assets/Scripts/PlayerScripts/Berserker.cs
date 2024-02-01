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

        //Create a ray from the Mouse position into the scene
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Use this ray to Raycast against the mathematical floor plane
        // "enter" will be a float holding the distance from the camera 
        // to the point where the ray hit the plane
        if (plane.Raycast(ray, out var enter))
        {
            //Get the 3D world point where the ray hit the plane
            var hitPoint = ray.GetPoint(enter);

            // project the player position onto the plane so you get the position
            // only in XZ and can directly compare it to the mouse ray hit
            // without any difference in the Y axis
            var playerPositionOnPlane = plane.ClosestPointOnPlane(this.transform.position);

            // now there are multiple options but you could simply rotate the player so it faces 
            // the same direction as the one from the playerPositionOnPlane -> hitPoint 
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
