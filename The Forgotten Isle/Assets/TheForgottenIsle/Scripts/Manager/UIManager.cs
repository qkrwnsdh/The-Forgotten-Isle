using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Enum
    private enum SceneType
    {
        LOBBYSCENE,
        PLAYSCENE
    }
    #endregion

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region Members

    #region Attribute Members

    #endregion

    #region Private Members

    #endregion

    #endregion

    #region Initialization and Setup
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "LobbyScene":

                break;
            case "PlayScene":

                break;
        }
    }
    #endregion

    private string inputId;
    private string inputPassword;

    #region Button
    public void ButtonInputId(TMP_InputField inputField) => inputId = inputField.text;
    public void ButtonnInPutPassword(TMP_InputField inputField) => inputPassword = inputField.text;
    public void ButtonSign() => HTTPManager.instance.PostAccount(Define.URL_JOIN, inputId, inputPassword);
    public void ButtonDelete() => HTTPManager.instance.DeleteData(Define.URL_JOIN, inputId);
    public void ButtonGet() => HTTPManager.instance.GetData(Define.URL_JOIN, inputId);
    //public void ButtonUpdate() => HTTPManager.instance.UpdateSign(Define.URL_JOIN, inputId);
    /* �ۼ���
    public void ButtonSinglePlay() => default;
    public void ButtonMultiPlay() => default;
    public void ButtonOption() => default;
    public void ButtonExit() => Application.Quit();
    */

    // Junoh 2024-06-20
    /*
    public void ButtonMap() => default;
    public void ButtonResume() => default;
    */
    #endregion
}