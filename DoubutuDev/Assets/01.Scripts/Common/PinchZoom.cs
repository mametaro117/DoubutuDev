using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour {

    private Camera camera;

    private float perspectiveZoomSpeed = 0.1f;
    private float orthoZoomSpeed = 0.1f;
    [SerializeField]
    private float _Debug1;
    [SerializeField]
    private float _Debug2;
    [SerializeField]
    private float _Debug3;
    [SerializeField]
    private GameObject Text1;
    private Text _Text1;
    [SerializeField]
    private GameObject Text2;
    private Text _Text2;
    [SerializeField]
    private GameObject Text3;
    private Text _Text3;
    private bool ScrollStart;
    private float ScrollStartPos;
    private float RetentionPos;
    private float ScrollAmount;
    private bool Reset_Poses;
    [SerializeField]
    private GameObject Buttons;
    private bool ButtonsActive = true;
    private float TowerDistance;
    [SerializeField]
    private GameObject _BG;
    private Vector3 __Pos;

    void Start()
    {
        camera = Camera.main;
        _Text1 = Text1.GetComponent<Text>();
        _Debug1 = camera.orthographicSize;
        _Text2 = Text2.GetComponent<Text>();
        _Debug2 = camera.transform.position.x;
        _Text3 = Text3.GetComponent<Text>();
        _Text3.text = "表示";
        TowerDistance = GameObject.Find("Player_Tower").transform.position.x;
        __Pos = _BG.transform.position;
    }

    void Update () {
		if (Input.touchCount == 2)
        {
            ScrollStart = false;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (camera.orthographic)
            {
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // 平行投影サイズが決して 0 未満にならないように気を付けてください
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 6f);
                camera.orthographicSize = Mathf.Min(camera.orthographicSize, 20f);
            }
            else
            {
                // そうでない場合は、タッチ間の距離の変化に基づいて有効視野を変更します
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // 有効視野を 0 から 180 の間に固定するように気を付けてください
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
        else if(Input.touchCount == 1)
        {
            if(!ScrollStart)
            {
                ScrollStart = true;
                ScrollStartPos = Input.GetTouch(0).position.x;
            }
            ScrollAmount = (ScrollStartPos - Input.GetTouch(0).position.x) * 0.07f;
            if(ScrollAmount + RetentionPos <= TowerDistance * -1)
            {
                camera.transform.position = new Vector3(TowerDistance * -1, camera.transform.position.y, camera.transform.position.z);
            }
            else if(ScrollAmount + RetentionPos >= TowerDistance)
            {
                camera.transform.position = new Vector3(TowerDistance, camera.transform.position.y, camera.transform.position.z);
            }
            else
            {
                camera.transform.position = new Vector3(ScrollAmount + RetentionPos, camera.transform.position.y, camera.transform.position.z);
            }
        }
        else
        {
            ScrollStart = false;
        }
        if(!ScrollStart)
        {
            RetentionPos = camera.transform.position.x;
        }
        if(Reset_Poses)
        {
            Reset_Poses = false;
            ScrollStartPos = 0f;
            RetentionPos = 0f;
            ScrollAmount = 0f;
            camera.transform.position = new Vector3(0, camera.transform.position.y, camera.transform.position.z);
        }
        _Debug1 = camera.orthographicSize;
        _Text1.text = _Debug1.ToString();
        _Debug2 = camera.transform.position.x;
        _Text2.text = _Debug2.ToString();

        float _Scale = 20f / camera.orthographicSize;
        _BG.GetComponent<RectTransform>().localScale = new Vector3(_Scale, _Scale, _Scale);

        _Debug3 = _BG.transform.position.x;

        _BG.transform.position = __Pos;
    }

    public void _DebugButton(int num)
    {
        switch(num)
        {
            case 0:
                Debug.Log("拡大率初期化");
                camera.orthographicSize = 10f;
                break;
            case 1:
                Debug.Log("カメラ位置初期化");
                Reset_Poses = true;
                break;
            case 2:
                ButtonsActive = !ButtonsActive;
                if(ButtonsActive)
                {
                    Debug.Log("ボタンを表示しました");
                    _Text3.text = "表示";
                }
                else
                {
                    Debug.Log("ボタンを非表示にしました");
                    _Text3.text = "非表示";
                }
                Buttons.SetActive(ButtonsActive);
                break;
            default:
                Debug.Log("<color=red>デバッグエラー！</color>");
                break;
        }
    }
}
