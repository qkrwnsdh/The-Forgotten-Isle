using System;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    #region Singleton
    public static ServerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        networkManager = NetworkManager.Singleton;

        networkManager.NetworkConfig.ConnectionApproval = true;
    }
    #endregion

    private NetworkManager networkManager;

    private int currentConnection = 0;

    private readonly int MAX_CONNECTION = 4;

    private int inGameTime;

    public static event Action<int> setTimeEvent;

    private void Start()
    {
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    // Ŭ���̾�Ʈ ���� �� ȣ��Ǵ� �ݹ�
    private void OnClientConnected(ulong clientId)
    {
        if (networkManager.IsServer)
        {
            currentConnection += 1;
            UpdateConnectionCountClientRpc(currentConnection);
        }
    }

    // Ŭ���̾�Ʈ ���� ���� �� ȣ��Ǵ� �ݹ�
    private void OnClientDisconnected(ulong clientId)
    {
        if (networkManager.IsServer)
        {
            currentConnection -= 1;
            UpdateConnectionCountClientRpc(currentConnection);
        }
    }

    // ���� ����
    public void StartConnection()
    {
        if (GetConnectionApproval() == false)
        {
            // UIȣ�� 
            return;
        }

        if (currentConnection == 0)
        {
            networkManager.StartHost();
            Debug.Log("ȣ��Ʈ�� ������ �����մϴ�.");

            StartTime();
        }
        else
        {
            networkManager.StartClient();
            Debug.Log("Ŭ���̾�Ʈ�� ������ �����մϴ�.");
        }

        networkManager.OnClientConnectedCallback += OnClientConnected;
    }

    // ���� ����
    public void StopConnection()
    {
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    // ���� ���� ���� ��������
    private bool GetConnectionApproval()
    {
        return networkManager.NetworkConfig.ConnectionApproval;
    }

    // �ΰ��� �ð�
    private void StartTime()
    {
        InvokeRepeating(nameof(UpdateTime), 1f, 1f);
    }

    // ȣ��Ʈ ����� �ʱ�ȭ
    private void StartServer()
    {
        StartTime();
    }

    private void UpdateTime()
    {
        inGameTime += 1;
        UpdateTimeServerRpc(inGameTime);
    }

    #region RPC
    [ServerRpc]
    private void UpdateTimeServerRpc(int newTime)
    {
        inGameTime = newTime;

        UpdateTimeClientRpc(inGameTime);
    }

    [ClientRpc]
    private void UpdateTimeClientRpc(int newTime)
    {
        if (setTimeEvent != null) { setTimeEvent.Invoke(newTime); }
    }

    [ClientRpc]
    private void UpdateConnectionCountClientRpc(int connectionCount)
    {
        currentConnection = connectionCount;
        Debug.Log($"���� ����� Ŭ���̾�Ʈ ��: {currentConnection}");
    }
    #endregion
}