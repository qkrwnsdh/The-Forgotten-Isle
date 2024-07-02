using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;

    private string loginUrl = "http://localhost:1435/login";

    public void OnLoginButtonClicked()
    {
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(loginUrl, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            resultText.text = "Login successful";
        }
        else
        {
            resultText.text = "Login failed: " + www.error;
        }
    }
}
