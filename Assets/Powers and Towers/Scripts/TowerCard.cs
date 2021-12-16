using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TowerData towerData;
    public LayerMask mLayerMask;
    private bool holdingCard;
    public void SetTower(Vector3 pos)
    {
        towerData.SetTower(pos);
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
            //if (objs != null)
            //print(objs.name);
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
}
