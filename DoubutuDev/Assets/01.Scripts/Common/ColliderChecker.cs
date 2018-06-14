using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderChecker : MonoBehaviour {

    float radius;
    Vector3 vec3;

    // Use this for initialization
    void Start () {
        if (GetComponent<CircleCollider2D>() != null)
            radius = GetComponent<CircleCollider2D>().radius;
        if (GetComponent<BoxCollider2D>() != null)
        {
            vec3 = GetComponent<BoxCollider2D>().size;
            vec3.x = vec3.x * gameObject.transform.lossyScale.x;
            vec3.y = vec3.y * gameObject.transform.lossyScale.y;
            vec3.z = vec3.z * gameObject.transform.lossyScale.z;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        if (GetComponent<CircleCollider2D>() != null)
        {
            Gizmos.DrawSphere(transform.position, radius * gameObject.transform.lossyScale.x);
        }
        else if (GetComponent<BoxCollider2D>() != null)
        {
            Gizmos.DrawCube(transform.position, vec3);
        }
    }
}
