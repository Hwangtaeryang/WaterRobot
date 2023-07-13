using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDistance : MonoBehaviour
{
    public Transform target;


    void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z - 6f);
        transform.LookAt(target);
    }


    void Update()
    {
        
    }
}
