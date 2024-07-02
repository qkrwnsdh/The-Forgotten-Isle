using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene(string PlayScene)
    {
        SceneManager.LoadScene(PlayScene);
    }
    
}
