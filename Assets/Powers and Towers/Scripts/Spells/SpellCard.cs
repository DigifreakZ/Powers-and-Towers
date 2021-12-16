using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpellCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private SpellData spellData;
    public SpellEffectManager effectManager;
    [SerializeField]
    private float spellLevel;
    [SerializeField]
    private TextMeshProUGUI spellNameField;
    [SerializeField]
    private TextMeshProUGUI spellCostField;
    [SerializeField]
    private Image spellIcon;
    [SerializeField]
    private TextMeshProUGUI spellDescriptionField;
    private bool load;
    private bool grabbed;

    void OnEnable()
    {
        load = true;
        effectManager = FindObjectOfType<SpellEffectManager>();
        spellLevel = 1f;
        spellNameField.text = spellData.spellNameLevel1;
        spellCostField.text = spellData.spellCost.ToString();
        spellIcon.sprite = spellData.spellIconLevel1;
        spellDescriptionField.text = spellData.spellDescription;
    }

    void Update()
    {
        if (grabbed)
        {
            transform.position = Mouse.current.position.ReadValue();
        }
    }

    private void LateUpdate()
    {
        if (load)
        {
            spellIcon.SetNativeSize();
        }
    }

    public void LevelUp()
    {
        if (spellLevel < 3)
        {
            spellLevel++;
            if (spellLevel == 2)
            {
                spellNameField.text = spellData.spellNameLevel2;
                spellIcon.sprite = spellData.spellIconLevel2;
            }
            else if (spellLevel == 3)
            {
                spellNameField.text = spellData.spellNameLevel3;
                spellIcon.sprite = spellData.spellIconLevel3;
            }
        }
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
            LevelUp();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        grabbed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        effectManager.CastSpell(1);
        grabbed = false;
        Destroy(gameObject);
    }
}
