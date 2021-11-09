using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
    public event Action Pressed;
        
    [SerializeField] private Text _text;
    public Text Text => _text;

    public void OnPointerClick(PointerEventData eventData)
    {
        Pressed?.Invoke();
    }
}