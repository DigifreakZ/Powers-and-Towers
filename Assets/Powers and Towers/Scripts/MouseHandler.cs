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
    private GameObject rangeIndicatorInstance;
    private void Start()
    {
        instance = this;
        image = GetComponent<Image>();
    }
    
    public void Show(TowerData tower)
    {
        float scaleValue = tower.range * 2;

        Vector3 scale = new Vector2(scaleValue, scaleValue);
        if (rangeIndicatorInstance == null)
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab);
        rangeIndicatorInstance.transform.localScale = scale;
        rangeIndicatorInstance.SetActive(true);
    }
    public void Hide()
    {
        if (rangeIndicatorInstance == null)
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab, transform);
        rangeIndicatorInstance.SetActive(false);
    }
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
        if (rangeIndicatorInstance != null)
            rangeIndicatorInstance.transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + Vector3.forward * 10; 
    }
    internal void SetDefaultMouse()
    {
        image.enabled = false;
    }
}
