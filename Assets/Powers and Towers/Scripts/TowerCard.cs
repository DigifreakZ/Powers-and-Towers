using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TowerData towerData;
    private bool holdingCard;
    public void SetTower(Vector3 pos)
    {
        towerData.SetTower(pos);
        
    }

    private void Update()
    {
        if (holdingCard)
        {
            print("Hello " + name);
        }
        //    Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdingCard = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        holdingCard = false;
    }
}
