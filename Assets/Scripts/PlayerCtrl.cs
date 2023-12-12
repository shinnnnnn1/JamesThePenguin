using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform body;
    [SerializeField] Transform bodyR;
    [SerializeField][Range(1f, 10f)] float moveSpd = 10;
    [SerializeField][Range(0.1f, 5f)] float sensitivity;
    float rotX;
    float savedSpd;
    float top = 2.462f;
    float bottom = 1.55f;
    float target;
    public float recoilX;
    public float recoilY;
    public float spread;
    public float recoilValue;
    public Vector3 recoilDeg;

    [HideInInspector] public bool crouch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigid = GetComponent<Rigidbody>();
        savedSpd = moveSpd;
    }
    void Update()
    {
        Crouch();
    }
    void FixedUpdate()
    {
        Move();
        RotationY();
        RotationX();
        //Origin();
    }

    void Move()
    {
        Vector3 hor = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 ver = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 veloc = (hor + ver).normalized * moveSpd;
        rigid.MovePosition(transform.position + veloc * Time.deltaTime);
    }
    void RotationY()
    {
        Vector3 rotY = Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotY));
    }
    void RotationX()
    {
        rotX -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        rotX = Mathf.Clamp(rotX, -90, 90);
        body.localEulerAngles = Vector3.right * rotX + Vector3.right;
    }
    void Crouch()
    {
        crouch = Input.GetButton("Crouch");
        if(crouch)
        {
            moveSpd = savedSpd * 0.5f;
            target = Mathf.Lerp(body.position.y, bottom, 0.25f);
            body.position = new Vector3(body.position.x, target, body.position.z);
        }
        else
        {
            moveSpd = savedSpd * 1;
            target = Mathf.Lerp(body.position.y, top, 0.25f);
            body.position = new Vector3(body.position.x, target, body.position.z);
        }
    }

    void Origin()
    {
        print(bodyR.localEulerAngles);
        //recoilX = Mathf.Clamp(recoilX, -10, 0);
        //bodyR.localEulerAngles = new Vector3(recoilX, 0, 0);

        spread = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));

        recoilX = Mathf.Lerp(bodyR.localEulerAngles.x, 90, 0.05f);
        recoilY = Mathf.Lerp(bodyR.localEulerAngles.y, 180, 0.05f);
        bodyR.localEulerAngles = new Vector3(recoilX, 0, 0);
    }
    public void Recoil()
    {
        //recoilValue++;
        //bodyR.localEulerAngles = new Vector3(bodyR.localEulerAngles.x - Random.Range(3, 5) - spread, 0, bodyR.localEulerAngles.z + Random.Range(-1, 1) - spread);
        bodyR.transform.DOPunchRotation(new Vector3(-1, 0, 0), 0.5f, 1, 1);
        bodyR.position = new Vector3(0, 2.462f, 0);
    }
}
