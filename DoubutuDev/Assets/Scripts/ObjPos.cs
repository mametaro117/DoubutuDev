using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPos : MonoBehaviour
{
    public RaycastHit2D hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);
            Debug.Log(hit.collider);
        }
    }
}
