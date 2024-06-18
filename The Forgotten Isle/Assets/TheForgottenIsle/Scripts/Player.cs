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

            // Ŭ���̾�Ʈ���� ��� �����ֱ� ���� ���� ��ġ�� ����մϴ�.
            Vector3 predictedPos = transform.position + move;

            // ������ ��ġ ������Ʈ�� ��û�մϴ�.
            UpdatePositionServerRpc(predictedPos);
        }
        else
        {
            // �������� ���� �ֽ� ��ġ�� �����Ͽ� �ε巯�� �������� ����ϴ�.
            transform.position = Vector3.Lerp(transform.position, lastServerPos, Time.deltaTime * 10f);
        }
    }

    [ServerRpc]
    private void UpdatePositionServerRpc(Vector3 targetPos)
    {
        pos.Value = targetPos;

        // ��� Ŭ���̾�Ʈ���� ��ġ ������Ʈ�� �˸��ϴ�.
        UpdatePositionClientRpc(targetPos);
    }

    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 targetPos)
    {
        // Ŭ���̾�Ʈ�� �������� ���� ��ġ�� �ֽ�ȭ�մϴ�.
        lastServerPos = targetPos;
    }
}