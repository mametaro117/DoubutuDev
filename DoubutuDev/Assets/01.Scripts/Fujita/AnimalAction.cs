using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAction : MonoBehaviour {

    private bool _isUp = true;
    private float _timer = 0;
    private Vector3 _startPos;

	// Use this for initialization
	void Start () {
        _startPos = GetComponent<RectTransform>().localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(_isUp)
        {
            UpStep();
        }
        else
        {
            DownStep();
        }
	}

    private void UpStep()
    {
        var pos = _startPos;
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _isUp = false;
            _timer -= 0.5f;
            pos.y += (20f - _timer * 20f);
        }
        else
        {
            pos.y += Mathf.Lerp(0, 20f, _timer * 2);
        }
        GetComponent<RectTransform>().localPosition = pos;
    }

    private void DownStep()
    {
        var pos = _startPos;
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _isUp = true;
            _timer -= 0.5f;
            pos.y += _timer * 20f;
        }
        else
        {
            pos.y += Mathf.Lerp(0, 20f, _timer * 2);
        }
        GetComponent<RectTransform>().localPosition = pos;
    }
}
