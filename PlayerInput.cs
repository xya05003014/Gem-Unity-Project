using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Variable
    [Header("===== Key settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string keyJUp;
    public string keyJDown;
    public string keyJLeft;
    public string keyJRight;



    [Header("===== Mouse settings =====")]
    //public bool mouseEnable = false;
    public float mouseSensitivityX = 2.5f;
    public float mouseSensitivityY = 3.0f;

    [Header("===== Output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;
    public bool run;
    [Header("===== Gravity =====")]
    public CameraControllerGem CC;
    public ActorController AC;
    public PlanetGravity planetGravity;
    private GameObject cameraHandle;
    private Transform m_transform;

    // 1.pressing signal
    
    // 2.trigger once signal
    // 3.double trigger


    [Header("===== Others =====")]

    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame

    private void Awake()
    {
        m_transform = transform;
    }
    void Update()
    {
        planetGravity.AddGravity(m_transform);
        //if (mouseEnable == true) 
        //{
        Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        Jright = Input.GetAxis("Mouse X") * mouseSensitivityX;
        /*}
        else
        {
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }*/

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }



        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector3 temDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = temDAxis.x;
        float Dup2 = temDAxis.y;

        //transform.position += Vector3.forward;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;// - new Vector3(0, 0, planetGravity.worldUp.z);
        //Dvec = transform.forward;
        run = Input.GetKey(keyA);

    }
    private Vector2 SquareToCircle(Vector2 input)
    {

        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }



}
