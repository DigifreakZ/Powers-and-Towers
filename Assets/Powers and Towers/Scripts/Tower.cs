using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackSpeed = 1f;
    public int damage = 1;
    public DamageType type;
    private float radius = 5f;

    private float attackCD = 0f;
    private void Update()
    {
        if (attackCD < 0f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Health -= damage;

                    break;
                }
            }
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }
}

public enum DamageType
{
    Normal
}