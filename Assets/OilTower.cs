using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTower : Tower
{
    private List<Enemy> EnemiesInRange;
    [SerializeField] protected ParticleSystem particle;
    protected List<Collider2D> colliders;
    [SerializeField] protected GameObject projectile;
    private void Awake()
    {
        Init(Data);
    }

    public override void Init(TowerData data)
    {
        base.Init(data);
        EnemiesInRange = new List<Enemy>();
    }
    protected override void Attack()
    {
        if (attackCD < 0f)
        {
            Collider2D[] hitCollider =
            Physics2D.OverlapCircleAll
            (
                transform.position,
                range,
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
                if ((colliders[0].transform.position - transform.position).sqrMagnitude >= range * range) return;
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
        if (EnemiesInRange.Count <= 0) return;

        List<Enemy> enemies = EnemiesInRange;
        for (int i = 0; i < EnemiesInRange.Count; i++)
        {
            try
            {
                enemies[i].GetSlowed(Mathf.Clamp(damage * 0.5f,0,0.8f), 1f);
                enemies[i].ReceiveDamage(damage, type);
            }
            catch { Debug.LogWarning("Warning Error in ShootProjectile: OilTower.cs"); }
        }
        attackCD = attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            EnemiesInRange.Add(collision.transform.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            EnemiesInRange.Remove(collision.transform.GetComponent<Enemy>());
        }
    }

}
