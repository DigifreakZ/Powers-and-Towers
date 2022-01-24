using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeckBuilderHandSpell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int holderID;
    [Tooltip("Determines what Spell the card is holding")]
    [SerializeField] private SpellData cardData;
    public SpellData CardData { set { cardData = value; UpdateVisuals(); } private get => cardData; }
    [Tooltip("Layers Tower can't be placed on")]
    [SerializeField] private TextMeshProUGUI cardCostText;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private Image cardSpellImage;
    private UITweener tweener;
    private bool load = false;

    public void initiziate(int TowerCardID)
    {
        if (GameManager.instance.cardDatabase.cardData[TowerCardID].GetType() == typeof(SpellData))
            CardData = (SpellData)GameManager.instance.cardDatabase.cardData[TowerCardID];
        else
        {
            // Destroy(gameObject);
        }
        //UpdateVisuals();
    }
    /// <summary>
    /// Updates Visuals of Card to corresponding TowerData
    /// </summary>
    private void UpdateVisuals()
    {
        cardCostText.text = cardData.cardCost.ToString();
        cardNameText.text = cardData.cardName.ToString();
        cardDescription.text = cardData.cardDescription.ToString();
        cardSpellImage.sprite = cardData.cardImage;
        load = true;
    }

    private void Start()
    {
        CardData = (SpellData)GameManager.GetHandS(holderID);
    }

    private void LateUpdate()
    {
        if (load)
        {
            cardSpellImage.SetNativeSize();
            load = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.instance.hoverID = holderID;
        if (GameManager.instance.holdingCard)
        {
            GameManager.instance.SpellToAdd = this;
            GameManager.instance.hoveringDropCard = true;
            initiziate(GameManager.instance.currentHoldID);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        initiziate(GameManager.GetHandS(holderID).cardID);
        GameManager.instance.hoveringDropCard = false;
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
