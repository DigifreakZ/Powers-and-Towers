using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaTower : Tower
{
    protected List<Collider2D> colliders;
    protected override void Attack()
    {
        if (attackCD < 0f)
        {
            Collider2D[] hitCollider =
            Physics2D.OverlapCircleAll
            (
                transform.position,
                radius,
                1 << 8
            );
            colliders = new List<Collider2D>();
            colliders.AddRange(hitCollider);
            if (colliders != null && !(colliders.Count <= 0))
            {
                colliders.Sort(
                    (x1, x2) =>
                (
                    x1.transform.position - transform.position
                    )
                    .sqrMagnitude.CompareTo
                    (
                        (x2.transform.position - transform.position).sqrMagnitude
                    )
                );
                if ((colliders[0].transform.position - transform.position).sqrMagnitude >= radius * radius) return;
                if (colliders[0] != null)
                {
                    Enemy enemy = colliders[0].GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ReceiveDamage(damage, type);
                        Vector3 triangle = enemy.transform.position - transform.position;
                        headTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
                        attackCD = attackSpeed;
                    }
                }
            }
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }
}
