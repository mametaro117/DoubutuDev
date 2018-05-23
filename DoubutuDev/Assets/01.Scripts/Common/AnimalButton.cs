using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButton : MonoBehaviour {

    private Button btn;

	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) Test();
	}

    void Test()
    {
        btn.onClick.AddListener(TapAnimalButton);
    }

    void TapAnimalButton()
    {
        Debug.Log("Tap");
    }
}
