using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // receive the damage, play animation and stuff
    // Start is called before the first frame update
    internal void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
