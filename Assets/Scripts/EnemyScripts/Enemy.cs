using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;
using UnityEngine.AI;
using MoreMountains.Feedbacks;
using System;
public class Enemy : MonoBehaviour
{
    public enum Type { Basic,Boss,Custom,Slime}
    public Type enemyType;

    public enum Situation { Moving,Pushback,Stop,Slow}
    public Situation enemySituation;

    public PhotonView view;
    
    [Header("Attributes")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    public float speed;
    [Header("Bools")]
    public bool canAttack;
    public bool foundPlayer;
     public bool lockedToPlayer;

    [Header("Timers")]
    public float attackTime;
    public float attackTimer;
    public float pushTimer = .3f;
    public float stopTimer;
    [Header("Range")]
    public float triggerRange;
   [HideInInspector] public Animator anim = null;
   [HideInInspector]  public MeshRenderer renderer;
   [HideInInspector]  public NavMeshAgent agent;
   [HideInInspector] public Rigidbody rb;

    
    public Vector3 pushVelocity;
    
    
    public MMF_Player mmf;

  [HideInInspector]  public Transform closestPlayer = null;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        attackTimer = attackTime;
        currentHp = maxHp;
        EnemiesManager.i.enemies.Add(this);
        InvokeRepeating("LookForPlayer",.2f,.2f);
    }
    void HandleTick()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            attackTimer = 0;
            canAttack = true;
        }
    }

    void LookForPlayer(){
        if(Mathf.Abs(Vector3.Distance(this.transform.position,FindClosestPlayer().position))< triggerRange)
        foundPlayer = true;

    }
    public Transform FindClosestPlayer()
    {
        float minDistance = Mathf.Infinity;
        foreach (var player in GameManager.i.playerList)
        {
            float distance = Mathf.Abs(Vector3.Distance(agent.transform.position, player.transform.position));
            if (distance < minDistance && !lockedToPlayer)
            {
                Debug.Log("Found player");
                closestPlayer = player.transform;
                minDistance = distance;
            }
        }
        return closestPlayer;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        HandleTick();

    }
    [PunRPC]
    public void TakeDamage(float _damage)
    {
        if(mmf)
        mmf.PlayFeedbacks();
        DamageTextController.CreateFloatingText(_damage.ToString("0"),transform);
        currentHp -= _damage;
        Debug.Log("Damage taken enemy side");
        
        
        
        if (currentHp <= 0)
        {
            Die();
            
        }
           
    }

    public void Die()
    {
        EUActions.OnEnemyDeath?.Invoke(this);
        EnemiesManager.i.enemies.Remove(this);
        Destroy(gameObject);
    }

    [PunRPC]
    public void Pushback(Vector3 pos)
    {
        pushTimer = .06f;
        enemySituation = Situation.Pushback;
        Debug.Log("Pushback");
        pushVelocity = this.transform.position - pos;
        pushVelocity *= 5;
        agent.velocity = pushVelocity;
       
        //rb.AddForce(pushVelocity, ForceMode.Impulse);
    }
    [PunRPC]
    public void Pushback(Vector3 pos,float power,float time)
    {
        pushTimer = time;
        enemySituation = Situation.Pushback;
        Debug.Log("Pushback");
        pushVelocity = this.transform.position - pos;
        pushVelocity *= power;
        //agent.speed = 5;
        agent.isStopped = false;
        agent.velocity = pushVelocity;

        //rb.AddForce(pushVelocity, ForceMode.Impulse);
    }
}
