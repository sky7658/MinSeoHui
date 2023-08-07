using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    void TakeDamage(int damage,Vector3 reactVec);

    int GetDamage();
}
