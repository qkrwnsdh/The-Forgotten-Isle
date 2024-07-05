using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSign : MonoBehaviour
{
    public GameObject hiddenImage; // 드래그 앤 드롭으로 설정할 숨겨진 이미지 오브젝트
    public Button showButton;      // 드래그 앤 드롭으로 설정할 버튼

    void Start()
    {
        // 버튼 클릭 시 실행될 메서드 연결
        showButton.onClick.AddListener(ShowImage);
    }

    void ShowImage()
    {
        // 숨겨진 이미지 오브젝트 활성화
        hiddenImage.SetActive(true);
    }
}
