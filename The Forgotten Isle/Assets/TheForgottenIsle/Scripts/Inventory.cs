using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private enum type
    {
        WEAPON,
        INGREDIENT,
        EXPENDABLES
    }

    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject[] quickSlots;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject dragItem;

    private bool isDragging = false;
    private GraphicRaycaster graphicRay;
    private int layerItem;

    private GameObject currentSlot;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        { UseItem(0); }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        { UseItem(1); }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        { UseItem(2); }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        { UseItem(3); }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        { UseItem(4); }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        { UseItem(5); }
    }

    private void UseItem(int number)
    {
    }

    #region Initialization and Setup
    private void Start()
    {
        InitializationComponents();
        InitializationSetups();
    }

    private void InitializationComponents()
    {
        graphicRay = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
    }

    private void InitializationSetups()
    {
        layerItem = LayerMask.NameToLayer("Item");
    }
    #endregion

    #region Handler
    public void OnPointerDown(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        graphicRay.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == layerItem)
            {
                currentSlot = result.gameObject;

                ItemSwap(result.gameObject.transform, dragItem.transform);

                SetDraggedPosition(eventData);
                dragItem.SetActive(true);
                isDragging = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            SetDraggedPosition(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            List<RaycastResult> results = new List<RaycastResult>();

            graphicRay.Raycast(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer == layerItem)
                {
                    ItemSwap(result.gameObject.transform, dragItem.transform);
                }
            }

            ItemSwap(currentSlot.gameObject.transform, dragItem.transform);

            isDragging = false;
            dragItem.SetActive(false);
        }
    }

    private void ItemSwap(Transform parent1, Transform parent2)
    {
        Transform child1_1 = parent1.GetChild(0);
        Transform child1_2 = parent1.GetChild(1);
        Transform child2_1 = parent2.GetChild(0);
        Transform child2_2 = parent2.GetChild(1);

        child1_1.SetParent(parent2);
        child2_1.SetParent(parent1);
        child1_2.SetParent(parent2);
        child2_2.SetParent(parent1);

        Vector3 tempPosition = child1_1.position;

        child1_1.position = child2_1.position;
        child2_1.position = tempPosition;

        tempPosition = child1_2.position;

        child1_2.position = child2_2.position;
        child2_2.position = tempPosition;
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragItem.transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        dragItem.GetComponent<RectTransform>().localPosition = localPoint;
    }
    #endregion


}