using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Tooltip tooltip;
    
    private Button _button;
    private UpgradeData _data;
    private Action<UpgradeData> _onPicked;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);
    }
    
    private void OnDestroy() => _button.onClick.RemoveListener(HandleClick);

    public void Bind(UpgradeData data, Action<UpgradeData> onPicked)
    {
        _data = data;
        _onPicked = onPicked;
        titleText.text = data.title;
        descriptionText.text = data.description;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_data == null) return;
        tooltip.ShowTooltip(_data.GetTooltip());
    }

    public void OnPointerExit(PointerEventData eventData) => tooltip.HideTooltip();
    
    private void HandleClick() => _onPicked?.Invoke(_data);
}
   