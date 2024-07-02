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

    // TODO: 아이템 갯수 수정시 최대 최소값 조정
    public void AddItemCount(int value) => count += value;
    public void RemoveItemCount(int value) => count -= value;

    public void UseItem()
    {
        if (isEmpty) { return; }
    }
}