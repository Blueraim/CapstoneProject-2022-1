using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    // 상태 변수
    private bool isRun = false;

    // 충돌 여부 체크를 위한 컴포넌트
    private CapsuleCollider capsuleCollider;

    // 필요한 컴포넌트
    private Rigidbody myRigid; //플레이어의 실질적인 몸, Rigidbody가 Collider에 물리학을 입힌다.
    private Vector3 _velocity;
    
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        
        applySpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        TryRun();
        Move();
    }

    private void Move(){
        _velocity = transform.forward * applySpeed;
        
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
        

    private void CharacterRotation(){
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X"); // 가상 패드로부터 회전값 받아오기

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(new Vector3(0f, _yRotation, 0f)));
    }

    // 달리기
    private void TryRun(){
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        else if(!Input.GetKey(KeyCode.LeftShift)){
            RunningCancle();
        }
    }

    private void Running(){
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancle(){
        isRun = false;
        applySpeed = walkSpeed;
    }
}
