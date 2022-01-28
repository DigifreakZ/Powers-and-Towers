using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int cardHolderID = -1;
    [Tooltip("Determines what Tower the card is holding")]
    [SerializeField] private TowerData towerData;
    public TowerData TowerData { set { towerData = value; UpdateVisuals(); } private get => towerData; }
    [Tooltip("Layers Tower can't be placed on")]
    public LayerMask mLayerMask;
    [SerializeField] private TextMeshProUGUI cardCostText;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDamageText;
    [SerializeField] private TextMeshProUGUI cardSpeedText;
    [SerializeField] private TextMeshProUGUI cardRangeText;
    [SerializeField] private TextMeshProUGUI cardElementText;
    [SerializeField] private Image cardTowerImage;
    private UITweener tweener;
    private bool holdingCard;
    public void SetTower(Vector3 pos)
    {
        towerData.SetTower(pos);
    }
    private void Start()
    {
        if (cardHolderID == -1) Debug.LogError("TowerCard has no CardHolderId: TowerCard.CS");
        TowerData = (TowerData)GameManager.GetHand(cardHolderID);
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
    private void Update()
    {
        if (!holdingCard || MouseHandler.instance == null) return;
        
        MouseHandler mouse = MouseHandler.instance;

        if (ViablePlacementArea && GameManager.instance.Currency >= towerData.cardCost && !IsPointerOverUIObject())
        {
            mouse.towerPreviewBodyRender.color = Color.white;
            mouse.towerPreviewHeadRender.color = Color.white;
        }
        else if (IsPointerOverUIObject())
        {
            mouse.towerPreviewBodyRender.color = new Color(0, 0, 0, 0);
            mouse.towerPreviewHeadRender.color = new Color(0, 0, 0, 0);
        }
        else
        {
            mouse.towerPreviewBodyRender.color = Color.red;
            mouse.towerPreviewHeadRender.color = Color.red;
        }
    }

    // Sets tower att mouse Position
    // towerData.SetTower(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0, 0, 10));
    
    /// <summary>
    /// Returns true if an object is blocking placement of Tower
    /// </summary>
    private bool ViablePlacementArea
    {
        get
        {
            Collider2D objs;
            objs = Physics2D.OverlapBox
            (
                Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0,0,10),
                towerData.TowerPrefab.transform.localScale * 0.8f,
                0f,
                mLayerMask
            );
            // if there is an object att location Return true
            return objs == null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdingCard = true;
        MouseHandler.instance.Show(towerData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        holdingCard = false;
        if (ViablePlacementArea && GameManager.instance.Currency >= towerData.cardCost && !IsPointerOverUIObject())
        {
            GameManager.instance.Currency -= towerData.cardCost;
            towerData.SetTower(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0, 0, 10));
        }
        if (MouseHandler.instance == null) return;
        MouseHandler.instance.SetDefaultMouse();
        MouseHandler.instance.Hide();
    }
    public void LevelUp()
    {

    }

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
    //    {
    //        LevelUp();
    //        print("Level Up");
    //    }
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tweener == null) tweener = GetComponent<UITweener>();
        tweener.On();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tweener == null) tweener = GetComponent<UITweener>();
        tweener.Off();
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