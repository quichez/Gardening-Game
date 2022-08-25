using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralStatusPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dateText;
    [SerializeField] TextMeshProUGUI _tempText;
    [SerializeField] TextMeshProUGUI _humidText;
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Start()
    {
        UpdateStatusPanel();
        Calendar.Instance?.SubscribeToGameTickEvents(UpdateStatusPanel);
    }

    private void OnEnable()
    {
        Calendar.Instance?.SubscribeToGameTickEvents(UpdateStatusPanel);
    }

    private void OnDisable()
    {
        // Unsubscribe to this --> Calendar.Instance.SubscribeToGameTickEvents(UpdateStatusPanel);
    }

    private void UpdateStatusPanel()
    {
        ChangeDateText();
        ChangeTempText();
        ChangeHumidityText();
    }

    private void ChangeDateText() => _dateText.text = Calendar.Instance.date.ToString();
    private void ChangeTempText() 
    {
        _tempText.color = Weather.Instance.IsHeatWave ? Color.red + Color.yellow : Weather.Instance.IsColdSnap ? Color.blue + Color.white / 2.0f : Color.white;
        _tempText.text = Weather.Instance.currentTemperature + "°F";
    }

    private void ChangeHumidityText()
    {
        _humidText.color = Color.black + (Color.white * (1 - Weather.Instance.currentHumidity));
        _humidText.text = (Weather.Instance.currentHumidity * 100.0f).ToString("F0") + " %";
    }

    public void UpdateMoneyText()
    {
        _moneyText.text = string.Format("{0:C2}", Money.Instance.Balance);
    }
}
