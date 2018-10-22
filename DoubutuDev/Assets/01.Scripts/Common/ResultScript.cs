using _System = System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour { //リザルト構成用

    //変数エリア
    [SerializeField]
    private Text clearTimeText;             //クリアタイムを表示するテキスト
    [SerializeField]
    private ChoiceParamator choiceparam;    //メインゲームから情報を持ってくる
    private float clearTime;                //クリアタイム
    private GameObject animal;              //一番使った動物
    private GameObject _selectedAnimal;     //リザルトで実際に表示する動物
    private Animator _select_Animator;      //リザルトで動物のアニメーションをさせるアニメーター

    void Start ()
    {
        //メインゲームから情報を持ってくる
        choiceparam = FindObjectOfType(typeof(ChoiceParamator)) as ChoiceParamator;
        clearTime = (float)_System.Math.Round(choiceparam.ClearTime, 0);
        animal = choiceparam.FavoAnimal;

        //クリアタイムを表示
        clearTimeText.text = clearTime.ToString() + "sec";

        //一番使った動物を表示
        _selectedAnimal = Instantiate(animal) as GameObject;

        // 不必要な物を削除していく
        Animalstate select_AnimalState = _selectedAnimal.GetComponent<Animalstate>();
        if (select_AnimalState != null)
        {
            select_AnimalState.enabled = !select_AnimalState.enabled;
        }
        Status select_Status = _selectedAnimal.GetComponent<Status>();
        if (select_Status != null)
        {
            select_Status.enabled = !select_Status.enabled;
        }
        AnimalScript serect_Animalscript = _selectedAnimal.GetComponent<AnimalScript>();
        if (serect_Animalscript != null)
        {
            serect_Animalscript.enabled = !serect_Animalscript.enabled;
        }
        if (_selectedAnimal.transform.Find("HPBer") != null)
        {
            Destroy(_selectedAnimal.transform.Find("HPBer").gameObject);
        }
        if (_selectedAnimal.transform.Find("SkillBer") != null)
        {
            Destroy(_selectedAnimal.transform.Find("SkillBer").gameObject);
        }

        //場所指定
        _selectedAnimal.transform.position = new Vector3(0, -3.5f, 0);

        //アニメーターのコルーチンを起動
        if (_selectedAnimal.GetComponent<Animator>() != null)
        {
            _select_Animator = _selectedAnimal.GetComponent<Animator>();
            _select_Animator.SetTrigger("Attack");
            StartCoroutine(Cor_Anim());
        }
    }

    //コルーチンでアニメーションをループさせる
    IEnumerator Cor_Anim()          
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _select_Animator.SetTrigger("Walk");
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            _select_Animator.SetTrigger("Attack");
        }
    }
}

