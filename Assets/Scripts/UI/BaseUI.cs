using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [Header("Base References")]
    [SerializeField] private Health _blueBaseHealth;
    [SerializeField] private Health _redBaseHealth;

    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI _blueHPText;
    [SerializeField] private TextMeshProUGUI _redHPText;

    private void Start()
    {
        if (_blueBaseHealth != null)
        {
            _blueBaseHealth.onChanged += UpdateBlueHP;
            UpdateBlueHP(_blueBaseHealth.GetHealth());
        }

        if (_redBaseHealth != null)
        {
            _redBaseHealth.onChanged += UpdateRedHP;
            UpdateRedHP(_redBaseHealth.GetHealth());
        }
    }

    private void OnDestroy()
    {
        if (_blueBaseHealth != null) _blueBaseHealth.onChanged -= UpdateBlueHP;
        if (_redBaseHealth != null) _redBaseHealth.onChanged -= UpdateRedHP;
    }

    private void UpdateBlueHP(int currentHealth)
    {
        if (_blueHPText != null)
        {
            _blueHPText.text = $"Blue Base HP: {currentHealth}";
        }
    }

    private void UpdateRedHP(int currentHealth)
    {
        if (_redHPText != null)
        {
            _redHPText.text = $"Red Base HP: {currentHealth}";
        }
    }
}