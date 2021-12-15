using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellEffectManager : MonoBehaviour
{
    public LayerMask enemy;
    public void CastSpell(float spellID)
    {
        switch (spellID)
        {
            case 1:
                { 
                    print("Fireball!");
                    Physics2D.OverlapCircle(Mouse.current.position.ReadValue(), 5f);
                
                }
                break;
            default:
                break;
        }
    }
}
