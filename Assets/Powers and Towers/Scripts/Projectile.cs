using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public DamageType damageType;
    public float DestroyMe = 2f;
    protected bool foundTarget;
    protected bool iniziated;
    protected virtual void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 7 && !foundTarget)
        {
            foundTarget = true;
            Enemy EM = collision.GetComponent<Enemy>();
            EM.ReceiveDamage(damage, damageType);
            Destroy(gameObject);
        }
    }
    public virtual void Update()
    {
        if (!iniziated) return;
        if (DestroyMe <= 0) Destroy(gameObject);
        DestroyMe -= Time.deltaTime;
    }

    public virtual void Init(int damage, DamageType type)
    {
        this.damage = damage;
        damageType = type;
        iniziated = true;
    }
}
