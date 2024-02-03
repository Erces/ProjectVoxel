using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 offset;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.i.transform.position + offset;
    }
}
