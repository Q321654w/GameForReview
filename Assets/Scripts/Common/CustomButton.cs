using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action Pressed;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            Pressed?.Invoke();
        }
    }
}