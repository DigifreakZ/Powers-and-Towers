using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Tower Data")]
    [SerializeField] private TowerData towerData;
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
    private void Awake()
    {
        cardCostText.text = towerData.cardCost.ToString();
        cardNameText.text = towerData.cardName.ToString();
        cardDamageText.text = towerData.damage.ToString();
        cardSpeedText.text = (1/towerData.attackCooldown).ToString();
        cardRangeText.text = towerData.range.ToString();
        cardElementText.text = towerData.type.ToString();
        cardTowerImage.sprite = towerData.spriteImage;
    }
    private void Update()
    {
        if (!holdingCard || MouseHandler.instance == null) return;
        
        MouseHandler m = MouseHandler.instance;

        if (!m.image.enabled) m.image.enabled = true;

        if (m.image.sprite != towerData.spriteImage)
            m.image.sprite = towerData.spriteImage;

        if (ViablePlacementArea) m.image.color = Color.white;
        else                     m.image.color = Color.red;
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
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        holdingCard = false;
        if (ViablePlacementArea)
        {
            towerData.SetTower(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0, 0, 10));
        }
        if (MouseHandler.instance == null) return;
        MouseHandler.instance.SetDefaultMouse();
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
}