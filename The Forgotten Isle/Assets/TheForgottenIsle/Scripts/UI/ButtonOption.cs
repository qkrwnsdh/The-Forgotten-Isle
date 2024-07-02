using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOption : MonoBehaviour
{
    public GameObject optionImage; // �ɼ� ��ư�� ������ �� Ȱ��ȭ�� �̹���
    public GameObject[] initialObjects; // �̹��� ���� ��ư�� ������ �� ��ȯ�� ������Ʈ��
    public Button optionButton; // �ɼ� ��ư
    public Button exitButton; // ����Ʈ ��ư
    public Button[] imageButtons; // �̹��� �ȿ� �ִ� ��ư��

    private GameObject currentActiveObject;

    void Start()
    {
        // �ɼ� ��ư Ŭ�� �̺�Ʈ�� ������ �߰�
        if (optionButton != null)
        {
            optionButton.onClick.AddListener(OnOptionButtonClick);
        }

        // �̹��� ���� ��ư�� Ŭ�� �̺�Ʈ�� ������ �߰�
        foreach (Button button in imageButtons)
        {
            button.onClick.AddListener(delegate { OnImageButtonClick(button); });
        }

        // �ʱ� ���·� �ɼ� �̹����� ��Ȱ��ȭ
        optionImage.SetActive(false);

        // �ʱ� ���·� ��� ������Ʈ�� ��Ȱ��ȭ
        foreach (GameObject obj in initialObjects)
        {
            obj.SetActive(false);
        }
    }

    void OnOptionButtonClick()
    {
        // �ɼ� �̹����� Ȱ��ȭ
        optionImage.SetActive(true);
    }

    void OnImageButtonClick(Button clickedButton)
    {
        // ���� Ȱ��ȭ�� ������Ʈ�� ��Ȱ��ȭ
        if (currentActiveObject != null)
        {
            currentActiveObject.SetActive(false);
        }

        // Ŭ���� ��ư�� �Ҵ�� ������Ʈ�� ã�Ƽ� Ȱ��ȭ
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
        // �ɼ� �̹����� �� ���� ��� ������Ʈ�� ��Ȱ��ȭ
        optionImage.SetActive(false);

        // ���� Ȱ��ȭ�� ������Ʈ�� ��Ȱ��ȭ
        if (currentActiveObject != null)
        {
            currentActiveObject.SetActive(false);
            currentActiveObject = null;
        }
    }

}
