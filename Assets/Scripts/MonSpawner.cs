using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonSpawner : MonoBehaviour {
    // 몬스터가 스폰되어 있는지
    public bool isSpawned;
    // 몬스터 타입
    public MonType monType;
    
    // 이펙트
    public GameObject spawnEffect;

    private void Start()
    {
        isSpawned = false;
        //이펙트 할당
        spawnEffect = LMS.Manager.GameManager.Instance.ResourceLoadObj("SpawnEffect");
    }

    // 몬스터 스폰
    public void Spawn(Transform target)
    {
        Monster monster = MonsterPool.GetObject(monType, transform.position);
        monster.transform.SetParent(transform);

        monster.target = target;


        //이펙트 생성
        Instantiate(spawnEffect, transform.position, Quaternion.identity);
    }
    
    public void ReSpawn()
    {
        isSpawned = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonCol"))
        {
            if (!isSpawned)
            {
                isSpawned = true;
                Spawn(other.transform);
            }
        }
    }
}