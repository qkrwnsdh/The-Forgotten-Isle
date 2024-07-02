using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;

    public bool isEmpty;
    public int count;

    public void AddItem()
    {
        isEmpty = true;
    }

    public void RemoveItem()
    {
        isEmpty = false;
    }

    // TODO: ������ ���� ������ �ִ� �ּҰ� ����
    public void AddItemCount(int value) => count += value;
    public void RemoveItemCount(int value) => count -= value;

    public void UseItem()
    {
        if (isEmpty) { return; }
    }
}