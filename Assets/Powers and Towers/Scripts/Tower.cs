using UnityEngine;

public class Tower : MonoBehaviour
{
    // Stats
    protected int damage = 1;
    protected float attackSpeed = 1f;
    protected float radius = 5f;
    protected DamageType type;
    // Chache
    protected TowerData data;
    protected Transform headTransform;
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