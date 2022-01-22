using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerCardDeckSelecter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Tooltip("Determines what Tower the card is holding")]
    [SerializeField] private TowerData towerData;
    public TowerData TowerData { set { towerData = value; UpdateVisuals(); } private get => towerData; }
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
        cardCostText.text = towerData.cardCost.ToString();
        cardNameText.text = towerData.cardName.ToString();
        cardDamageText.text = towerData.damage.ToString();
        cardSpeedText.text = (1 / towerData.attackCooldown).ToString();
        cardRangeText.text = towerData.range.ToString();
        cardElementText.text = towerData.type.ToString();
        cardTowerImage.sprite = towerData.spriteImage;
    }

    private void Start()
    {
        if (towerData != null)
        UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.currentHoldID = towerData.cardID;
        GameManager.instance.holdingCard = true;
        print("Down");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.instance.hoveringDropCard)
        {
            GameManager.instance.DeckCards[GameManager.instance.scriptToAdd.holderID] = towerData.cardID;
        }
        GameManager.instance.holdingCard = false;
        GameManager.instance.currentHoldID = -1;
        print("Up");
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
