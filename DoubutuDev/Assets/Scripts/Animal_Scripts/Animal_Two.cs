using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Two : MonoBehaviour
{
    private Vector3 offset;
    private Vector2 tmp;
    private Vector2 _orgPosition = Vector2.zero;
    private GameObject _hitObject;
    private bool ani = false;
    public void OnMouseDown()
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        _orgPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }
    public void OnMouseDrag()
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPosition;
    }
    //マウスでDropしたときの処理
    public void OnMouseUp()
    {
        //aniがtrueのときpositionを更新
        if (ani == true)
        {
            var hitPos = _hitObject.transform.position;
            hitPos.z = 0;
            transform.position = hitPos;
        }
        else //aniがfalseの時位置を戻す(コライダーに入る前)
        {
            transform.position = _orgPosition;
        }
        //aniがfalseの時位置を戻す(コライダーから出たとき)
        if (ani == false)
        {
            transform.position = tmp;
        }
    }
    //コライダーに入ったときaniをtrueにする
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Animal_Box"))
        {
            ani = true;
            _hitObject = col.gameObject;
        }
    }
    //コライダーから出たときaniをfalseにする
    public void OnTriggerExit2D(Collider2D collision)
    {
        ani = false;
        _hitObject = null;
    }
    void Start()
    {
        tmp = transform.position;
    }
}
