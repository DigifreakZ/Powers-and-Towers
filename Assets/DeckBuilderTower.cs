using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeckBuilderTower : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Tooltip("Determines what Tower the card is holding")]
    [SerializeField] private TowerData cardData;
    public TowerData TowerData { set { cardData = value; UpdateVisuals(); } private get => cardData; }
    [Tooltip("Layers Tower can't be placed on")]
    [SerializeField] private TextMeshProUGUI cardCostText;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDamageText;
    [SerializeField] private TextMeshProUGUI cardSpeedText;
    [SerializeField] private TextMeshProUGUI cardRangeText;
    [SerializeField] private TextMeshProUGUI cardElementText;
    [SerializeField] private Image cardTowerImage;
    private UITweener tweener;
    private bool holdingCard;
    public void initiziate(int TowerCardID)
    {
        if (GameManager.instance.cardDatabase.cardData[TowerCardID].GetType() == typeof(TowerData))
            TowerData = (TowerData)GameManager.instance.cardDatabase.cardData[TowerCardID];
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
        cardDamageText.text = cardData.damage.ToString();
        cardSpeedText.text = (1 / cardData.attackCooldown).ToString();
        cardRangeText.text = cardData.range.ToString();
        cardElementText.text = cardData.type.ToString();
        cardTowerImage.sprite = cardData.spriteImage;
    }

    private void Update()
    {
        if (!holdingCard || MouseHandler.instance == null) return;
        MouseHandler m = MouseHandler.instance;

        if (!m.image.enabled) m.image.enabled = true;

        if (m.image.sprite != cardData.spriteImage)
            m.image.sprite = cardData.spriteImage;
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
            GameManager.instance.DeckCards[GameManager.instance.TowerToAdd.holderID] = cardData.cardID;
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
