using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private ClientUI clientUI;

    public NetworkVariable<Vector3> pos = new NetworkVariable<Vector3>();
    private Vector3 lastServerPos;
    private readonly float SPEED = 5.0f;

    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    #region Initialization and Setup
    private void Start()
    {
        InitializationSetup();
        UpdateKeySetting();
        InitializationEvents();
    }

    private void InitializationSetup()
    {
        if (IsOwner)
        {
            foreach (Transform childTransform in transform)
            { childTransform.gameObject.SetActive(true); }
        }
    }

    private void InitializationEvents()
    {
        GameManager.keySetting += UpdateKeySetting;
    }
    #endregion

    private void UpdateKeySetting()
    {
        if (IsOwner)
        {
            keyBindings = GameManager.instance.keyBindings;
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            #region InputKey
            // ¿Ãµø
            if (Input.GetKey(keyBindings["Up"]) || Input.GetKey(keyBindings["Down"]) ||
                Input.GetKey(keyBindings["Left"]) || Input.GetKey(keyBindings["Right"])) { MoveKey(); }
            // ªÛ»£¿€øÎ
            if (Input.GetKey(keyBindings["Interaction"])) { InteractionKey(); }
            // ¥ﬁ∏Æ±‚
            if (Input.GetKey(keyBindings["Run"])) { RunKey(); }
            // ∞¯∞›
            if (Input.GetKey(keyBindings["Attack"])) { AttackKey(); }
            // ¡¶¿€
            if (Input.GetKey(keyBindings["Craft"])) { CraftKey(); }
            // ∏ 
            if (Input.GetKeyUp(keyBindings["Map"])) { MapKey(); }
            // ¿Œ∫•≈‰∏Æ
            if (Input.GetKeyUp(keyBindings["Inventory"])) { InventoryKey(); }
            // ΩΩ∑‘ 01
            if (Input.GetKey(keyBindings["Slot_01"])) { SlotKey(1); }
            // ΩΩ∑‘ 02
            if (Input.GetKey(keyBindings["Slot_02"])) { SlotKey(2); }
            // ΩΩ∑‘ 03
            if (Input.GetKey(keyBindings["Slot_03"])) { SlotKey(3); }
            // ΩΩ∑‘ 04
            if (Input.GetKey(keyBindings["Slot_04"])) { SlotKey(4); }
            // ΩΩ∑‘ 05
            if (Input.GetKey(keyBindings["Slot_05"])) { SlotKey(5); }
            // ΩΩ∑‘ 06
            if (Input.GetKey(keyBindings["Slot_06"])) { SlotKey(6); }
            // ∞‘¿” ∏ﬁ¥∫
            if (Input.GetKeyUp(keyBindings["Escape"])) { EscapeKey(); }
            #endregion
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lastServerPos, Time.deltaTime * 100f);
        }
    }

    private void MoveKey()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(keyBindings["Up"])) { move += Vector3.forward; }
        if (Input.GetKey(keyBindings["Down"])) { move += Vector3.back; }
        if (Input.GetKey(keyBindings["Left"])) { move += Vector3.left; }
        if (Input.GetKey(keyBindings["Right"])) { move += Vector3.right; }

        move *= Time.deltaTime * SPEED;

        transform.Translate(move);

        Vector3 predictedPos = transform.position + move;

        UpdatePositionServerRpc(predictedPos);
    }

    private void InteractionKey()
    {

    }

    private void RunKey()
    {

    }

    private void AttackKey()
    {

    }

    private void CraftKey()
    {

    }

    private void MapKey()
    {
        clientUI.Map();
    }

    private void InventoryKey()
    {
        clientUI.Inventory();
    }

    private void SlotKey(int slotNumber)
    {

    }

    private void EscapeKey()
    {
        clientUI.Setting();
    }

    #region RPC
    [ServerRpc]
    private void UpdatePositionServerRpc(Vector3 targetPos)
    {
        pos.Value = targetPos;

        UpdatePositionClientRpc(targetPos);
    }

    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 targetPos)
    {
        lastServerPos = targetPos;
    }
    #endregion
}