using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage hoverImage;   //��ư �� �̹��� 
    public float offsetX = -100f; //��ư �̹��� ���� �Ÿ� 
    // Start is called before the first frame update
    void Start()
    {
        //���� ���� ȭ��ǥ ��Ȱ��ȭ
        hoverImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //���콺 ��ư�� Ȱ��ȭ
        hoverImage.gameObject.SetActive(true);

        RectTransform buttonRect = GetComponent<RectTransform>();
        Vector3 imagePosition = buttonRect.position;

        // ��ư�� �ٷ� ���ʿ� Raw Image�� ��ġ��Ű��
        imagePosition.x += buttonRect.rect.width;
        // ��ư�� �������� �̵�
        imagePosition.x += offsetX;
        hoverImage.transform.position = imagePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //���콺 ������ ��Ȱ��ȭ 
        hoverImage.gameObject.SetActive(false);
    }
}
