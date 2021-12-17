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

    [SerializeField] private TowerData towerData;
    private List<Collider2D> colliders;
    private float attackCD = 0f;
    private void Update()
    {
        Attack();
    }
    private void Attack()
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

public enum DamageType
{
    Normal,
    Fire
}