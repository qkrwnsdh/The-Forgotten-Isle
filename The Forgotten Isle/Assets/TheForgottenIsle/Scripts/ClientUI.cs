using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeUGUI;

    private void OnEnable()
    {
        ServerManager.setTimeEvent += UpdateTime;
    }

    private void OnDisable()
    {
        ServerManager.setTimeEvent -= UpdateTime;
    }

    private void UpdateTime(int setHour, int setMinute)
    {
        timeUGUI.text = $"{setHour:D2}:{setMinute:D2}";
    }
}