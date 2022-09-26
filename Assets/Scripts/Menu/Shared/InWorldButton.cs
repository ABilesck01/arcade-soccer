using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

public class InWorldButton : MonoBehaviour
{
    public Image gfx;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;
    public ButtonOnInteractEvent onInteractEvent;
    [Space]
    [TextArea(3,10)] public string text;

    [SerializeField] public TMPro.TextMeshProUGUI textMeshPro;

    private void OnValidate()
    {
        if(textMeshPro != null)
            textMeshPro.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(hoverSprite != null)
            {
                gfx.sprite = hoverSprite;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (normalSprite != null)
            {
                gfx.sprite = normalSprite;
            }
        }
    }

    public void InvokeInteraction()
    {
        if(pressedSprite != null)
            gfx.sprite = pressedSprite;

        onInteractEvent?.Invoke();

        if (normalSprite != null)
        {
            gfx.sprite = normalSprite;
        }
    }
}

[Serializable]
public class ButtonOnInteractEvent : UnityEvent { }
