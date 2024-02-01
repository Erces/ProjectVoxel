using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Photon.Pun;

public class Slime : Enemy
{
    public MMF_Player mmfFound;
    public bool mmfFoundPlayed;
    public bool specialMovement;
    public float attackRange;
    public float rotateSpeed;
    public enum Mode { Searching, Found, Triggered, NullWait }
    public Mode slimeMode;
    Vector3 lockedPos;
    public Transform lockedPlayer;
    private bool dashStarted = false;
   
    public override void Update()
    {
        base.Update();
        HandleSlime();
    }
    public void HandleSlime()
    {
        if(enemySituation == Situation.Pushback || !PhotonNetwork.IsMasterClient)
        {
            return;
        }
        closestPlayer = FindClosestPlayer();
        if(Mathf.Abs(Vector3.Distance(transform.position,closestPlayer.transform.position)) < triggerRange){
            var rotation = Quaternion.LookRotation(closestPlayer.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            lockedPlayer = closestPlayer;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position,closestPlayer.transform.position)) < triggerRange && slimeMode != Mode.Triggered)
            slimeMode = Mode.Found;
        switch (slimeMode)
        {
            case Mode.Searching:
                agent.speed = 0;
                agent.ResetPath();
                break;
            case Mode.Found:
            agent.isStopped = false;
            agent.SetDestination(Player.i.transform.position);
                lockedToPlayer = true;
                agent.speed = 4;
                if (canAttack && Mathf.Abs(Vector3.Distance(transform.position,closestPlayer.transform.position)) < attackRange)
                {
                        //mmfFound.PlayFeedbacks();
                        canAttack = false;
                        
                        agent.ResetPath();
                        agent.isStopped = true;
                        mmfFoundPlayed = true;
                        lockedPos = lockedPlayer.position;
                        StartCoroutine("Dash");
                } 
                break;
            case Mode.Triggered:
                agent.isStopped = false;

                agent.speed = 18;
                agent.SetDestination(lockedPos);
                var rotation = Quaternion.LookRotation(lockedPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
                StartCoroutine("Revert");
                break;
            case Mode.NullWait:               
                break;
        }
    }

    
    public IEnumerator Dash()
    {
        if(dashStarted)
        yield break;
        Debug.Log(EULog.excBlue + " Dash Started");
        dashStarted = true;
        slimeMode = Mode.NullWait;
        yield return new WaitForSeconds(0.65f);

        slimeMode = Mode.Triggered;
        mmfFoundPlayed = false;

    }

    public IEnumerator Revert()
    {
        yield return new WaitForSeconds(3);
        attackTimer = attackTime;
        if (slimeMode == Mode.Triggered)
            slimeMode = Mode.Found;
            
        dashStarted = false;
        
        //CancelInvoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canAttack && slimeMode == Mode.Triggered)
        {
            CancelInvoke();
            slimeMode = Mode.Searching;
            attackTimer = attackTime;
            canAttack = false;
            other.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 5f);
        }
    }
}
