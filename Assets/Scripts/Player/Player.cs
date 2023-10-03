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
    [SerializeField] private HpBarUI hpBarUI;
    private ParticleSystem healingEffect;
    State _state;
    [SerializeField] Camera _camera;
    public StateMachine _stateMachine;
    public float distance = 11f;
    private Vector3 velocity = Vector3.zero;
    
    //스피드 조정 변수
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float runSpeed = 10f;
    
    public float applySpeed;
    
    [SerializeField] public float jumpForce = 10f;
    
    //상태 변수
    public bool isGround = true;
    public bool isHit = false;
    
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
    public float superArmorTime = 3f;
    
    public PlayerUIManger playerUIManger;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsulCol = GetComponent<CapsuleCollider>();
        playerUIManger = new PlayerUIManger();
        healingEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
    }
    void Start()
    {
        applySpeed = walkSpeed;
        FadeManager.Instance.OnFadeIn();
        
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
        _stateMachine.AddState(StateName.SKILL, new Skill());

        hpBarUI.Initialized(maxHp, false);

        playerUIManger.CreateInfoText();
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.UpdateState();
        CharacterRotation();
        //SmoothDamp를 이용한 카메라 줌인아웃
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        if (distance < 10f) distance = 10.0f;
        if (distance > 30f) distance = 30.0f;
        
        Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance); // 카메라가 바라보는 앞방향은 Z 축입니다. 이동량에 따른 Z 축방향의 벡터를 구합니다.
        _camera.transform.position = Vector3.SmoothDamp(
            _camera.transform.transform.position,
            this.transform.position - _camera.transform.rotation * reverseDistance,
            ref velocity,
            0.2f);


        //마우스 왼쪽 클릭시 콤보어택
        if (Input.GetMouseButtonDown(0))
        {
            playerUIManger.ComboAttacks(gameObject, () => _stateMachine.ChangeState(StateName.ATTACK));
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            int rand = UnityEngine.Random.Range(0, 4);
            //int rand2 = Random.Range(1, 4);
            //playerUI.PushCard(rand, (LMS.Cards.CardProperty)rand2);
            playerUIManger.PushCard(rand, (LMS.Cards.CardProperty)0);
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
            if (playerUIManger.UseCard(gameObject, transform.forward))
                _stateMachine.ChangeState(StateName.SKILL);
        }
    }

    public void PlayHealingEffect()
    {
        healingEffect.gameObject.SetActive(true);
        healingEffect.Play();
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
        if (playerUIManger.disableMovement) return;
        
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGround =
                Physics.Raycast(transform.position, Vector3.down, capsulCol.bounds.extents.y / 2 + 0.1f);
            if(isGround)
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
        _stateMachine.ChangeState(StateName.IDLE);
    }

    private Coroutine superArmorCoroutine;
    public void TakeDamage(float damage, Vector3 reactVec)
    {
        if(isHit) return;

        hp -= damage;
        hpBarUI.UpdateHpBar(damage);
        if (hp <= 0)
            _stateMachine.ChangeState(StateName.DEAD);
        else
            _stateMachine.ChangeState(StateName.HIT);

        if(superArmorCoroutine != null)
        {
            StopCoroutine(superArmorCoroutine);
            superArmorCoroutine = null;
        }
        
        superArmorCoroutine = StartCoroutine(SuperArmor());
    }

    public void RecoveryHp(float value)
    {
        hp += value;
        hpBarUI.UpdateHpBar(value, false);
    }

    IEnumerator SuperArmor()
    {
        isHit = true;
        yield return new WaitForSeconds(superArmorTime);
        isHit = false;

        yield break;
    }
    
    public float GetDamage()
    {
        return 0;
    }
}
