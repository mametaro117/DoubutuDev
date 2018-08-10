using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectedAnimalScript : MonoBehaviour {

    [SerializeField]
    private GameObject Rabbit;
    [SerializeField]
    private GameObject Hukurou;
    [SerializeField]
    private GameObject Zou;
    [SerializeField]
    private GameObject SelectedAnimal;
    [SerializeField]
    private int AnimalNum;
    private Animalstate S_Animalstate;
    private Totalstatus S_Totalstatus;
    private AnimalScript S_Animalscript;
    [SerializeField]
    private GameObject Rabbit_Anim;
    [SerializeField]
    private CharacterController Zou_Anim;
    private CharacterController _Animation;
    private Animator _Animator;
    [SerializeField]
    private AnimatorStateInfo _anim;

    public void ShowAnimal(GameObject Box)
    {
        if(!GetComponent<WindowChangeScript>().Equip_Changing && GetComponent<WindowChangeScript>().WindowStationary)
        {
            int BoxNum = int.Parse(Box.name.Substring(Box.name.Length - 1));
            WindowChangeScript _Param = GetComponent<WindowChangeScript>();
            AnimalNum = _Param.AnimalAndWeaponList[BoxNum - 1, 0];
            switch (AnimalNum)
            {
                case 0:
                    SelectedAnimal = Instantiate(Rabbit) as GameObject;
                    //_Animation = 
                    break;
                case 1:
                    SelectedAnimal = Instantiate(Hukurou) as GameObject;
                    break;
                case 2:
                    SelectedAnimal = Instantiate(Zou) as GameObject;
                    break;
                default:
                    Debug.Log("未実装アニマルを選んだね？");
                    break;
            }
            S_Animalstate = SelectedAnimal.GetComponent<Animalstate>();
            if (S_Animalstate != null)
            {
                S_Animalstate.enabled = !S_Animalstate.enabled;
            }
            S_Totalstatus = SelectedAnimal.GetComponent<Totalstatus>();
            if (S_Totalstatus != null)
            {
                S_Totalstatus.enabled = !S_Totalstatus.enabled;
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
                _anim = _Animator.GetCurrentAnimatorStateInfo(0);
            }
        }
    }

    public void DestroyAnimal()
    {
        Destroy(SelectedAnimal);
        SelectedAnimal = null;
    }

    //重い気がするけど許して(改善する気がする)
    private void Update()
    {
        if(SelectedAnimal != null)
        {
            //_Animator.
        }
    }
}
