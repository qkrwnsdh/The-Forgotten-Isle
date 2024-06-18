using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    private void Update()
    {
        if (IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 5.0f;
            transform.Translate(move);
            Position.Value = transform.position;
        }
        else
        {
            transform.position = Position.Value;
        }
    }
}
