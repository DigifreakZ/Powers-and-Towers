using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    hitCollider.GetComponent<Enemy>()
    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("AddDamage");
        }
    }
}
