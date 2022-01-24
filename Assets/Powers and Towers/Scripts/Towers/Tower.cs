using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Stats
    protected int damage = 1;
    protected float damageModifier = 1;
    protected float attackSpeed = 1f;
    protected float attacSpeedModifier = 1;
    protected float range = 5f;
    protected float rangeModifier = 1;
    protected DamageType type;
    // Chache
    public TowerData data;
    protected Transform headTransform;
    protected SpriteRenderer headRender;
    protected SpriteRenderer baseRender;
    protected float attackCD = 0f;
    protected bool statsChanged;

    // 
    public virtual void Init(TowerData data)
    {
        if (data != this.data)
        {
            this.damage = data.damage;
            this.attackSpeed = data.attackCooldown;
            this.range = data.range;
            this.type = data.type;
            this.data = data;
        }

        if (headTransform == null)
            headTransform = gameObject.transform.GetChild(0);
        if (baseRender == null)
            baseRender = GetComponent<SpriteRenderer>();
        if (headRender == null && headTransform != null)
            headRender = headTransform.GetChild(0).GetComponent<SpriteRenderer>();

        if (data.Head != null)
        headRender.sprite = data.Head;
        if (data.Towerbase != null)
        baseRender.sprite = data.Towerbase;

    }
    protected virtual void Update()
    {
        CheckForBuffMod();
        Attack();
    }
    public virtual void Upgrade()
    {
        data.UpgradeTower(this);
        statsChanged = true;
    }
    protected virtual void Attack()
    {
        Debug.Log($"{name} Attacked");
    }
    public void DestroyTower(bool returnMoney)
    {
        //Debug.Log($"Destroyed {name}");
        if (returnMoney)
        {
            //Debug.Log("Return Money");
            GameManager.instance.Currency += Convert.ToInt32(data.cardCost * 0.5f);
        }
        //Debug.Log("Destroy Object");
        Destroy(gameObject);
    }

    private void CheckForBuffMod()
    {
        if (statsChanged)
        {
            statsChanged = false;
            damage = (int)MathF.Round(data.damage * damageModifier);
            print("Damage: " + damage);
            attackSpeed = data.attackCooldown * attacSpeedModifier;
            print("Attack Speed: " + attackSpeed);
            range = MathF.Round(data.range * rangeModifier);
            print("Range: " + range);
        }
    }

    public void ApplyBuff(float buffMod, BuffType buffType)
    {
        StartCoroutine(ApplyBuffRoutine(buffMod, buffType));
    }
    private IEnumerator ApplyBuffRoutine(float buffMod, BuffType buffType)
    {
        if (buffType.HasFlag(BuffType.Damage) && damageModifier < buffMod)
        {
            damageModifier = buffMod;
        }
        if (buffType.HasFlag(BuffType.Range) && rangeModifier < buffMod)
        {
            rangeModifier = buffMod;
        }
        if (buffType.HasFlag(BuffType.AttackSpeed) && attackSpeed < buffMod)
        {
            attacSpeedModifier = buffMod;
        }
        statsChanged = true;
        yield return new WaitForSeconds(10);
        if (buffType.HasFlag(BuffType.Damage))
        {
            damageModifier = 1;
        }
        if (buffType.HasFlag(BuffType.Range))
        {
            rangeModifier = 1;
        }
        if (buffType.HasFlag(BuffType.AttackSpeed))
        {
            attacSpeedModifier = 1;
        }
        statsChanged = true;
    }
}

public enum DamageType
{
    Physical,
    Fire,
    Lightning,
    Nature,
    Ice,
    Air,
    Cosmic
}