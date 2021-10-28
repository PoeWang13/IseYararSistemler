using System;
using UnityEngine;

public class SpriteButton_World : MonoBehaviour
{
    public Action OnClick;
    public Action OnEnter;
    public Action OnExit;
    private void OnMouseUpAsButton()
    {
        OnClick?.Invoke();
    }
    private void OnMouseEnter()
    {
        OnEnter?.Invoke();
    }
    private void OnMouseExit()
    {
        OnExit?.Invoke();
    }
}