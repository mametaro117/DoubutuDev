using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private CostScript costScript;

    [SerializeField]
    private GameObject CostErrorImage;
    [SerializeField]
    private GameObject ChoiceErrorImage;
    [SerializeField]
    private GameObject[] units = new GameObject[8];

    void Awake()
    {
        costScript = GameObject.FindObjectOfType<CostScript>();
    }
    public void ClickGround()
    {
        //  どうぶつボタンと武器ボタンが押されていれば
        if (costScript.IsSetCostValue())
        {
            //  コストが両方足りていたらTrue
            if (costScript.IsCreate())
            {
                //  コストの消費
                costScript.ConsumeAnimalCost();
                costScript.ConsumeWeaponCost();
                //  生成する場所の取得
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                //  インスタンス生成
                var obj = Instantiate(units[costScript.GetSelectNumber()], new Vector3(pos.x, pos.y, pos.z), units[costScript.GetSelectNumber()].transform.rotation);
                //  エフェクトの表示
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Magic, obj.transform.position, 1, obj);
                //  効果音再生
                AudioManager.Instance.PlaySe((int)AudioManager.SelistName.AnimalSpawn);
            }
            else
            {
                //  コストが足りない場合画面に表示
                StartCoroutine(FadeCostErrorImage());
                Debug.Log("コストが足りない");
            }
        }
        else
        {
            //  ボタンを選んでないよを表示
            StartCoroutine(FadeChoiceErrorImage());
            Debug.Log("ボタン選んでないよ");
        }
    }
    //  選ばれた動物編成で配列にセット
    public void SetAnimalsPrefab(GameObject[] _animals)
    {
        units = _animals;
    }

    private bool isFadeCostError = false;
    private bool isFadeChoiceError = false;

    IEnumerator FadeCostErrorImage()
    {
        if (!isFadeCostError)
        {
            isFadeCostError = true;
            CostErrorImage.SetActive(true);
            CostErrorImage.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i <= 20; i++)
            {
                CostErrorImage.GetComponent<CanvasGroup>().alpha = 1 - (0.04f * i);
                yield return new WaitForSeconds(0.04f);
            }
            CostErrorImage.GetComponent<CanvasGroup>().alpha = 0;
            CostErrorImage.SetActive(false);
            isFadeCostError = false;
        }
        yield break;
    }
    IEnumerator FadeChoiceErrorImage()
    {
        if (!isFadeChoiceError)
        {
            isFadeChoiceError = true;
            ChoiceErrorImage.SetActive(true);
            ChoiceErrorImage.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i <= 6; i++)
            {
                ChoiceErrorImage.GetComponent<CanvasGroup>().alpha = 1 - (0.167f * i);
                yield return new WaitForSeconds(0.05f);
            }
            ChoiceErrorImage.GetComponent<CanvasGroup>().alpha = 0;
            ChoiceErrorImage.SetActive(false);
            isFadeChoiceError = false;
        }
        yield break;
    }
}
