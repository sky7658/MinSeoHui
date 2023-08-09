using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
    [SerializeField] float bulletSpeed = 15;
    [SerializeField] int bulletDamage = 10;

    private void OnEnable()
    {
        //5초뒤 자동으로 비활성화
        Invoke("DestroyBullet", 5);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void DestroyBullet()
    {
        BulletPool.ReturnObject(this);
    }

    public void TakeDamage(int damage, Vector3 reactVec)
    {
        BulletPool.ReturnObject(this);
    }
    
    public int GetDamage()
    {
        return bulletDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TakeDamage(0, Vector3.zero);
        }
    }
}