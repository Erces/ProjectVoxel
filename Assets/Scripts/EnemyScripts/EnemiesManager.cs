using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager i;
    public List<Enemy> enemies;
    private void Awake()
    {
        i = this;
    }
    void OnEnable()
    {
        EUActions.OnEnemyDeath += EnemyDied;
    }
    private void OnDisable()
    {
        EUActions.OnEnemyDeath -= EnemyDied;

    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        HandleEnemies();
        
    }

    void EnemyDied(Enemy _enemy)
    {
        enemies.Remove(_enemy);
        if(enemies.Count == 0)
        {

            Debug.Log(EULog.exc + "Enemy count = 0, tile cleared!");
            EUActions.OnTileCleared?.Invoke();

        }
    }
    void HandleEnemies()
    {
        
        foreach (var agent in enemies)
        {
            
            switch (agent.enemySituation)
            {
                case Enemy.Situation.Moving:
                    Walk(agent);
                    break;
                case Enemy.Situation.Pushback:
                    Pushback(agent);
                    break;
                case Enemy.Situation.Stop:
                    Stop(agent);
                    break;
                case Enemy.Situation.Slow:
                    break;    
                
            }

            

        }
    }
    public void Stop(Enemy agent){
        if(agent.stopTimer <= 0){
            agent.enemySituation = Enemy.Situation.Moving;
        }
        agent.stopTimer -= Time.deltaTime;
        agent.agent.speed = 0;
        agent.agent.ResetPath();
    }
    public void Walk(Enemy agent)
    {
        if (agent.enemyType != Enemy.Type.Basic || agent.enemySituation != Enemy.Situation.Moving || !agent.foundPlayer)
            return;

        if (agent != null)
        {
            Transform closestPlayer = null;
            float minDistance = Mathf.Infinity;
            foreach (var player in GameManager.i.playerList)
            {
                float distance = Mathf.Abs(Vector3.Distance(agent.transform.position, player.transform.position));
                if (distance < minDistance)
                {
                    closestPlayer = player.transform;
                    minDistance = distance;
                }
            }

            agent.agent.speed = agent.speed;
            agent.gameObject.GetComponent<NavMeshAgent>().SetDestination(closestPlayer.position);
            agent.anim.SetBool("Walk",true);

        }
    }
    public void Pushback(Enemy agent)
    {
        

        agent.pushTimer -= Time.deltaTime;
       
        if (agent.pushVelocity.magnitude < 0.1f)
        {
            agent.pushTimer = 0;
        }

        if (agent.pushTimer <= 0)
        {
            agent.agent.speed = 1;
            agent.agent.isStopped = false;
            agent.enemySituation = Enemy.Situation.Moving;

            
            agent.agent.speed = 1;

            agent.agent.updateRotation = true;
            agent.enemySituation = Enemy.Situation.Moving;

          
            agent.agent.angularSpeed = 999;
            agent.agent.acceleration = 100;
            agent.agent.speed = 2;

            return;
        }
    }
}
