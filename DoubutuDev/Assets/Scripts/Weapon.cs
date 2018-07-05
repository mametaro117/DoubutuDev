using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Vector3 offset;
    private GameObject _hitObject;
    private Vector2 _orgPosition = Vector2.zero;
    private Vector2 tmp;
    private bool wea = false;
    public void OnMouseDown()
    {
        _orgPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }
    public void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPosition;
    }

    public void OnMouseUp()
    {
        if (wea == true)
        {
            var hitPos = _hitObject.transform.position;
            hitPos.z = 0;
            transform.position = hitPos;
        }
        else
        {
            transform.position = _orgPosition;
        }
        if (wea == false)
        {
            transform.position = tmp;
        }
    }
    /// <summary>
    /// 当たり判定エリアに入ったら呼び出される
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Weapon_Box"))
        {
            wea = true;
            _hitObject = col.gameObject;
        }
    }
    /// <summary>
    /// 当たり判定エリアから外に出たときに呼び出される
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        wea = false;
        _hitObject = null;
    }
    void Start()
    {
        tmp = transform.position;
    }
}