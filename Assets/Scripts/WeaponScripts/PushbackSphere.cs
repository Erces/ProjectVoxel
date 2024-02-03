using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushbackSphere : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Enemy>().enemySituation != Enemy.Situation.Pushback)
        {
            
            other.GetComponent<Enemy>().Pushback(transform.position,20,1);
        }
    }
}
