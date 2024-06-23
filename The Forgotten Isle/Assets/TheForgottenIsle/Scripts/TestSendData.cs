using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



//[System.Serializable]
//public class SignUpData
//{
//    public string id;
//    public string password;

//    public SignUpData(string id, string password)
//    {
//        this.id = id;
//        this.password = password;
//    }
//}

public class TestSendData : MonoBehaviour
{
    private enum HTTP
    {
        POST,
        GET,
        UPDATE,
        DELETE
    }

    private readonly string url = "http://localhost:8080";

    private void SendPostRequest(string url, object obj, Action<UnityWebRequest> callback)
    {

    }

    private void SendGetRequest(string url, object obj,)
    {

    }

    private IEnumerator CosendWebRequest(string url, string method, object obj, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{_baseUrl}/{url}/";

        byte[] jsonByte = null;

        if (obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonByte = Encoding.UTF8.GetBytes(jsonStr);
        }
        var uwr = new UnityWebRequest(sendUrl, method);

        uwr.uploadHandler = new UploadHandlerRaw(jsonByte);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(uwr.result);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            callback.Invoke(uwr);
        }
    }





    public TMP_InputField idInput;
    public TMP_InputField passwordInput;
    public Button send;

    public TextMeshProUGUI message;


    private void Start()
    {
        send.onClick.AddListener(SendButtonClicked);
    }

    private void SendButtonClicked()
    {
        if (string.IsNullOrEmpty(url))
        {
            message.text = "Please save the URL first.";
            return;
        }

        string id = idInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            message.text = "Please enter both ID and password.";
            return;
        }

        SignUpData signUpData = new SignUpData(id, password);
        StartCoroutine(SendSignUpRequest(signUpData));
    }

    private IEnumerator SendSignUpRequest(SignUpData signUpData)
    {
        string jsonData = JsonUtility.ToJson(signUpData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            message.text = "Sign up successful!";
        }
        else
        {
            message.text = "Sign up failed: " + request.error;
        }
    }
}