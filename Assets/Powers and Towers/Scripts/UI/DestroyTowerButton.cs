using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DestroyTowerButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Tower selectedTower;
    private Color _color;
    private void Start()
    {
        TowerSelector.instance.destroyButton = this;
    }
    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
    }
    // Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        _color = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = _color;
    }

    // Clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectedTower != null) selectedTower.DestroyTower(true);
    }
}

