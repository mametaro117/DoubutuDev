using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectedAnimalScript : MonoBehaviour {
    
    private GameObject _selectedAnimal;                 // 武器選択中の動物
    private Animator _select_Animator;                  // Animator
    private Coroutine cor_AnimRoop;                     // ループ用コルーチン

    [SerializeField]
    private GameObject[] _RabbitPrefabs = new GameObject[4];
    [SerializeField]
    private GameObject[] _OwlPrefabs = new GameObject[4];
    [SerializeField]
    private GameObject[] _ZouPrefabs = new GameObject[4];
    [SerializeField]
    private GameObject[] _PenguinPrefabs = new GameObject[4];

    [SerializeField]
    private GameObject spawnPoint;

    public void ShowAnimal(GameObject box)              // 武器選択中の動物を表示する
    {
        if(!GetComponent<WindowChangeScript>().Equip_Changing && 
            GetComponent<WindowChangeScript>().WindowStationary)
        {
            // _selectedAnimalに表示する動物のPrefabを入れる
            int boxNum = int.Parse(box.name.Substring(box.name.Length - 1));
            WindowChangeScript param = GetComponent<WindowChangeScript>();
            int animalNum = param.AnimalAndWeaponList[boxNum - 1, 0];
            switch(animalNum)
            {
                case 0:
                    _selectedAnimal = Instantiate(_RabbitPrefabs[Random.Range(0, 4)]) as GameObject;
                    break;
                case 1:
                    _selectedAnimal = Instantiate(_OwlPrefabs[Random.Range(0, 4)]) as GameObject;
                    break;
                case 2:
                    _selectedAnimal = Instantiate(_ZouPrefabs[Random.Range(0, 4)]) as GameObject;
                    break;
                case 3:
                    _selectedAnimal = Instantiate(_PenguinPrefabs[Random.Range(0, 4)]) as GameObject;
                    break;
            }

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

            // 表示する時の体裁
            _selectedAnimal.transform.SetParent(spawnPoint.transform);
            Debug.Log(_selectedAnimal.transform.parent);
            _selectedAnimal.transform.localPosition = new Vector3(0f, 0f, 0f);

            // アニメーターのコルーチンを起動
            if (_selectedAnimal.GetComponent<Animator>() != null)
            {
                _select_Animator = _selectedAnimal.GetComponent<Animator>();
                _select_Animator.SetTrigger("Attack");
                cor_AnimRoop = StartCoroutine(Cor_Anim());
            }
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

    public void DestroyAnimal()     // 武器選択終了時に動物を削除する
    {
        Destroy(_selectedAnimal);
        _selectedAnimal = null;
        if(cor_AnimRoop != null)
        {
            StopCoroutine(cor_AnimRoop);
            cor_AnimRoop = null;
        }
    }
}
