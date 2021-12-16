using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackSpeed = 1f;
    public int damage = 1;
    public DamageType type;
    private float radius = 5f;

    private float attackCD = 0f;
    private void Update()
    {
        if (attackCD < 0f)
        {
            Collider2D hitCollider =
            Physics2D.OverlapCircle
            (
                transform.position,
                radius,

                1<<8
            );
            if (hitCollider != null)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Health -= damage;
                    attackCD = attackSpeed;
                }
            }
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }
}

public enum DamageType
{
    Normal
}