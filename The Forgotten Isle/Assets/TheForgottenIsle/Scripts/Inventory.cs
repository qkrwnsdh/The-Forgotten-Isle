using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private enum type
    {
        WEAPON,
        INGREDIENT,
        EXPENDABLES
    }

    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject description;

    #region Inventory Interaction
    private void Start()
    {

    }

    #endregion

    #region Item Interaction
    private void UseItem()
    {

    }
    #endregion

    // 아이템 슬롯
    // 아이템 넣기
    // 아이템 위치 바꾸기
    // 아이템 빼기
}
