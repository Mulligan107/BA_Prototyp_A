using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    
    private Text _tooltipText;
    private RectTransform _tooltipBackgroundRect;
    private RectTransform _parentRect;
    private bool _initialized;

    private void Awake()
    {
        Init();
        HideTooltip();
    }

    private void Init()
    {
        if (_initialized) return;
        _tooltipBackgroundRect =  transform.Find("Background").GetComponent<RectTransform>();
        _tooltipText = transform.Find("TooltipText").GetComponent<Text>();
        _parentRect = transform.parent.GetComponent<RectTransform>();
        _initialized = true;
    }

    private void Update()
    {
        if (Mouse.current == null) return;
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRect, mousePos, uiCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }

    public void ShowTooltip(string text)
    {
        Init();
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        const float padding = 4f;
        _tooltipText.text = text;
        _tooltipBackgroundRect.sizeDelta = new Vector2(
            _tooltipText.preferredWidth + padding * 2f,
            _tooltipText.preferredHeight + padding * 2f);
    }

    public void HideTooltip() => gameObject.SetActive(false);
}
