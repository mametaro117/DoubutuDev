using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TowerMater : MonoBehaviour {

	[SerializeField]
	private GameObject TowerBer;

	private float DefaultTowerHP;

	private float value;

	void Start () {
		DefaultTowerHP = GetComponent<Status> ().HitPoint;
		value = 750 / DefaultTowerHP;
	}
	
	void Update () {
		TowerBer.GetComponent<RectTransform> ().sizeDelta = new Vector2 (value * GetComponent<Status> ().HitPoint, TowerBer.GetComponent<RectTransform> ().sizeDelta.y);
	}

    //  攻撃を受けた時のメーターのアニメーション
    public void TowerDamaged(){
        Vector3 vec3 = TowerBer.transform.parent.transform.position;
        TowerBer.transform.parent.transform.DOShakePosition(0.25f, 10, 25);
        TowerBer.transform.parent.transform.position = vec3;
    }
}
