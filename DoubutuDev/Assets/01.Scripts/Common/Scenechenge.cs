using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenechenge : MonoBehaviour
{
    private bool isPlay = false;
    public void ChengeScene(int scenenum)
    {
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
