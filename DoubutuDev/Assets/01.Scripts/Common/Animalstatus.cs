﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animalstatus : MonoBehaviour {

    private int StateNum = 0;
    [SerializeField]
    private int NowStateNum = 0;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        //Debug.Log("Start");
        StartCoroutine(Depoly());
	}
	
	// Update is called once per frame
	void Update () {
        CheckState();
        Move();
    }

    void CheckState()
    {
        if(NowStateNum != StateNum)
        {
            //Debug.Log("ChechState");
            NowStateNum = StateNum;
            switch (NowStateNum)
            {
                case 0:
                    animator.SetInteger("State", 0);
                    break;
                case 1:
                    animator.SetInteger("State", 1);
                    break;
                case 2:
                    animator.SetInteger("State", 2);
                    break;
                default:
                    animator.SetInteger("State", 0);
                    break;
            }
        }
    }

    void Move()
    {
        switch (NowStateNum)
        {
            case 1:
                Vector3 v;
                v = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
                transform.position = v;
                break;
            case 0:
            case 2:
            default:
                break;
        }

        //if (NowStateNum == 1)
        //{
        //    //  ローカル変数の宣言
        //    Vector3 v;
        //    v = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        //    transform.position = v;
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");

        if (collision.tag == "Enemy")
        {
            StateNum = 2;
        }
        else
        {
            StateNum = 1;
        }
    }


    IEnumerator Depoly()
    {
        //Debug.Log("コルーチン１");
        yield return new WaitForSeconds(2.0f);
        StateNum = 1;
        //Debug.Log("コルーチン２");
        yield return new WaitForSeconds(2.0f);        
        //Debug.Log("コルーチン３");
        yield break;
    }    
}
