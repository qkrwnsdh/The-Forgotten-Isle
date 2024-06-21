using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Start()
    {
        ServerManager.instance.StartConnection();
    }

    // "M" Ű�� �۵��ϴ� ��
    public void ToggleMap()
    { 
    
    }

    // "ESC" Ű�� �۵��ϴ� â
    public void ToggleBoard()
    { 
    
    }

    private void InGameTime()
    { 
    //if()
    }
}