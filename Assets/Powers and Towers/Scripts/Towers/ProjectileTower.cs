using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : Tower
{
    protected List<Collider2D> colliders;
    [SerializeField] protected GameObject projectile;
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
                        ShootProjectile(enemy.transform.position);
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

    protected void ShootProjectile(Vector3 target)
    {
        Vector3 triangle = target - transform.position;
        GameObject _obj= Instantiate(projectile,transform.position,Quaternion.identity);
        _obj.GetComponent<Projectile>().Init(damage,type);
        _obj.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
        _obj.GetComponent<Rigidbody2D>().AddForce(_obj.transform.right * 1000f);
    }
}
