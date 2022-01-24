using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeckBuilderSpell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Tooltip("Determines what Tower the card is holding")]
    [SerializeField] private SpellData cardData;
    public SpellData TowerData { set { cardData = value; UpdateVisuals(); } private get => cardData; }
    [Tooltip("Layers Tower can't be placed on")]
    [SerializeField] private TextMeshProUGUI cardCostText;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private Image cardSpellImage;
    private UITweener tweener;
    private bool holdingCard;
    public void initiziate(int TowerCardID)
    {
        if (GameManager.instance.cardDatabase.cardData[TowerCardID].GetType() == typeof(SpellData))
            TowerData = (SpellData)GameManager.instance.cardDatabase.cardData[TowerCardID];
        else
        {
            Destroy(gameObject);
        }
        UpdateVisuals();
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
    }

    private void Update()
    {
        if (!holdingCard || MouseHandler.instance == null) return;
        MouseHandler m = MouseHandler.instance;

        if (!m.image.enabled) m.image.enabled = true;

        if (m.image.sprite != cardData.cardImage)
            m.image.sprite = cardData.cardImage;
    }

    private void Start()
    {
        if (cardData != null)
            UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.currentHoldID = cardData.cardID;
        GameManager.instance.holdingCard = true;
        holdingCard = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.instance.hoveringDropCard)
        {
            bool duplicate = false;
            for(int i = 0; i < GameManager.instance.SpellCards.Length; i++)
            {
                if(GameManager.instance.SpellCards[i] == cardData.cardID)
                {
                    duplicate = true;
                    break;
                }
            }
            if (!duplicate)
            GameManager.instance.SpellCards[GameManager.instance.SpellToAdd.holderID] = cardData.cardID;
        }
        GameManager.instance.holdingCard = false;
        GameManager.instance.currentHoldID = -1;
        holdingCard = false;
        MouseHandler m = MouseHandler.instance;

        if (m.image.enabled) m.image.enabled = false;
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

}
