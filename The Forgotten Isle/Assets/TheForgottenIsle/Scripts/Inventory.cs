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

    // ������ ����
    // ������ �ֱ�
    // ������ ��ġ �ٲٱ�
    // ������ ����
}
