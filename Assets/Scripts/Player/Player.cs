using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    State _state;
    [SerializeField] Camera _camera;
    public StateMachine _stateMachine;
    //스피드 조정 변수
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float runSpeed = 10f;
    
    public float applySpeed;
    
    [SerializeField] public float jumpForce = 10f;
    
    //상태 변수
    public bool isGround = true;
    
    //땅 착지여부
    public CapsuleCollider capsulCol;

    //민감도
    [SerializeField] private float lookSensitivity = 1f;
    [SerializeField] private float scrollSpeed = 2f;
    private float currentCameraRotationX = 0f;
    
    [SerializeField] private Camera playerCamera;
    public StateName stateName;
    
    public Rigidbody rb;
    public Animator anim;
    
    public float hp = 100f;
    public float maxHp = 100f;


    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsulCol = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        applySpeed = walkSpeed;
        
        _stateMachine = new StateMachine(StateName.IDLE, new Idle(), this);
        _stateMachine.AddState(StateName.MOVE, new Move());
        _stateMachine.AddState(StateName.SPRINT, new Sprint());
        _stateMachine.AddState(StateName.JUMP, new Jump());
        _stateMachine.AddState(StateName.HIT, new Hit());
        _stateMachine.AddState(StateName.BACK, new BackMove());
        _stateMachine.AddState(StateName.DEAD, new Dead());
        _stateMachine.AddState(StateName.FALL, new Fall());
        _stateMachine.AddState(StateName.ROLL, new Roll());
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.UpdateState();
        CharacterRotation();
        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        _camera.transform.position += Vector3.forward * Time.deltaTime * scroollWheel * scrollSpeed;
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        transform.eulerAngles += _characterRotationY;
    }

    public void Roll()
    {
        //컨트롤키 누르면 구르기
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _stateMachine.ChangeState(StateName.ROLL);
        }
        
    }

    public void TryMove()
    {
        //w를 눌렀을때
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _stateMachine.ChangeState(StateName.SPRINT);
            }
            else
            {
                _stateMachine.ChangeState(StateName.MOVE);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _stateMachine.ChangeState(StateName.BACK);
        }
        else
        {
            _stateMachine.ChangeState(StateName.IDLE);
        }
    }

    public void TryJump()
    {
        //스페이스바가 눌리면 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            _stateMachine.ChangeState(StateName.JUMP);
        }
    }

    private void CameraRotation()
    {
        // float _xRotation = Input.GetAxisRaw("Mouse Y");
        // float _cameraRotationX = _xRotation * lookSensitivity;
        // currentCameraRotationX -= _cameraRotationX;
        // currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        //
        // playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            IDamageable damageableObject = other.GetComponent<IDamageable>();

            if (damageableObject == null)
                damageableObject = other.GetComponentInParent<IDamageable>();

            if (damageableObject != null)
                TakeDamage(damageableObject.GetDamage(), Vector3.zero);
        }
    }
    
    public void ToIdle()
    {
        print("ToIdle");
        _stateMachine.ChangeState(StateName.IDLE);
    }
    
    public void TakeDamage(int damage, Vector3 reactVec)
    {
        hp -= damage;
        if (hp <= 0)
            _stateMachine.ChangeState(StateName.DEAD);
        else
            _stateMachine.ChangeState(StateName.HIT);
    }
    
    public int GetDamage()
    {
        return 0;
    }
}
