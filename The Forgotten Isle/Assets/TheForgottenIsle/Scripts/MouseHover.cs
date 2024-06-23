using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage hoverImage;   //버튼 옆 이미지 
    public float offsetX = -100f; //버튼 이미지 사이 거리 
    // Start is called before the first frame update
    void Start()
    {
        //게임 시작 화살표 비활성화
        hoverImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스 버튼시 활성화
        hoverImage.gameObject.SetActive(true);

        RectTransform buttonRect = GetComponent<RectTransform>();
        Vector3 imagePosition = buttonRect.position;

        // 버튼의 바로 왼쪽에 Raw Image를 위치시키기
        imagePosition.x += buttonRect.rect.width;
        // 버튼의 왼쪽으로 이동
        imagePosition.x += offsetX;
        hoverImage.transform.position = imagePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //마우스 떼지면 비활성화 
        hoverImage.gameObject.SetActive(false);
    }
}
