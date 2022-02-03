using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite stopSprite;
    private Image spriteRenderer;
    private bool autoplay;
    private Image SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<Image>();
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }
    private void AutoPlayOn()
    {
        SpriteRenderer.sprite = playSprite;
        MapManager.instance.nextWave = autoplay;
    }
    private void AutoPlayOff()
    {
        SpriteRenderer.sprite = stopSprite;
        MapManager.instance.nextWave = autoplay;
    }
    private void Start()
    {
        AutoPlayOff();
    }
    public void ButtonDown()
    {
        autoplay = !autoplay;
        if (autoplay)
        {
            AutoPlayOn();
        }
        else
        {
            AutoPlayOff();
        }
    }
}
