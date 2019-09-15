using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerGem : MonoBehaviour
{
    public PlayerInput pi;
    public PlanetGravity planetGravity;
    public float horizontalSpeed = 100.00f;
    public float verticalSpeed = 100.00f;
    public float camareDampValue = 0.2f;
    public float cameraHorizontalAngleMax = 30;
    public float cameraHorizontalAngleMin = 300;
    public float distance = 2.1f;
    public float cameraHeigh = 0.0f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private new GameObject camera;
    private Vector3 worldup;

    private Vector3 cameraDampVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 0;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;


        //藏鼠标
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 tempModelEuler = new Vector3(model.transform.eulerAngles.y, model.transform.eulerAngles.y, playerHandle.transform.eulerAngles.z);
        Vector3 tempModelEuler = model.transform.eulerAngles;
        
        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.fixedDeltaTime);
        //playerHandle.transform.Rotate(Vector3.right, pi.Jup * verticalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, cameraHorizontalAngleMin, cameraHorizontalAngleMax);
        //cameraHandle.transform.localEulerAngles player 坐标以此改变
        //cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);//世界坐标
        cameraHandle.transform.localRotation = Quaternion.Euler(tempEulerX, 0, 0);
        cameraHandle.transform.rotation = Quaternion.Euler(cameraHandle.transform.eulerAngles.x, pi.transform.eulerAngles.y, pi.transform.eulerAngles.z);
        //camera.transform.localRotation = new Vector3(tempEulerX, 0, 0);
        //cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, playerHandle.transform.eulerAngles.x, playerHandle.transform.eulerAngles.z);
        model.transform.eulerAngles = tempModelEuler;
        //worldup = new Vector3(0, playerHandle.transform.eulerAngles.y, 0);
        //Debug.Log("caremaLocalAngle本地坐标为：" + cameraHandle.transform.localEulerAngles);
        

        //最精简相机追上
        //camera.transform.position = transform.position;
        //camera.transform.eulerAngles = transform.eulerAngles;

        //物体重力方向

        //lerp方法，摄影机追上
        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);
        //camera.transform.LookAt(cameraHandle.transform, worldup = new Vector3(0, playerHandle.transform.position.y, 0));
        //camera.transform.LookAt(new Vector3(cameraHandle.transform.position.x, cameraHandle.transform.position.y, cameraHandle.transform.position.z))

        //camera.transform.position=
        //smoothDamp方法，摄影机追上
        //camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, camareDampValue);
        //camera.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, playerHandle.transform.localEulerAngles.z);
        //camera.transform.localRotation = Quaternion.Euler(pi.transform.eulerAngles.x, pi.transform.eulerAngles.y, pi.transform.eulerAngles.z);
        camera.transform.localRotation = Quaternion.Euler(cameraHandle.transform.eulerAngles.x, playerHandle.transform.eulerAngles.y, cameraHandle.transform.eulerAngles.z);
        //camera.transform.localRotation = Quaternion.Euler(cameraHandle.transform.localEulerAngles);
        //camera.transform.LookAt(cameraHandle.transform);
        //forward方法，锁定人物的一个位置
        camera.transform.position = cameraHandle.transform.position + cameraHandle.transform.forward * distance;
        //camera.transform.localPosition = new Vector3(0,0,0) + new Vector3(cameraHandle.transform.forward.x, cameraHandle.transform.forward.y, cameraHandle.transform.forward.z)*distance;
        //camera.transform.localPosition =new Vector3(cameraHandle.transform.position.x, 0,distance);

        Debug.Log("caremaEulerAngle本地坐标为：" + camera.transform.localEulerAngles);
        Debug.Log("cameraHandleForward:" + cameraHandle.transform.forward.y);
        //Debug.Log("posEulerAngle本地坐标为：" + transform.rotation.eulerAngles);
        //Debug.Log("playHandleEulerAngle本地坐标为：" + playerHandle.transform.rotation.localeulerAngles);
        //Debug.Log("piEulerAngle本地坐标为：" + pi.transform.rotation.eulerAngles);
        //Debug.Log("caremaPotion世界位置为：" + camera.transform.position);
        //Debug.Log("Y轴输出：" + Input.GetAxis("Mouse Y"));


    }
}
