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

    private void UpdateTime(int newTime)
    {
        timeUGUI.text = $"{newTime}";
    }
}
