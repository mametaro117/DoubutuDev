using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {

    [SerializeField]
    private GameObject DamageObject;

    #region Singleton

    private static DamageText instance;

    public static DamageText Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (DamageText)FindObjectOfType(typeof(DamageText));

                if (instance == null)
                {
                    Debug.LogError(typeof(DamageText) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
		
	}	

    public void DiplayText(Vector3 pos, float damage)
    {
        //  重なり順の変更
        pos.z = -5;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();

        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 80), Random.Range(100, 180)));
        StartCoroutine(DestryText(obj));
    }

    IEnumerator DestryText(GameObject destryObj)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(destryObj);
        yield break;
    }
}
