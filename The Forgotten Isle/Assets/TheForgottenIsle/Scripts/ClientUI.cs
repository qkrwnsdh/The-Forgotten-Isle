using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeUGUI;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject setting;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider foodSlider;

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
    #region Button
    public void ButtonInventory()
    {
        Debug.Log("¿©±â µé¾î¿È?");
        Inventory();
    }

    public void ButtonSetting()
    {

    }
    #endregion

    #region Interaction
    public void Map()
    {
        if (map.activeSelf) { map.SetActive(false); }
        else { map.SetActive(true); }
    }

    public void Inventory()
    {
        if (inventory.activeSelf) { inventory.SetActive(false); }
        else { inventory.SetActive(true); }
    }

    public void Setting()
    {
        if (map.activeSelf) { map.SetActive(false); }
        else if (inventory.activeSelf) { inventory.SetActive(false); }
        else if (setting.activeSelf) { setting.SetActive(false); }
        else { setting.SetActive(true); }
    }

    public void HpSlider(float value)
    {
        hpSlider.value = value;
    }

    public void FoodSlider(float value)
    {
        foodSlider.value = value;
    }
    #endregion
}