using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMater : MonoBehaviour {

	[SerializeField]
	private GameObject TowerBer;

	private int DefaultTowerHP;

	private float value;

	// Use this for initialization
	void Start () {
		DefaultTowerHP = GetComponent<Totalstatus> ().HitPoint;
		value = 500 / DefaultTowerHP;
	}
	
	// Update is called once per frame
	void Update () {
		TowerBer.GetComponent<RectTransform> ().sizeDelta = new Vector2 (value * GetComponent<Totalstatus> ().HitPoint, TowerBer.GetComponent<RectTransform> ().sizeDelta.y);
	}
}
