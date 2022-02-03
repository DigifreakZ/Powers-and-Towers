using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingArrow : MonoBehaviour
{
     Vector3 start;
    [SerializeField] Vector3 end;
    private bool forward;
    private float procent;
    private void Start()
    {
        start = transform.position;
        end = start + end;
        MapManager.instance.StarthelpIndicator.Add(this);
    }
    void Update()
    {
        if (procent == 0f || procent == 1f)
        {
            forward = !forward;
        }

        if (forward)
            procent += Time.deltaTime;
        else
            procent -= Time.deltaTime;

        procent = Mathf.Clamp(procent, 0f, 1f);

        transform.position = Vector3.Lerp(start, end,procent);
    }
    public void DestroyMe()
    {
        MapManager.instance.StarthelpIndicator.Remove(this);
        Destroy(gameObject);
    }
}
