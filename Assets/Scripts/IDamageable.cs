using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    void TakeDamage(float damage,Vector3 reactVec);

    float GetDamage();
}
