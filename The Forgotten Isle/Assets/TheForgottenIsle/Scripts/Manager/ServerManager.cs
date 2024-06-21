using System;
using System.Collections;
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

    [SerializeField] private GameObject light;

    private int currentConnection = 0;

    private readonly int MAX_CONNECTION = 4;
    private readonly int MAX_HOUR = 24;
    private readonly int MAX_MINUTE = 60;
    private readonly float DURATION = 5.0f;
    private readonly float ADD_ANGLE = 2.5f;
    private readonly float DEFAULT_ANGLE = 90.0f;

    private int timeHour;
    private int timeMinute;

    private Coroutine lightCoroutine;

    public static event Action<int, int> setTimeEvent;

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
        InvokeRepeating(nameof(UpdateTime), 0f, DURATION);
        InvokeRepeating(nameof(StartLightCoroutine), 0f, DURATION);
    }

    private void StartLight()
    {

    }

    // 호스트 연결시 초기화
    private void StartServer()
    {
        StartTime();
        StartLight();
    }

    private void UpdateTime()
    {
        timeMinute += 10;

        if (timeMinute >= MAX_MINUTE)
        {
            timeHour += 1;
            timeMinute = 0;

            if (timeHour >= MAX_HOUR)
            {
                timeHour = 0;
            }
        }

        UpdateTimeServerRpc(timeHour, timeMinute);
    }

    #region RPC
    [ServerRpc]
    private void UpdateTimeServerRpc(int setHour, int setMinute)
    {
        timeHour = setHour;
        timeMinute = setMinute;

        UpdateTimeClientRpc(timeHour, timeMinute);
    }

    [ClientRpc]
    private void UpdateTimeClientRpc(int setHour, int setMinute)
    {
        if (setTimeEvent != null) { setTimeEvent.Invoke(setHour, setMinute); }
    }

    [ClientRpc]
    private void UpdateConnectionCountClientRpc(int connectionCount)
    {
        currentConnection = connectionCount;
        Debug.Log($"현재 연결된 클라이언트 수: {currentConnection}");
    }

    [ClientRpc]
    private void UpdateLightClientRpc(float angle)
    {
        light.transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    }
    #endregion

    private void StartLightCoroutine()
    {
        if (lightCoroutine != null) { StopCoroutine(lightCoroutine); }
        lightCoroutine = StartCoroutine(LightCoroutine());
    }

    private IEnumerator LightCoroutine()
    {
        int timeTotal = (timeHour * 60) + (timeMinute);
        float currentAngle = DEFAULT_ANGLE + ((float)timeTotal / 4);
        float targetAngle = currentAngle + ADD_ANGLE;

        float timeElapsed = 0;

        while (timeElapsed < DURATION)
        {
            timeElapsed += Time.deltaTime;

            float time = Mathf.Clamp01(timeElapsed / DURATION);
            float setAngle = Mathf.Lerp(currentAngle, targetAngle, time);

            UpdateLightClientRpc(setAngle);

            yield return null;
        }

        lightCoroutine = null;
    }
}