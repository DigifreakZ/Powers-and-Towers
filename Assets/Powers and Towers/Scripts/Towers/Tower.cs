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
    protected TowerData data;
    public TowerData Data => data;

    protected Transform headTransform;
    protected SpriteRenderer headRender;

    protected SpriteRenderer baseRender;
    protected float attackCD = 0f;
    protected bool statsChanged;

    internal void Deselect()
    {
        if (TowerSelector.instance == null) return;
        print($"Deselected|{name}|");

        Transform towerIndicator = TowerSelector.instance.SelectedTowerRangeIndictor.transform;

        if (towerIndicator.gameObject.activeSelf)
            towerIndicator.gameObject.SetActive(false);
    }
    internal void Select()
    {
        if (TowerSelector.instance == null) return;
        print($"Selected|{name}|");

        Transform towerIndicator = TowerSelector.instance.SelectedTowerRangeIndictor.transform;
        towerIndicator.position = transform.position;

        SpriteRenderer tempSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        SpriteRenderer tempSpriteRenderer2 = towerIndicator.GetComponent<SpriteRenderer>();
        towerIndicator.localScale = Vector3.one * range * 2;
        tempSpriteRenderer2.renderingLayerMask = tempSpriteRenderer.renderingLayerMask;
        tempSpriteRenderer2.sortingOrder = tempSpriteRenderer.sortingOrder - 1;

        if (!towerIndicator.gameObject.activeSelf)
            towerIndicator.gameObject.SetActive(true);
    }

    // 
    public virtual void Init(TowerData data)
    {
        this.damage = data.damage;
        this.attackSpeed = data.attackCooldown;
        this.range = data.range;
        this.type = data.type;
        this.data = data;

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
        Data.UpgradeTower(this);
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
            GameManager.instance.Currency += Convert.ToInt32(Data.cardCost * 0.5f);
        }
        //Debug.Log("Destroy Object");
        Destroy(gameObject);
    }

    private void CheckForBuffMod()
    {
        if (statsChanged)
        {
            statsChanged = false;
            damage = (int)MathF.Round(Data.damage * damageModifier);
            print("Damage: " + damage);
            attackSpeed = Data.attackCooldown * attacSpeedModifier;
            print("Attack Speed: " + attackSpeed);
            range = MathF.Round(Data.range * rangeModifier);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f,0f,0f,0.2f);
        Gizmos.DrawSphere(transform.position, Data.range);
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