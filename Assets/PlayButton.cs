using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerDownHandler
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
    public void AutoPlayOn()
    {
        print(autoplay);
        SpriteRenderer.sprite = playSprite;
        MapManager.instance.nextWave = autoplay;
    }
    public void AutoPlayOff()
    {
        print(autoplay);
        SpriteRenderer.sprite = stopSprite;
        MapManager.instance.nextWave = autoplay;
    }
    private void Start()
    {
        AutoPlayOn();
    }
    public void OnPointerDown(PointerEventData eventData)
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
