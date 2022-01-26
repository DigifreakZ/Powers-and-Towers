using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowProjectile : Projectile
{
    [Range(0f,1f)]
    [SerializeField] protected float slowAmount;
    [Range(0f,30f)]
    [SerializeField] protected float slowDuration;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 7 && !foundTarget)
        {
            foundTarget = true;
            try
            {
                Enemy EM = collision.GetComponent<Enemy>();
                EM.ReceiveDamage(damage, damageType);
                EM.GetSlowed(Mathf.Clamp(damage * 0.01f, 0, 0.5f), 1f);
                Destroy(gameObject);
            }
            catch { }
        }
    }
}
