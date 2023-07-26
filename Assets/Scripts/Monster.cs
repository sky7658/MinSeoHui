using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public BoxCollider meleeArea;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Material mat;
    private NavMeshAgent nav;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        //mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        Invoke("ChaseStart",2);
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

    private void FixedUpdate()
    {
        Targerting();
        if(!isChase)
            FreezeVelocity();
    }

    void Targerting()
    {
        float targetRadius = 1.5f;
        float targetRange = 0.2f;
        
        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        
        if(rayHits.Length > 0.7f && !isAttack)
        {
            transform.LookAt(target);
            
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Attack 01");
        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;
        
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        
        isChase = true;
        isAttack = false;
        anim.SetTrigger("Attack 01");
    }

    private void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
        
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if(curHealth > 0)
            mat.color = Color.white;
        else
        {
            mat.color = Color.gray;
            //수정 : 레이어 변경 로직 추가
            
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("Die");
            
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rb.AddForce(reactVec * 5, ForceMode.Impulse);
            
            Destroy(gameObject, 4);
        }
    }
}
