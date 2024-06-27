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
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject dragItem;

    private bool isDragging = false;
    private Image dragItemImage;
    private TextMeshProUGUI dragItemText;
    private GraphicRaycaster graphicRay;
    private int layerItem;

    private void Start()
    {
        InitializationComponents();
        InitializationSetup();
    }

    private void InitializationComponents()
    {
        graphicRay = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        dragItemImage = dragItem.transform.GetChild(0).GetComponent<Image>();
        dragItemText = dragItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void InitializationSetup()
    {
        layerItem = LayerMask.NameToLayer("ItemLayer");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        graphicRay.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == 10)
            {
                Debug.Log(result.gameObject.name);

                Image targetImage = result.gameObject.transform.GetChild(0).GetComponent<Image>();
                TextMeshProUGUI targetUGUI = result.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                dragItemImage.color = targetImage.color;
                dragItemText.text = targetUGUI.text;

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
            isDragging = false;
            dragItem.SetActive(false);
        }
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
}