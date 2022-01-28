using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class MouseHandler : MonoBehaviour
{
    public static MouseHandler instance;
    public Image image;
    [SerializeField] private Sprite defaultMouse;
    [SerializeField] private GameObject rangeIndicatorPrefab;
    [SerializeField] private Sprite DefaultTowerBase;
    private GameObject rangeIndicatorInstance;
    public Transform towerPreviewBody;
    public SpriteRenderer towerPreviewBodyRender;
    public Transform towerPreviewHead;
    public SpriteRenderer towerPreviewHeadRender;
    private void Start()
    {
        instance = this;
        image = GetComponent<Image>();
        towerPreviewBody = Instantiate(rangeIndicatorPrefab).transform;
        towerPreviewBodyRender = towerPreviewBody.GetComponent<SpriteRenderer>();
        towerPreviewHead = Instantiate(rangeIndicatorPrefab,Vector3.zero,Quaternion.identity, towerPreviewBody).transform;
        towerPreviewHeadRender = towerPreviewHead.GetComponent<SpriteRenderer>();
        towerPreviewHeadRender.sortingOrder = 1;
        towerPreviewBody.gameObject.SetActive(false);
    }
    
    public void Show(TowerData tower)
    {
        // Tower Visuals
        SpriteRenderer body = towerPreviewBody.GetComponent<SpriteRenderer>();
        SpriteRenderer head = towerPreviewHead.GetComponent<SpriteRenderer>();
        body.color = body.color + new Color(0, 0, 0, 1f);
        head.color = head.color + new Color(0, 0, 0, 1f);
        float scaleValue = tower.range;
        if (tower.Towerbase != null)
            body.sprite = tower.Towerbase;
        else
        {
            body.sprite = DefaultTowerBase;
        }

        if (tower.Head != null)
            head.sprite = tower.Head;
        else
        {
            head.sprite = tower.TowerPrefab.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }



        // Tower Range Indication
        Vector3 scale = new Vector2(scaleValue, scaleValue);
        if (rangeIndicatorInstance == null)
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab);
        rangeIndicatorInstance.transform.localScale = scale * 2;
        rangeIndicatorInstance.SetActive(true);
        towerPreviewBody.gameObject.SetActive(true);
    }
    public void Hide()
    {
        if (rangeIndicatorInstance == null)
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab, transform);
        rangeIndicatorInstance.SetActive(false);
        towerPreviewBody.gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();

        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + Vector3.forward * 10;
        if (towerPreviewBody != null)
            towerPreviewBody.position = mousePositionWorld;
        if (rangeIndicatorInstance != null)
            rangeIndicatorInstance.transform.position = mousePositionWorld;
    }

    internal void SetDefaultMouse()
    {
        image.enabled = false;
    }
}
