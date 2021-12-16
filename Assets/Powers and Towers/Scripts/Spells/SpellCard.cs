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
    private int spellLevel = 0;
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
        spellNameField.text = spellData.spellNames[spellLevel];
        spellCostField.text = spellData.spellCost.ToString();
        spellIcon.sprite = spellData.spellIcons[spellLevel];
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
        if (spellLevel < spellData.spellNames.Length)
        {
            spellLevel++;
            spellNameField.text = spellData.spellNames[spellLevel];
            spellIcon.sprite = spellData.spellIcons[spellLevel];

        }
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
        {
            LevelUp();
            print("Card Leveled up!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        grabbed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        spellData.Cast(spellLevel);
        grabbed = false;
        Destroy(gameObject);
    }
}
