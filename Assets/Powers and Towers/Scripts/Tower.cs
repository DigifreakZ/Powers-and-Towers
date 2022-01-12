using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Stats
    protected int damage = 1;
    protected float attackSpeed = 1f;
    protected float radius = 5f;
    protected DamageType type;
    // Chache
    public TowerData data;
    protected Transform headTransform;
    protected SpriteRenderer headRender;
    protected SpriteRenderer baseRender;
    protected float attackCD = 0f;
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
        Attack();
    }
    public virtual void Upgrade()
    {
        data.UpgradeTower(this);
    }
    protected virtual void Attack()
    {
        Debug.Log($"{name} Attacked");
    }
    public virtual void Destroy()
    {
        Debug.Log($"Destroyed {name}");

        //Debug.Log("Return Money");
        GameManager.instance.Currency += Convert.ToInt32(data.cardCost * 0.5f);

        //Debug.Log("Destroy Object");
        Destroy(gameObject);
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