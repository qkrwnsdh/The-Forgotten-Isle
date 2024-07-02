using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOption : MonoBehaviour
{
    public GameObject optionImage; // 옵션 버튼을 눌렀을 때 활성화할 이미지
    public GameObject[] initialObjects; // 이미지 안의 버튼을 눌렀을 때 전환할 오브젝트들
    public Button optionButton; // 옵션 버튼
    public Button exitButton; // 엑시트 버튼
    public Button[] imageButtons; // 이미지 안에 있는 버튼들

    private GameObject currentActiveObject;

    void Start()
    {
        // 옵션 버튼 클릭 이벤트에 리스너 추가
        if (optionButton != null)
        {
            optionButton.onClick.AddListener(OnOptionButtonClick);
        }

        // 이미지 내의 버튼들 클릭 이벤트에 리스너 추가
        foreach (Button button in imageButtons)
        {
            button.onClick.AddListener(delegate { OnImageButtonClick(button); });
        }

        // 초기 상태로 옵션 이미지를 비활성화
        optionImage.SetActive(false);

        // 초기 상태로 모든 오브젝트를 비활성화
        foreach (GameObject obj in initialObjects)
        {
            obj.SetActive(false);
        }
    }

    void OnOptionButtonClick()
    {
        // 옵션 이미지를 활성화
        optionImage.SetActive(true);
    }

    void OnImageButtonClick(Button clickedButton)
    {
        // 현재 활성화된 오브젝트를 비활성화
        if (currentActiveObject != null)
        {
            currentActiveObject.SetActive(false);
        }

        // 클릭된 버튼에 할당된 오브젝트를 찾아서 활성화
        for (int i = 0; i < imageButtons.Length; i++)
        {
            if (imageButtons[i] == clickedButton && initialObjects.Length > i)
            {
                initialObjects[i].SetActive(true);
                currentActiveObject = initialObjects[i];
                break;
            }
        }
    }
    void OnExitButtonClick()
    {
        // 옵션 이미지와 그 안의 모든 오브젝트를 비활성화
        optionImage.SetActive(false);

        // 현재 활성화된 오브젝트를 비활성화
        if (currentActiveObject != null)
        {
            currentActiveObject.SetActive(false);
            currentActiveObject = null;
        }
    }

}
