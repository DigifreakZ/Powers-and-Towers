using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector instance;
    public UpgradeTowerButton upgradeButton;
    public DestroyTowerButton destroyButton;
    [SerializeField] private GameObject rangeIndicatorPrefab;
    private Transform selectedTowerRangeIndictor;
    private UITweener tweener;
    public Transform SelectedTowerRangeIndictor
    {
        set
        {
            selectedTowerRangeIndictor = value;
        }
        get
        {
            if (selectedTowerRangeIndictor == null)
            {
                selectedTowerRangeIndictor = Instantiate(rangeIndicatorPrefab,new Vector3(0,-1000,0),Quaternion.identity).transform;
            }
            return selectedTowerRangeIndictor;
        }
    }
    private Tower selectedTower;
    private Tower SelectedTower
    {
        get
        {
            return selectedTower;
        }
        set
        {
            if (selectedTower != null)
                selectedTower.Deselect();
            selectedTower = value;
            if (selectedTower != null)
                selectedTower.Select();
        }
    }
    public UITweener Tweener => tweener;

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
            if (raycastHit_Game && raycastHit_Game.transform.gameObject.layer != 6 && !IsPointerOverUIObject())
            {
                if (raycastHit_Game.collider.transform.gameObject.layer == 8)
                {
                    if (raycastHit_Game.collider.transform.gameObject.GetComponent<Tower>().Data != null)
                    {
                        SelectedTower = raycastHit_Game.collider.transform.gameObject.GetComponent<Tower>();
                        SelectTower(ref selectedTower);
                    }
                }
            }
            else if (!IsPointerOverUIObject())
            {
                tweener.Off();
                SelectedTower = null;
            }
        }
    }

    public void SelectTower(ref Tower tower)
    {
        upgradeButton.SelectTower(tower);
        destroyButton.SelectTower(tower); 
        tweener.On();
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
