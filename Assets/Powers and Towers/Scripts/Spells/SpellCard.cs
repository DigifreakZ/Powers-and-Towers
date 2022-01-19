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
    private GameObject spellVisualEffect;
    private bool load;
    private bool grabbed;
    public Transform targetCircle;
    private bool targetInRange;
    [HideInInspector] public SpellCard mergeTarget;
    private Vector3 returnPosition;

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
        spellVisualEffect = spellData.visualEffect;
    }

    void Update()
    {
        if (grabbed)
        {
            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y, 0);
            if (IsPointerOverUIObject())
            {
                targetCircle.transform.position = new Vector3(0, 0, -10);
            }
            else
            {
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
        if (spellLevel < 2)
        {
            spellLevel++;
            spellNameField.text = spellData.spellNames[spellLevel];
            spellIcon.sprite = spellData.spellIcons[spellLevel];
        }
    }

    private Vector2 CurrentSpellRangeScale()
    {
        float scale = (spellData.spellRange + spellLevel * spellData.spellRangeLevelUp) * 2;
        return new Vector2(scale, scale);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        returnPosition = transform.localPosition;
        grabbed = true;
        SendMessageUpwards("GrabbedCard", this);
        targetCircle.transform.localScale = CurrentSpellRangeScale();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsPointerOverUIObject())
        {
            MergeSpell();
        }
        else
        {
            CastSpell();
        }
        SendMessageUpwards("ResetGrab");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Enter");
        SendMessageUpwards("ViewCard", gameObject);
        SendMessageUpwards("MergeTarget", this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
        if (!grabbed)
        {
            SendMessageUpwards("NormalHandPosition");
        }
    }

    private void CastSpell()
    {
        grabbed = false;
        targetCircle.transform.position = new Vector3(0, 0, -10);
        if (GameManager.instance.Currency >= spellData.cardCost)
        {
            GameManager.instance.Currency -= spellData.cardCost;
            Vector2 castPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            spellData.Cast(spellLevel, castPoint);
            grabbed = false;
            targetCircle.transform.position = new Vector3(0, 0, -10);
            SendMessageUpwards("UpdateHand", gameObject);
            transform.position = new Vector3(10000, 10000, 0);
            if (spellData.continousSpellEffect)
            {
                StartCoroutine(ContinousEffectRoutine(spellData.spellDuration, castPoint));
            }
            else
            {
                StartCoroutine(SpawnVisual(castPoint, spellData.continousSpellEffect));
            }
        }
        else
        {
            Debug.Log("Not enough currency");
            StartCoroutine(ReturnToPosition());
        }
        
    }

    private IEnumerator ContinousEffectRoutine(int duration, Vector2 castPoint)
    {
        StartCoroutine(SpawnVisual(castPoint, spellData.continousSpellEffect));
        for (int i = 0; i < duration; i++)
        {
            spellData.Cast(spellLevel, castPoint);
            print(i);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator SpawnVisual(Vector2 castPoint, bool continous)
    {
        GameObject tmpVisual = Instantiate(spellVisualEffect, castPoint, Quaternion.identity);
        tmpVisual.transform.localScale = CurrentSpellRangeScale();
        int duration;
        if (continous)
        {
            duration = spellData.spellDuration;
        }
        else
        {
            duration = (int)spellData.VisualEffectDuration;
        }
        yield return new WaitForSeconds(duration);
        Destroy(tmpVisual);
        Destroy(gameObject);
    }

    private void MergeSpell()
    {
        grabbed = false;
        targetCircle.transform.position = new Vector3(0, 0, -10);
        if ( mergeTarget != null && mergeTarget.spellLevel == spellLevel && mergeTarget.spellData == spellData && spellLevel < 2 )
        {
            mergeTarget.LevelUp();
            SendMessageUpwards("UpdateHand", gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Merge target not valid");
            StartCoroutine(ReturnToPosition());
        }
    }
    IEnumerator ReturnToPosition()
    {
        while (transform.localPosition.y < returnPosition.y)
        {
            float step = 256 * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, returnPosition, step);
            yield return null;
        }
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
