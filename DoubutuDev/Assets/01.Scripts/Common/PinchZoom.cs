using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour {

    private Camera camera;

    private float perspectiveZoomSpeed = 0.5f;
    private float orthoZoomSpeed = 0.5f;

    void Start()
    {
        camera = Camera.main;
    }

    void Update () {
		if (Input.touchCount == 2)
        {
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
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // そうでない場合は、タッチ間の距離の変化に基づいて有効視野を変更します
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // 有効視野を 0 から 180 の間に固定するように気を付けてください
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
	}
}
