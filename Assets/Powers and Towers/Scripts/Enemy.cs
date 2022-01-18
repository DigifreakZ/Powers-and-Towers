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
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DamageType[] resistances;
    [SerializeField] private DamageType[] weakness;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool DebugDummy;
    private int nextNodeIndex = 0;
    private List<float> slowEffects;
    private EnemyData data;
    public EnemyData EnemyData
    {
        get { return data; }
        set
        {
            if (data != value) data = value;
            else return;
            Init();
        }
    }
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
        _renderer = GetComponent<SpriteRenderer>();
        slowEffects = new List<float>();
        if (!DebugDummy)
        {
            Init();
        }
    }

    public void Init()
    {
        _nodes = MapManager.GetPath();
        if (_nodes == null) Destroy(gameObject);
        Vector3 triangle = _nodes[nextNodeIndex].position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
        Physics2D.IgnoreLayerCollision(6, 6);
        if (data != null)
        {
            transform.localScale = data.Scale;
            _health = data.health;
            _speed = data.speed;
            _lootValue = data.lootValue;
            if (_renderer != null)_renderer.sprite = data.spriteImage;
            resistances = data.resistances;
            weakness = data.weakness;
        }

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
        foreach(DamageType _type in weakness)
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

    private void FindBiggestSlow(List<float> effectList)
    {
        float totalSlow = 0;
        if (effectList.Count > 0)
        {
            try
            {
                foreach(float slowPercentage in effectList)
                {
                    if (slowPercentage > totalSlow)
                        totalSlow = slowPercentage;
                };
            }
            catch{}
        }

        _speed = data.speed * Mathf.Clamp(1 - totalSlow,0,1f);
    }

    private IEnumerator GetSlowedRoutine(float slowAmount, float slowDuration)
    {
        slowEffects.Add(slowAmount);
        FindBiggestSlow(slowEffects);
        yield return new WaitForSeconds(slowDuration);
        slowEffects.Remove(slowAmount);
        FindBiggestSlow(slowEffects);
    }

    protected virtual void ReachedEnd()
    {
        nextNodeIndex = 0;
        if (!DebugDummy)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        MapManager.instance.EnemyDied(this);
        GameManager.instance.Currency = _lootValue + GameManager.instance.Currency;
        try
        {
            Destroy(gameObject);
        }
        catch { }
        return;
    }
}