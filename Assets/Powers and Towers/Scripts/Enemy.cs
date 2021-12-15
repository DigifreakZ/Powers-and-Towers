using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private float _lootValue = 1f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform[] _nodes;
    [SerializeField] Rigidbody2D rb;
    private int index = 0;
    public virtual int Health 
    { 
        get 
        {
            return _health;
        }
        set
        {
            if (value >= 0)
            {
                Die();
            }
            _health = value;
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
        Vector3 triangle = _nodes[index].position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
        Physics2D.IgnoreLayerCollision(6, 6);
    }
    private void FixedUpdate()
    {
        if (_nodes != null)
        {
            if ((transform.position - _nodes[index].position).sqrMagnitude < 0.1 * 0.1)
            {
                if (index < _nodes.Length - 1)
                    index += 1;
                else
                    ReachedEnd();

                Vector3 triangle = _nodes[index].position - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg);
            }
            rb.position = Vector3.MoveTowards(transform.position, _nodes[index].position, _speed * Time.fixedDeltaTime);
        }
    }
    protected virtual void ReachedEnd()
    {
        index = 0;
    }

    protected virtual void Die()
    {
        return;
    }

}
