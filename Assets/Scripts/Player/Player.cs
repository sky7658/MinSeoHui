using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using LMS.UI;
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
    
    public PlayerUIManger playerUIManger;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsulCol = GetComponent<CapsuleCollider>();
        playerUIManger = new PlayerUIManger();
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
        _stateMachine.AddState(StateName.ATTACK, new Attack());

        playerUIManger.CreateInfoText();
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.UpdateState();
        CharacterRotation();
        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        _camera.transform.position += Vector3.forward * Time.deltaTime * scroollWheel * scrollSpeed;
        
        //마우스 왼쪽 클릭시 콤보어택
        if (Input.GetMouseButtonDown(0))
        {
            playerUIManger.ComboAttacks(gameObject, () => _stateMachine.ChangeState(StateName.ATTACK));
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            int rand = UnityEngine.Random.Range(0, 3);
            //int rand2 = Random.Range(1, 4);
            //playerUI.PushCard(rand, (LMS.Cards.CardProperty)rand2);
            playerUIManger.PushCard(rand, (LMS.Cards.CardProperty)3);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerUIManger.SelectCard(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerUIManger.SelectCard(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerUIManger.SelectCard(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerUIManger.SelectCard(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerUIManger.SelectCard(4);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            playerUIManger.SetHand();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            playerUIManger.PopCard();
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            playerUIManger.UseCard(gameObject, transform.forward);
        }
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
