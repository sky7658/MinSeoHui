using System;
using System.Collections;
using System.Collections.Generic;
using LMS.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float curHealth;
    public float damage;
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public BoxCollider meleeArea;
    public MonType monsterType;
    [SerializeField] float targetRadius = 10f;
    HpBarUI hpBarUI;

    private Rigidbody rb;
    private Collider boxCollider;
    private Material mat;
    public NavMeshAgent nav;
    protected Animator anim;
    
    Action OnDieCallBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
        //mat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hpBarUI = transform.GetChild(0).GetChild(0).GetComponent<HpBarUI>();
    }

    public virtual void Init(Action OnDieCallBack = null)
    {
        this.OnDieCallBack = OnDieCallBack;
        hpBarUI.Initialized(maxHealth, true);
        boxCollider.enabled = true;
        rb.useGravity = true;
        nav.enabled = true;
        deadCnt = 0;
    }

    private void OnEnable()
    {
        Invoke("ChaseStart",2);
    }

    private void OnDisable()
    {
        //인보크 종료
        CancelInvoke();
        StopAllCoroutines();
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("Walk Forward", true);
    }

    void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    protected void FixedUpdate()
    {
    }

    protected bool Targerting()
    {
        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, Vector3.up, 0f, LayerMask.GetMask("Player"));
        
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0f, targetDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        if (rayHits.Length > 0 && !isAttack)
        {
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (!isChase)
            FreezeVelocity();
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }
    private int deadCnt = 0;
    IEnumerator OnDamage(Vector3 reactVec)
    {
        //mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (curHealth > 0)
        { }
        else
        {
            if (deadCnt > 0) yield break;
            deadCnt++;
            var _item = LMS.Utility.ObjectPool.Instance.GetObject<LMS.Item.DropItem>("Item");
            LMS.Utility.UtilFunction.TurnOnOff(LMS.Utility.ObjectPool.Instance.objectInfos[6], _item.gameObject, true);
            _item.Initialized(transform.position);
            anim.SetTrigger("Die");
            boxCollider.enabled = false;
            rb.useGravity = false;
            isChase = false;
            nav.enabled = false;
        }
    }

    public void ReturnMon()
    {
        MonsterPool.ReturnObject(this);
        OnDieCallBack?.Invoke();
    } 

    public void TakeDamage(float damage, Vector3 reactVect)
    {
        curHealth -= damage;
        hpBarUI.UpdateHpBar(damage);
        StartCoroutine("OnDamage", reactVect);
    }
    
    public float GetDamage()
    {
        return damage;
    }
}
