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

    // "M" 키로 작동하는 맵
    public void ToggleMap()
    { 
    
    }

    // "ESC" 키로 작동하는 창
    public void ToggleBoard()
    { 
    
    }

    private void InGameTime()
    { 
    //if()
    }
}