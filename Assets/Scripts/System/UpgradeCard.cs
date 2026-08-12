using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    
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
    
    private void HandleClick() => _onPicked?.Invoke(_data);
}
   