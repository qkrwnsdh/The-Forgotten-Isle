using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<Vector3> pos = new NetworkVariable<Vector3>();
    private Vector3 lastServerPos;

    private void Update()
    {
        if (IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 5.0f;
            transform.Translate(move);

            // 클라이언트에서 즉시 보여주기 위해 예측 위치를 사용합니다.
            Vector3 predictedPos = transform.position + move;

            // 서버에 위치 업데이트를 요청합니다.
            UpdatePositionServerRpc(predictedPos);
        }
        else
        {
            // 서버에서 받은 최신 위치로 보간하여 부드러운 움직임을 만듭니다.
            transform.position = Vector3.Lerp(transform.position, lastServerPos, Time.deltaTime * 10f);
        }
    }

    [ServerRpc]
    private void UpdatePositionServerRpc(Vector3 targetPos)
    {
        pos.Value = targetPos;

        // 모든 클라이언트에게 위치 업데이트를 알립니다.
        UpdatePositionClientRpc(targetPos);
    }

    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 targetPos)
    {
        // 클라이언트는 서버에서 받은 위치를 최신화합니다.
        lastServerPos = targetPos;
    }
}