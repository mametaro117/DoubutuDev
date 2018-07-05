using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenechenge : MonoBehaviour {

    private bool isPlay = false;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChengeScene(int scenenum)
    {
        Debug.Log("シーン遷移");
        if (!isPlay)
            StartCoroutine(ChengeSceneCol(scenenum));
    }

    IEnumerator ChengeSceneCol(int scenenum)
    {
        Debug.Log("Chenge中");
        isPlay = true;
        FadeManager.Instance.LoadScene(scenenum, 1f);
        yield return new WaitForSeconds(1f);
        isPlay = false;
        yield break;
    }
}
