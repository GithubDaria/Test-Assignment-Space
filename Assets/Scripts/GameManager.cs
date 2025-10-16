using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameDuration = 30f;      
    [SerializeField] private float spawnInterval = 3f;     
    [SerializeField] private Vector2Int gridSize = new Vector2Int(5, 5); 
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private GameObject itemPrefab;         


    [SerializeField] private Transform spawnOrigin; 
    [SerializeField] private Vector3 wallRight = Vector3.right; 
    [SerializeField] private Vector3 wallUp = Vector3.up;      
    [SerializeField] private Vector3 wallNormal = Vector3.forward; 

    private float timer;
    private bool gameRunning;

    private Coroutine gameLoopCoroutine;

    private int score = 0;

    public static GameManager Instance { get; private set; }

    public static event Action<int> OnScoreChanged;
    public static event Action<float> OnTimerChanged;
    public static event Action OnGameEnd;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        if (gameRunning)
            return;

        Debug.Log("Game Started!");
        gameRunning = true;
        timer = gameDuration;
        gameLoopCoroutine = StartCoroutine(GameLoop());

    }

    private IEnumerator GameLoop()
    {
        float nextSpawnTime = 0f;
        int spawnCount = 1;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            OnTimerChanged?.Invoke(timer);

            if (Time.time >= nextSpawnTime)
            {

                SpawnItems(spawnCount);
                spawnCount++;
                nextSpawnTime = Time.time + spawnInterval;
            }

            yield return null;
        }

        GameOver();
    }

    private void GameOver()
    {
        gameRunning = false;
        Debug.Log("Game Over");

        if (gameLoopCoroutine != null)
            StopCoroutine(gameLoopCoroutine);
        OnGameEnd?.Invoke();
    }

    private void SpawnItems(int count)
    {
        if (!itemPrefab || !spawnOrigin)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int gridX = UnityEngine.Random.Range(0, gridSize.x);
            int gridY = UnityEngine.Random.Range(0, gridSize.y);

            Vector3 basePos = spawnOrigin.position
                            + wallRight * (gridX * cellSize)
                            + wallUp * (gridY * cellSize);


            Vector3 spawnPos = basePos + (wallRight  + wallUp) *spawnOffset;
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        }
    }
        public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score); 
    }
}
