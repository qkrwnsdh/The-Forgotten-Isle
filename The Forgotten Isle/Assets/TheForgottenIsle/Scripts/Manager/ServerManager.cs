using System;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    #region Singleton
    public static ServerManager instance;
    private NetworkManager networkManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        networkManager = NetworkManager.Singleton;
    }
    #endregion


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
        if (currentConnection == 0)
        {
            networkManager.StartClient();
            Debug.Log("ȣ��Ʈ�� ������ �����մϴ�.");

            //StartServer();
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