using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    private bool isRun = false;

    private VirtualJoystick virtualJoystick;
    private CapsuleCollider capsuleCollider;
    private Rigidbody myRigid; 
    private Vector3 _velocity;


    void Start()
    {
        virtualJoystick = FindObjectOfType<VirtualJoystick>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();

        applySpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        Move();

        if (!isRun)
        {
            RunningCancle();
        }
    }

    private void Move()
    {
        _velocity = transform.forward * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }



    public void TryRun()
    {
        Running();
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancle()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Update()
    {
        FreezeVelocity();
        float x = virtualJoystick.Horizontal();
        float z = virtualJoystick.Vertical();


        if (x != 0 || z != 0)
        {
            this.transform.rotation = (Quaternion.Euler(0f, Mathf.Atan2(virtualJoystick.Horizontal(), virtualJoystick.Vertical()) * Mathf.Rad2Deg, 0f));
        }
    }

    void FreezeVelocity()
    {
        myRigid.velocity = Vector3.zero;
        myRigid.angularVelocity = Vector3.zero;
    }
}