using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private int _lootValue = 1;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform[] _nodes;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] DamageType[] resistances;
    [SerializeField] DamageType[] Weakness;
    private int nextNodeIndex = 0;
    public virtual int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _nodes = MapManager.GetPath();
        if (_nodes == null) Destroy(gameObject);
        Vector3 triangle = _nodes[nextNodeIndex].position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
        Physics2D.IgnoreLayerCollision(6, 6);
    }
    private void FixedUpdate()
    {
        if (_nodes != null)
        {
            if ((transform.position - _nodes[nextNodeIndex].position).sqrMagnitude < 0.3 * 0.3)
            {
                if (nextNodeIndex < _nodes.Length - 1)
                    nextNodeIndex += 1;
                else
                    ReachedEnd();


                Vector3 triangle = _nodes[nextNodeIndex].position - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
            }
            rb.position = Vector3.MoveTowards(transform.position, _nodes[nextNodeIndex].position, _speed * Time.fixedDeltaTime);
        }
    }

    public void ReceiveDamage(float dmg, DamageType type)
    {
        float dmgMultiplyer = 1f;
        foreach(DamageType _type in Weakness)
        {
            if (_type == type)
            {
                dmgMultiplyer *= 2f;
            }
        }
        foreach (DamageType _type in resistances)
        {
            if (_type == type)
            {
                dmgMultiplyer *= 0.5f;
            }
        }
        Health -= Mathf.Clamp(Mathf.RoundToInt(dmg * dmgMultiplyer),1,int.MaxValue);
    }

    public void StartDOTRoutine(float damageOverTimeDamage, float damageOverTimeDuration, DamageType damageType)
    {
        StartCoroutine(DamageOverTime(damageOverTimeDamage, damageOverTimeDuration, damageType));
    }
    public IEnumerator DamageOverTime(float damageOverTimeDamage, float damageOverTimeDuration, DamageType damageType)
    {
        for (int i = 0; i < damageOverTimeDuration; i++)
        {
            ReceiveDamage(damageOverTimeDamage, damageType);
            Debug.Log("Took " + damageOverTimeDamage + " at second " + i);
            yield return new WaitForSeconds(1);
        }
    }

    public void GetSlowed(float slowAmount, float slowDuration)
    {
        StartCoroutine(GetSlowedRoutine(slowAmount, slowDuration));
    }
    private IEnumerator GetSlowedRoutine(float slowAmount, float slowDuration)
    {
        float defaultSpeed = _speed;
        _speed *= 1-slowAmount;
        yield return new WaitForSeconds(slowDuration);
        _speed = defaultSpeed;
    }

    protected virtual void ReachedEnd()
    {
        nextNodeIndex = 0;
        Die();
    }

    protected virtual void Die()
    {
        MapManager.instance.EnemyDied(this);
        GameManager.instance.Currency = _lootValue + GameManager.instance.Currency;
        Destroy(gameObject);
        return;
    }
}