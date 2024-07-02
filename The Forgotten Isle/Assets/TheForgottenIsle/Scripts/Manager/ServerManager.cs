using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    #region Singleton and Awake()
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

    #region Members

    #region Attribute Members
    [SerializeField] private GameObject light;          // 낮과 밤을 표현하기위한 Directional Light
    #endregion

    #region Private Members
    private Coroutine lightCoroutine;                   // LightCoroutine 중복 예방을 위한 코루틴

    private int currentConnection = 0;                  // 현재 연결된 인원 수
    private int timeHour, timeMinute;                   // 시간, 분 을 표시
    #endregion

    #region Readonly Members
    private readonly int MAX_CONNECTION = 4;            // 최대 연결 인원
    private readonly int MAX_HOUR = 24, MAX_MINUTE = 60;// 24시간, 60분을 표시
    private readonly float DURATION = 5.0f;             // 분이 지나는 시간
    private readonly float ADD_ANGLE = 2.5f;            // DURATION 동안 Light가 회전하는 각도
    private readonly float DEFAULT_ANGLE = 90.0f;       // 낮 부터 시작하기위한 값
    #endregion

    #region Delegate Members
    public static event Action<int, int> setTimeEvent;  // 클라이언트가 각각 가지고있는 타이머를 초기화
                                                        // 인수는 각각 <시간, 분> 표시
    #endregion

    #endregion

    #region Initialization and Setup
    private void Start()
    {
        InitializationEvents();
    }

    private void InitializationEvents()
    {
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }
    #endregion

    #region Connection
    // 연결 시작
    public void StartConnection()
    {
        if (currentConnection == 0)
        {
            networkManager.StartHost();
            Debug.Log("호스트로 연결을 시작합니다.");

            StartServer();
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
    #endregion

    #region HostConnection
    // 호스트 연결시 초기화
    private void StartServer()
    {
        StartTime();
    }

    // 시간 변경
    private void StartTime()
    {
        InvokeRepeating(nameof(UpdateTime), 0f, DURATION);
        InvokeRepeating(nameof(StartLightCoroutine), 0f, DURATION);
    }

    // 시간 설정
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

        UpdateTimeClientRpc(timeHour, timeMinute);
    }
    #endregion

    #region RPC
    // 변화된 시간을 클라이언트에게 전송
    [ClientRpc]
    private void UpdateTimeClientRpc(int setHour, int setMinute)
    {
        if (setTimeEvent != null) { setTimeEvent.Invoke(setHour, setMinute); }
    }

    // 변화된 연결된 인원을 클라이언트에게 전송
    [ClientRpc]
    private void UpdateConnectionCountClientRpc(int connectionCount)
    {
        currentConnection = connectionCount;
        Debug.Log($"현재 연결된 클라이언트 수: {currentConnection}");
    }

    // 변화된 낮과 밤을 클라이언트에게 전송
    [ClientRpc]
    private void UpdateLightClientRpc(float angle)
    {
        light.transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    }
    #endregion

    #region Coroutine
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
    #endregion
}