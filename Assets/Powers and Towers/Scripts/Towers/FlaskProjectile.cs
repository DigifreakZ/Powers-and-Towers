using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskProjectile : Projectile
{
    protected float procentToTarget = 0f;
    protected Vector3 start;
    protected Vector3 end;
    protected float dotTimer;
    protected bool flaskBroken;
    protected List<Collider2D> colliders;
    public void Init(int damage, DamageType type ,float dotTimer, Vector3 thrownPosition)
    {
        base.Init(damage, type);
        this.dotTimer = dotTimer;
        start = transform.position;
        end = thrownPosition;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    public override void Update()
    {
        base.Update();
        if (!iniziated) return;
        float speed = Mathf.Sin(Mathf.Lerp(0, 3.14f, procentToTarget)) + 0.5f;
        if (procentToTarget + Time.deltaTime * speed >= 1f)
        {
            procentToTarget = 1f;
        }
        else 
        {
            procentToTarget += Time.deltaTime * speed * 5f;
        }

        transform.localScale = Vector3.one * speed;
        transform.position = Vector3.Lerp(start, end, procentToTarget);
        if (procentToTarget == 1f && !flaskBroken)
        {
            flaskBroken = true;
            Splash();
        }
    }

    protected void Splash()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0);
        Collider2D[] hitCollider =
            Physics2D.OverlapCircleAll
            (
                transform.position,
                0.5f,
                1 << 7
            );
        colliders = new List<Collider2D>();
        colliders.AddRange(hitCollider);
        if (colliders != null && !(colliders.Count <= 0))
        {
            for(int i = 0; i < colliders.Count; i++)
            {
                Enemy EM = colliders[i].GetComponent<Enemy>();
                EM.StartDOTRoutine(damage,dotTimer,damageType);
            }
        }
    }
}
