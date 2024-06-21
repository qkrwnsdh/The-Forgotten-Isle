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

    // 클라이언트 연결 시 호출되는 콜백
    private void OnClientConnected(ulong clientId)
    {
        if (networkManager.IsServer)
        {
            currentConnection += 1;
            UpdateConnectionCountClientRpc(currentConnection);
        }
    }

    // 클라이언트 연결 해제 시 호출되는 콜백
    private void OnClientDisconnected(ulong clientId)
    {
        if (networkManager.IsServer)
        {
            currentConnection -= 1;
            UpdateConnectionCountClientRpc(currentConnection);
        }
    }

    // 연결 시작
    public void StartConnection()
    {
        if (currentConnection == 0)
        {
            networkManager.StartClient();
            Debug.Log("호스트로 연결을 시작합니다.");

            //StartServer();
        }
        else
        {
            networkManager.StartClient();
            Debug.Log("클라이언트로 연결을 시작합니다.");
        }

        networkManager.OnClientConnectedCallback += OnClientConnected;
    }

    // 연결 종료
    public void StopConnection()
    {
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    // 인게임 시간
    private void StartTime()
    {
        InvokeRepeating(nameof(UpdateTime), 1f, 1f);
    }

    // 호스트 연결시 초기화
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
        Debug.Log($"현재 연결된 클라이언트 수: {currentConnection}");
    }
    #endregion
}