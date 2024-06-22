using System;
using System.Collections.Generic;
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

    public static event Action keySetting;

    public Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    private void Start()
    {
        ServerManager.instance.StartConnection();
        InitializationBindings();
    }

    private void InitializationBindings()
    {
        keyBindings["Up"] = KeyCode.W;
        keyBindings["Down"] = KeyCode.S;
        keyBindings["Left"] = KeyCode.A;
        keyBindings["Right"] = KeyCode.D;
        keyBindings["Interaction"] = KeyCode.G;
        keyBindings["Run"] = KeyCode.LeftShift;
        keyBindings["Attack"] = KeyCode.Mouse0;
        keyBindings["Craft"] = KeyCode.B;
        keyBindings["Map"] = KeyCode.M;
        keyBindings["Inventory"] = KeyCode.I;
        keyBindings["Slot_01"] = KeyCode.Alpha1;
        keyBindings["Slot_02"] = KeyCode.Alpha2;
        keyBindings["Slot_03"] = KeyCode.Alpha3;
        keyBindings["Slot_04"] = KeyCode.Alpha4;
        keyBindings["Slot_05"] = KeyCode.Alpha5;
        keyBindings["Slot_06"] = KeyCode.Alpha6;
        keyBindings["Escape"] = KeyCode.Escape;
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