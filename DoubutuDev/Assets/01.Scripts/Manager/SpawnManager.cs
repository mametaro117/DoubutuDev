using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {


    public delegate float SpawnDelegate();
    public SpawnDelegate spawnDelegate;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject prefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ClickGround()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        GameObject obj = Instantiate(prefab, new Vector3(pos.x, pos.y, pos.z), prefab.transform.rotation);
        //obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log(pos);
    }

}
