using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackSpeed = 1f;
    public int damage = 1;
    public DamageType type;
    private float radius = 5f;
    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // enemy.TakeDamage();
            }
        }
    }
}

public enum DamageType
{
    Normal
}