using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region Members

    #region Attribute Members
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject[] quickSlots;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject dragItem;
    #endregion

    #region Private Members
    private bool isDragging = false;
    private GraphicRaycaster graphicRay;
    private int layerItem;
    private GameObject currentSlot;
    #endregion

    #endregion

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
        List<RaycastResult> results = PerformRaycast(eventData);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == layerItem)
            {
                currentSlot = result.gameObject;

                ItemSwap(result.gameObject.transform, dragItem.transform);

                SetDraggedPosition(eventData);
                SetActiveDrag(true);

                break;
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
            List<RaycastResult> results = PerformRaycast(eventData);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer == layerItem)
                {
                    ItemSwap(result.gameObject.transform, dragItem.transform);

                    break;
                }
            }

            ItemSwap(currentSlot.gameObject.transform, dragItem.transform);
            SetActiveDrag(false);
        }
    }

    private List<RaycastResult> PerformRaycast(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRay.Raycast(eventData, results);
        return results;
    }

    private void SetActiveDrag(bool value)
    {
        isDragging = value;
        dragItem.SetActive(value);
    }

    private void ItemSwap(Transform parent1, Transform parent2)
    {
        Transform child1 = parent1.GetChild(0);
        Transform child2 = parent2.GetChild(0);

        child1.SetParent(parent2);
        child2.SetParent(parent1);

        Vector3 tempPosition = child1.position;
        child1.position = child2.position;
        child2.position = tempPosition;
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