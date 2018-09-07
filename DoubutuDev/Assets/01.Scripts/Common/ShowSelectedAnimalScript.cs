using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectedAnimalScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] Animals = new GameObject[5];

    private GameObject SelectedAnimal;
    private int AnimalNum;
    private Animalstate S_Animalstate;
    private Status S_Status;
    private AnimalScript S_Animalscript;
    private CharacterController _Animation;
    private Animator _Animator;
    private AnimatorStateInfo _animinfo;
    Coroutine AnimRoop;

    public void ShowAnimal(GameObject Box)
    {
        if(!GetComponent<WindowChangeScript>().Equip_Changing && GetComponent<WindowChangeScript>().WindowStationary)
        {
            int BoxNum = int.Parse(Box.name.Substring(Box.name.Length - 1));
            WindowChangeScript _Param = GetComponent<WindowChangeScript>();
            AnimalNum = _Param.AnimalAndWeaponList[BoxNum - 1, 0];
            SelectedAnimal = Instantiate(Animals[AnimalNum]) as GameObject;
            S_Animalstate = SelectedAnimal.GetComponent<Animalstate>();
            if (S_Animalstate != null)
            {
                S_Animalstate.enabled = !S_Animalstate.enabled;
            }
            S_Status = SelectedAnimal.GetComponent<Status>();
            if (S_Status != null)
            {
                S_Status.enabled = !S_Status.enabled;
            }
            S_Animalscript = SelectedAnimal.GetComponent<AnimalScript>();
            if (S_Animalscript != null)
            {
                S_Animalscript.enabled = !S_Animalscript.enabled;
            }
            SelectedAnimal.transform.SetParent(GameObject.Find("WeaponSelectWindows2").transform);
            RectTransform _rect = SelectedAnimal.AddComponent<RectTransform>();
            _rect.anchoredPosition3D = new Vector3(300, -100, 0);
            SelectedAnimal.transform.localScale = new Vector3(100f, 100f, 100f);
            Debug.Log(SelectedAnimal.transform.Find("HPBer").gameObject);
            if (SelectedAnimal.transform.Find("HPBer").gameObject != null)
            {
                Destroy(SelectedAnimal.transform.Find("HPBer").gameObject);
            }
            if (SelectedAnimal.transform.Find("SkillBer").gameObject != null)
            {
                Destroy(SelectedAnimal.transform.Find("SkillBer").gameObject);
            }
            if (SelectedAnimal.GetComponent<Animator>() != null)
            {
                _Animator = SelectedAnimal.GetComponent<Animator>();
                Debug.Log(_Animator);
                _Animator.SetTrigger("Attack");
                _animinfo = _Animator.GetCurrentAnimatorStateInfo(0);
                Debug.Log("<color=red>" + _animinfo.length + "</color>");
                AnimRoop = StartCoroutine(Cor_Anim());
            }
        }
    }

    public void DestroyAnimal()
    {
        Destroy(SelectedAnimal);
        SelectedAnimal = null;
        StopCoroutine(AnimRoop);
    }

    //アニメーションうんたら
    IEnumerator Cor_Anim()
    {
        Debug.Log("Start");
        while(true)
        {
            yield return new WaitForSeconds(1f);
            _Animator.SetTrigger("Walk");
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            _Animator.SetTrigger("Attack");
            Debug.Log("<Color=green>RoopCor</color>");
        }
    }
}
