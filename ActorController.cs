using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public PlanetGravity planetGravity;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 4.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
    [SerializeField]
    private Vector3 modelForward;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        //print(pi.Dup);
        //Vector3 targetLocalEulerangles = pi.transform.localEulerAngles;
        
        float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);
        anim.SetFloat("forWard", pi.Dmag * Mathf.Lerp(anim.GetFloat("forWard"), targetRunMulti, 0.2f));
        if (pi.Dmag > 0.1f) 
        {
            
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            //Vector3 targetLocalEulerangles = pi.transform.localEulerAngles;
            //Vector3 targetForward = pi.Dvec;
            //Quaternion targetForward = pi.transform.rotation;
            //Quaternion targetForward1 = planetGravity.worldUp.eulerAngles;
            model.transform.forward = targetForward;
            
            //model.transform.forward = pi.Dright * transform.right + pi.Dup * transform.forward;
            //model.transform.localEulerAngles = targetLocalEulerangles;
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);//* pi.Dmag * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        model.transform.localEulerAngles = new Vector3(0, model.transform.localEulerAngles.y, 0);
        //Debug.Log("model的本地坐标为：" + model.transform.localEulerAngles);
        //Debug.Log("PI的本地坐标为：" + pi.transform.localEulerAngles);
    }
    void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }
}
