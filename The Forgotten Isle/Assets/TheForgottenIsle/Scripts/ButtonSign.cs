using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSign : MonoBehaviour
{
    public GameObject hiddenImage; // �巡�� �� ������� ������ ������ �̹��� ������Ʈ
    public Button showButton;      // �巡�� �� ������� ������ ��ư

    void Start()
    {
        // ��ư Ŭ�� �� ����� �޼��� ����
        showButton.onClick.AddListener(ShowImage);
    }

    void ShowImage()
    {
        // ������ �̹��� ������Ʈ Ȱ��ȭ
        hiddenImage.SetActive(true);
    }
}
