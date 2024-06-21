using System.Net.Sockets;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<Vector3> pos = new NetworkVariable<Vector3>();
    private Vector3 lastServerPos;
    private readonly float SPEED = 5.0f;

    #region Initialization and Setup
    private void Start()
    {
        InitializationSetup();
    }

    private void InitializationSetup()
    {
        if (IsOwner)
        {
            foreach (Transform childTransform in transform)
            { childTransform.gameObject.SetActive(true); }
        }
    }
    #endregion

    private void Update()
    {
        if (IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * SPEED;

            transform.Translate(move);

            Vector3 predictedPos = transform.position + move;

            UpdatePositionServerRpc(predictedPos);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lastServerPos, Time.deltaTime * 100f);
        }
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