using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRotation : MonoBehaviour
{
    public float moveSpeed = 2f;

    void Start()
    {
        
    }

    void Update()
    {

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }

    }
}


