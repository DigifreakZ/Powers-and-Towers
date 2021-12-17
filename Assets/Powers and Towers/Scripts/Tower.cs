using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Stats
    protected int damage = 1;
    protected float attackSpeed = 1f;
    protected float radius = 5f;
    protected DamageType type;
    //
    // Chache
    protected Transform headTransform;
    protected float attackCD = 0f;
    // 
    public virtual void Init(int damage, float attackspeed, float radius, DamageType type)
    {
        this.damage = damage;
        this.attackSpeed = attackspeed;
        this.radius = radius;
        this.type = type;
        headTransform = gameObject.transform.GetChild(0);
    }
    protected virtual void Update()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        Debug.Log($"{name} Attacked");
    }
}

public enum DamageType
{
    Physical,
    Fire,
    Lightning,
    Earth,
    Water,
    Air
}