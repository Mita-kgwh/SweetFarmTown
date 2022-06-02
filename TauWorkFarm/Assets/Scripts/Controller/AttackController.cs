using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] float offsetDistance = 1.2f;
    [SerializeField] Vector2 attackAreaSize = new Vector2(1, 1);

    [SerializeField] Rigidbody2D rigidbody;

    public bool Attack(int damage, Vector2 lastdirection)
    {
        Vector2 position = rigidbody.position + lastdirection * offsetDistance;
        
        Collider2D[] targets = Physics2D.OverlapBoxAll(position, attackAreaSize, 0f);

        foreach (Collider2D col in targets)
        {
            Damageable damageable = col.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        return targets.Length > 1;
    }
}
