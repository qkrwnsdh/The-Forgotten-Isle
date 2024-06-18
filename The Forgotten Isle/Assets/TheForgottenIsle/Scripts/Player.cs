using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : NetworkBehaviour
{
    public NetworkVariable<Vector3> pos = new NetworkVariable<Vector3>();

    private void Update()
    {
        if (IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 5.0f;
            transform.Translate(move);
            pos.Value = transform.position;

            if (!NetworkObject.IsOwner)
            {
                UpdatePositionServer(transform.position);
            }
            
        }
        else
        {
            transform.position = pos.Value;
        }
    }

    [ServerRpc]
    private void UpdatePositionServer(Vector3 targetPos)
    {
        if (IsServer)
        {
            UpdatePositionClient(targetPos);
        }
    }

    [ClientRpc]
    private void UpdatePositionClient(Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}
