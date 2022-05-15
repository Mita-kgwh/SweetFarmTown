using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    IDamageable damageable;
    // receive the damage, play animation and stuff
    internal void TakeDamage(int damage)
    {
        if (damageable == null)
        {
            damageable = GetComponent<IDamageable>();
        }

        damageable.CalculateDamage(ref damage);
        damageable.ApplyDamage(damage);
        GamesManager.Instance.messageSystem.PostMessage(transform.position, damage.ToString());
        damageable.CheckState(); // check if damageable obj iz dead or not
    }
}
