using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Stats
    protected int damage = 1;
    protected float attackSpeed = 1f;
    protected float radius = 5f;
    protected float statModifier = 1;
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
            this.radius = data.range;
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
    public virtual void Destroy(bool returnMoney)
    {
        Debug.Log($"Destroyed {name}");

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
            damage = (int)MathF.Round(data.damage * statModifier);
            attackSpeed = data.attackCooldown * statModifier;
            radius = MathF.Round(data.range * statModifier);
        }
    }

    public void ApplyBuff(float buffMod)
    {
        StartCoroutine(ApplyBuffRoutine(buffMod));
    }
    private IEnumerator ApplyBuffRoutine(float buffMod)
    {
        if (statModifier < buffMod)
        {
            StopCoroutine(ApplyBuffRoutine(statModifier));
            statModifier = buffMod;
            statsChanged = true;
            yield return new WaitForSeconds(10);
            statModifier = 1;
            statsChanged = true;
        }
        else
        {
            print("Stronger buff already applied");
            yield return null;
        }
        
    }

}

public enum DamageType
{
    Physical,
    Fire,
    Lightning,
    Nature,
    Ice,
    Air
}