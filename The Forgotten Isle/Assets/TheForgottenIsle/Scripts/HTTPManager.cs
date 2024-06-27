using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Account
{
    public string id;
    public string password;

    public Account(string id, string password)
    {
        this.id = id;
        this.password = password;
    }
}

public class HTTPManager : MonoBehaviour
{
    #region Singleton and Awake()
    public static HTTPManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private Coroutine postCoroutine;
    private Coroutine getCoroutine;
    private Coroutine updateCoroutine;
    private Coroutine deleteCoroutine;

    #region POST

    #region Account
    public void PostAccount(string url, string id, string password)
    {
        if (postCoroutine != null)
        {
            message.text = "In progress...";

            return;
        }

        Account signUpData = new Account(id, password);
        string jsonData = JsonUtility.ToJson(signUpData);

        postCoroutine = StartCoroutine(PostCoroutine(url, jsonData));
    }

    private IEnumerator PostCoroutine(string url, string data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        { message.text = "Sign up successful!"; }
        else
        { message.text = "Sign up failed: " + request.error; }
    }
    #endregion

    #endregion

    #region GET
    public void GetData(string url, string data)
    {
        if (getCoroutine != null)
        {
            message.text = "In progress...";

            return;
        }

        getCoroutine = StartCoroutine(GetCoroutine(url, data));
    }

    private IEnumerator GetCoroutine(string url, string data)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + "/" + "member_id");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        { message.text = $"{request.downloadHandler.text}"; }
        else
        { message.text = "Sign up failed: " + request.error; }

        getCoroutine = null;
    }
    #endregion

    #region UPDATE
    public void UpdateSign(string url, string id, string currentPassword, string newPassword)
    {
        if (updateCoroutine != null)
        {
            message.text = "In progress...";

            return;
        }

        //UpdateData updateData = new UpdateData(id, currentPassword, newPassword);

        //string jsonData = JsonUtility.ToJson(updateData);

        //updateCoroutine = StartCoroutine(UpdateCoroutine(url, jsonData));
    }

    private IEnumerator UpdateCoroutine(string url, string data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "PUT");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);

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

        updateCoroutine = null;
    }
    #endregion

    #region DELETE
    public void DeleteData(string url, string id)
    {
        if (deleteCoroutine != null)
        {
            message.text = "In progress...";

            return;
        }

        string deleteUrl = $"{url}/{id}";

        deleteCoroutine = StartCoroutine(DeleteCoroutine(deleteUrl));
    }

    private IEnumerator DeleteCoroutine(string url)
    {
        UnityWebRequest request = UnityWebRequest.Delete(url);

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            message.text = "Delete successful!";
        }
        else
        {
            message.text = "Delete failed: " + request.error;
        }

        deleteCoroutine = null;
    }
    #endregion

    public TextMeshProUGUI message;
}