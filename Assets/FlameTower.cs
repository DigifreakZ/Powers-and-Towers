using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : Tower
{
    private List<Enemy> EnemiesInFlame; 
    [SerializeField] protected ParticleSystem particle;
    protected List<Collider2D> colliders;
    [SerializeField] protected GameObject projectile;
    private void Awake()
    {
        Init(data);
    }

    public override void Init(TowerData data)
    {
        base.Init(data);
        EnemiesInFlame = new List<Enemy>();
    }
    protected override void Attack()
    {
        if (attackCD < 0f)
        {
            Collider2D[] hitCollider =
            Physics2D.OverlapCircleAll
            (
                transform.position,
                radius,
                1 << 7
            );
            colliders = new List<Collider2D>();
            colliders.AddRange(hitCollider);
            if (colliders != null && !(colliders.Count <= 0))
            {
                particle.Play();
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
                        Vector3 triangle = enemy.transform.position - transform.position;
                        headTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
                        // enemy.ReceiveDamage(damage, type);
                        ShootProjectile();
                    }
                }
            }
            else
            {
                particle.Stop();
            }
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }

    protected void ShootProjectile()
    {
        if (EnemiesInFlame.Count <= 0) return;

        List<Enemy> enemies = EnemiesInFlame;
        for (int i = 0; i < EnemiesInFlame.Count; i++)
        {
            enemies[i].ReceiveDamage(damage,type);
        }
        attackCD = attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
           EnemiesInFlame.Add(collision.transform.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            EnemiesInFlame.Remove(collision.transform.GetComponent<Enemy>());
        }
    }

}