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
    private void Start()
    {
        instance = this;
        image = GetComponent<Image>();
    }
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    internal void SetDefaultMouse()
    {
        image.enabled = false;
    }
}
