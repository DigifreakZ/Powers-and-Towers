using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class SpellCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SpellData spellData;
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
    public Transform targetCircle;
    private bool targetInRange;
    private SpellCard mergeTarget;

    void OnEnable()
    {
        //DisplayData();
    }

    public void DisplayData()
    {
        load = true;
        spellNameField.text = spellData.spellNames[spellLevel];
        spellCostField.text = spellData.cardCost.ToString();
        spellIcon.sprite = spellData.spellIcons[spellLevel];
        spellDescriptionField.text = spellData.cardDescription;
    }

    void Update()
    {
        if (grabbed)
        {
            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y, 0);
            targetCircle.transform.position = mousePos;
            targetInRange = Physics2D.OverlapCircle(mousePos, spellData.spellRange, spellData.target) != null;
            if (targetInRange)
            {
                targetCircle.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                targetCircle.GetComponent<SpriteRenderer>().color = Color.red;
            }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        grabbed = true;
        targetCircle.transform.localScale = new Vector2((spellData.spellRange + spellLevel * 0.5f) * 2, (spellData.spellRange + spellLevel * 0.5f) * 2);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CastSpell();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        SendMessageUpwards("ViewCard", gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
        if (!grabbed)
        {
            SendMessageUpwards("NormalHandPosition");
        }
    }

    private void CastSpell()
    {
        spellData.Cast(spellLevel);
        grabbed = false;
        targetCircle.transform.position = new Vector3(0, 0, -10);
        SendMessageUpwards("UpdateHand", gameObject);
        Destroy(gameObject);
    }

    private void MergeSpell()
    {
        if (mergeTarget.spellLevel == spellLevel && mergeTarget.spellData == spellData)
        {
            mergeTarget.LevelUp();
            grabbed = false;
            targetCircle.transform.position = new Vector3(0, 0, -10);
            SendMessageUpwards("UpdateHand", gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Merge target not valid");
            ReturnToPosition();
        }
    }

    private void ReturnToPosition()
    {
        throw new NotImplementedException();
    }
}
