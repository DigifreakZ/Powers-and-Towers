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
    private SpellData spellData;
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
    private List<SpellData> wildMagicTarget;

    public SpellData SpellData
    {
        get
        {
            return spellData;
        }
        set
        {
            spellData = value;
            DisplayData();
        }
    }
    public void DisplayData()
    {
        load = true;
        spellNameField.text = SpellData.spellNames[spellLevel];
        spellCostField.text = SpellData.cardCost.ToString();
        spellIcon.sprite = SpellData.spellIcons[spellLevel];
        spellDescriptionField.text = SpellData.cardDescription;
        spellVisualEffect = SpellData.visualEffect;
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
                targetInRange = Physics2D.OverlapCircle(mousePos, SpellData.spellRange, SpellData.target) != null;
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
            if (spellData.wildMagic)
            {
                wildMagicTarget = new List<SpellData>();
                foreach (var item in GetComponentInParent<SpellCardArea>().spellsInDeck)
                {
                    if (!item.wildMagic)
                    {
                        wildMagicTarget.Add(item);
                    }
                }
            }
        }
    }

    public void LevelUp()
    {
        if (spellLevel < 2)
        {
            spellLevel++;
            spellNameField.text = SpellData.spellNames[spellLevel];
            spellIcon.sprite = SpellData.spellIcons[spellLevel];
        }
    }

    private Vector2 CurrentSpellRangeScale()
    {
        float scale = (SpellData.spellRange + spellLevel * SpellData.spellRangeLevelUp) * 2;
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
        if (GameManager.instance.Currency >= SpellData.cardCost)
        {
            GameManager.instance.Currency -= SpellData.cardCost;
            Vector2 castPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (spellData.wildMagic)
            {
                SpellData = wildMagicTarget[UnityEngine.Random.Range(0,wildMagicTarget.Count)];
            }
            SpellData.Cast(spellLevel, castPoint);
            grabbed = false;
            targetCircle.transform.position = new Vector3(0, 0, -10);
            SendMessageUpwards("UpdateHand", gameObject);
            transform.position = new Vector3(10000, 10000, 0);
            if (SpellData.continousSpellEffect)
            {
                StartCoroutine(ContinousEffectRoutine(SpellData.spellDuration, castPoint));
            }
            else
            {
                StartCoroutine(SpawnVisual(castPoint, SpellData.continousSpellEffect));
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
        StartCoroutine(SpawnVisual(castPoint, SpellData.continousSpellEffect));
        for (int i = 0; i < duration; i++)
        {
            SpellData.Cast(spellLevel, castPoint);
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
            duration = SpellData.spellDuration;
        }
        else
        {
            duration = (int)SpellData.VisualEffectDuration;
        }
        yield return new WaitForSeconds(duration);
        Destroy(tmpVisual);
        Destroy(gameObject);
    }

    private void MergeSpell()
    {
        grabbed = false;
        targetCircle.transform.position = new Vector3(0, 0, -10);
        if ( mergeTarget != null && mergeTarget.spellLevel == spellLevel && (mergeTarget.SpellData == SpellData || spellData.wildMagic) && spellLevel < 2 )
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
