using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector instance;
    public UpgradeTowerButton upgradeButton;
    public DestroyTowerButton destroyButton;
    private UITweener tweener;

    private void Awake()
    {
        if (!tweener) tweener = GetComponent<UITweener>();
        TowerSelector.instance = this;
    }

    private void Update()
    {
        if (Mouse.current.press.wasPressedThisFrame)
        {
            RaycastHit2D raycastHit_Game =
                Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())
                    + new Vector3(0, 0, 10),
                    Vector2.zero
                );
            if (raycastHit_Game && !IsPointerOverUIObject())
            {
                if (raycastHit_Game.collider.transform.gameObject.layer == 8)
                {
                    upgradeButton.SelectTower(raycastHit_Game.collider.transform.gameObject.GetComponent<Tower>());
                    destroyButton.SelectTower(raycastHit_Game.collider.transform.gameObject.GetComponent<Tower>());
                    tweener.On();
                }
            }
            else if (!IsPointerOverUIObject())
            {
                tweener.Off();
            }
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
