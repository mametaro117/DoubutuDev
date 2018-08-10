using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMater : MonoBehaviour {

	[SerializeField]
	private GameObject TowerBer;

	private float DefaultTowerHP;

	private float value;

	// Use this for initialization
	void Start () {
		DefaultTowerHP = GetComponent<Status> ().HitPoint;
		value = 750 / DefaultTowerHP;
	}
	
	// Update is called once per frame
	void Update () {
		TowerBer.GetComponent<RectTransform> ().sizeDelta = new Vector2 (value * GetComponent<Status> ().HitPoint, TowerBer.GetComponent<RectTransform> ().sizeDelta.y);
	}
}
