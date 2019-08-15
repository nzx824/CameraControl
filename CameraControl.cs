using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public float minFov = 15.0f;
    public float maxFov = 150.0f;
    public float sensitivity = 10f;
    public Transform target_transform = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //检测鼠标左键是否按下
        bool press_leftmouse = Input.GetMouseButton(0);
        //检测鼠标右键是否按下
        bool press_rightmouse = Input.GetMouseButton(1);
        //检测鼠标中轮是否按下
        bool ScrollWheel = Input.GetMouseButton(2);
        //检测ALT键是否按下
        bool alt = Input.GetKey(KeyCode.LeftAlt);

        //获得鼠标水平 垂直方向的移动距离
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //鼠标滚轮调整角度
        if (ScrollWheel)
        {
            transform.Rotate(my*2.0f,mx*2.0f,0);
        }
        //alt+左键移动场景
        if (press_leftmouse & alt)
        {
            transform.Translate(-mx*5.0f,0,-my*5.0f);
        }
        //alt+右键摄像机按Y轴移动
        if (press_rightmouse & alt)
        {
            transform.Translate(0,-my*5.0f,0);
        }
        //拉近拉远效果

        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov,minFov,maxFov);
        Camera.main.fieldOfView = fov;

        //摄像机归位
        if (Input.GetKeyDown(KeyCode.H))
        {
            Camera.main.fieldOfView = 60.0f;
            Camera.main.transform.position = new Vector3(0f, 9.034401f, -13.91566f);
            Camera.main.transform.eulerAngles = new Vector3(21.4182f,0,0);
        }

        //让摄像机围绕指定对象旋转
        if (press_rightmouse)
        {
            Vector3 mouse_position = Input.mousePosition;
            Ray RayObj = Camera.main.ScreenPointToRay(mouse_position);
            RaycastHit hitObj;
            if (Physics.Raycast(RayObj, out hitObj))
            {
                if (hitObj.collider.gameObject.tag == "terrain")
                {
                    target_transform = null;
                }
                else { target_transform = hitObj.transform; }
            }
            if (target_transform != null)
            {
                //Debug.Log("摄像取得对象");
                //float mouseX=Input.GetAxis("Mouse X")*2.0f;
                //target_transform.transform.Rotate(new Vector3(0,-mouseX,0));
                transform.RotateAround(target_transform.transform.position, Vector3.up, 20*Time.deltaTime);
            }
            else { Debug.Log("无法获取对象"); }
        }
	}
}
