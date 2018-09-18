using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

    [SerializeField]
    private GameObject text;
    private int clearTime = 150;
    [SerializeField]
    private GameObject animal;
    private GameObject _selectedAnimal;
    private Animator _select_Animator;

    // Use this for initialization
    void Start ()
    {
        //ここに追加処理

        text.GetComponent<Text>().text = clearTime.ToString() + "sec";
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

        // アニメーターのコルーチンを起動
        if (_selectedAnimal.GetComponent<Animator>() != null)
        {
            _select_Animator = _selectedAnimal.GetComponent<Animator>();
            _select_Animator.SetTrigger("Attack");
            StartCoroutine(Cor_Anim());
        }
    }

    IEnumerator Cor_Anim()          // コルーチンでアニメーションをループさせる
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
