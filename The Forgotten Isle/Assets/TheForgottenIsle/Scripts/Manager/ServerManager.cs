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
    [SerializeField] private GameObject light;          // ���� ���� ǥ���ϱ����� Directional Light
    #endregion

    #region Private Members
    private Coroutine lightCoroutine;                   // LightCoroutine �ߺ� ������ ���� �ڷ�ƾ

    private int currentConnection = 0;                  // ���� ����� �ο� ��
    private int timeHour, timeMinute;                   // �ð�, �� �� ǥ��
    #endregion

    #region Readonly Members
    private readonly int MAX_CONNECTION = 4;            // �ִ� ���� �ο�
    private readonly int MAX_HOUR = 24, MAX_MINUTE = 60;// 24�ð�, 60���� ǥ��
    private readonly float DURATION = 5.0f;             // ���� ������ �ð�
    private readonly float ADD_ANGLE = 2.5f;            // DURATION ���� Light�� ȸ���ϴ� ����
    private readonly float DEFAULT_ANGLE = 90.0f;       // �� ���� �����ϱ����� ��
    #endregion

    #region Delegate Members
    public static event Action<int, int> setTimeEvent;  // Ŭ���̾�Ʈ�� ���� �������ִ� Ÿ�̸Ӹ� �ʱ�ȭ
                                                        // �μ��� ���� <�ð�, ��> ǥ��
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
    // ���� ����
    public void StartConnection()
    {
        if (currentConnection == 0)
        {
            networkManager.StartHost();
            Debug.Log("ȣ��Ʈ�� ������ �����մϴ�.");

            StartServer();
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
    #endregion

    #region HostConnection
    // ȣ��Ʈ ����� �ʱ�ȭ
    private void StartServer()
    {
        StartTime();
    }

    // �ð� ����
    private void StartTime()
    {
        InvokeRepeating(nameof(UpdateTime), 0f, DURATION);
        InvokeRepeating(nameof(StartLightCoroutine), 0f, DURATION);
    }

    // �ð� ����
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
    // ��ȭ�� �ð��� Ŭ���̾�Ʈ���� ����
    [ClientRpc]
    private void UpdateTimeClientRpc(int setHour, int setMinute)
    {
        if (setTimeEvent != null) { setTimeEvent.Invoke(setHour, setMinute); }
    }

    // ��ȭ�� ����� �ο��� Ŭ���̾�Ʈ���� ����
    [ClientRpc]
    private void UpdateConnectionCountClientRpc(int connectionCount)
    {
        currentConnection = connectionCount;
        Debug.Log($"���� ����� Ŭ���̾�Ʈ ��: {currentConnection}");
    }

    // ��ȭ�� ���� ���� Ŭ���̾�Ʈ���� ����
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