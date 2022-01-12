using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public DamageType damageType;
    public float DestroyMe = 2f;
    private bool foundTarget;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 7 && !foundTarget)
        {
            foundTarget = true;
            collision.GetComponent<Enemy>().ReceiveDamage(damage, damageType);
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (DestroyMe <= 0) Destroy(gameObject);
        DestroyMe -= Time.deltaTime;
    }

    public void Init(int damage, DamageType type)
    {
        this.damage = damage;
        damageType = type;
    }
}
