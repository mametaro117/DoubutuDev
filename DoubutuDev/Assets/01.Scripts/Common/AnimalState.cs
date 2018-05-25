using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalState : MonoBehaviour {

    private int StateNum = 0;
    private Animator animator;
    

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        Debug.Log("Start");
        StartCoroutine(Depoly());
	}
	
	// Update is called once per frame
	void Update () {
        CheckState();
    }

    void CheckState()
    {
        //  ローカル変数の宣言
        Vector3 v;
        switch (StateNum)
        {
            case 0:
                animator.SetInteger("State", 0);
                v = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;
            case 1:
                animator.SetInteger("State", 1);
                v = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                break;
            case 2:
                animator.SetInteger("State", 2);
                v = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;
            default:
                animator.SetInteger("State", 0);
                v = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                break;
        }
        transform.position = v;
    }

    IEnumerator Depoly()
    {
        Debug.Log("コルーチン１");
        yield return new WaitForSeconds(1.0f);
        StateNum = 1;
        yield return new WaitForSeconds(1.0f);
        
        Debug.Log("コルーチン２");
        yield return null;
    }    
}
