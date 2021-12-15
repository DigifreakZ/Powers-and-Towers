using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TowerData towerData;
    [SerializeField] private UITweener tweener;
    private bool holdingCard;
    private bool HoldingCard
    {
        set
        {
            if (tweener != null) tweener.animate = value;
            holdingCard = value;
        }
        get => holdingCard;
    }
    public void SetTower(Vector3 pos)
    {
        towerData.SetTower(pos);
    }

    private void Awake()
    {
        tweener = GetComponent<UITweener>();
    }
    // Sets tower att mouse Position
    // towerData.SetTower(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0, 0, 10));
    private void Update()
    {
        if (holdingCard)
        {
            transform.position = Mouse.current.position.ReadValue();
        }
        //    Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool ViablePlacementArea
    {
        get
        {
            Collider2D objs;
            objs = Physics2D.OverlapBox(Mouse.current.position.ReadValue(), Vector2.one, 0,6);
            if (objs != null)
            print(objs.name);
            return objs == null;
            //RaycastHit2D hit = Physics2D.BoxCast(Mouse.current.position.ReadValue(),
            //    Vector2.one,
            //    0,
            //    Vector2.zero,
            //    100,
            //    6);
            //if (hit)
            //print(hit.collider.gameObject.name);
            //return !hit;
        }
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        HoldingCard = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        HoldingCard = false;
        tweener.StartCoroutine("GoToStart");
        if (ViablePlacementArea)
        {
            towerData.SetTower(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0, 0, 10));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(Mouse.current.position.ReadValue(), Vector3.one);
    }
}
