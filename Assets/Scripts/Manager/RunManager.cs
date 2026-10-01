using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("Règles de l'étape")]
    [SerializeField, Min(1f)] private float runDuration = 90f;
    [SerializeField, Min(1f)] private float halfTimeAt = 45f;
    [Header("Références")]
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private Transform playerStartPoint;
    [SerializeField] private Defenders[] defenderPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField, Min(0.1f)] private float spawnInterval = 4f;

    public float ElapsedTime { get; private set; }
    public float RemainingTime => Mathf.Max(0, runDuration - ElapsedTime);
    private bool halfTimeReached;
    private float nextSpawnTime;

    private void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.IsPlaying) return;
        ElapsedTime += Time.deltaTime;
        UIManager.Instance?.UpdateTimer(RemainingTime);
        if (!halfTimeReached && ElapsedTime >= halfTimeAt)
        {
            halfTimeReached = true;
            GameManager.Instance.ReachHalfTime();
            return;
        }
        if (ElapsedTime >= runDuration) { GameManager.Instance.WinRun(); return; }
        if (Time.time >= nextSpawnTime) SpawnDefender();
    }

    public void StartStage()
    {
        ElapsedTime = 0f;
        halfTimeReached = false;
        nextSpawnTime = Time.time + 1f;
        if (player != null)
        {
            if (playerStartPoint != null) player.transform.SetPositionAndRotation(playerStartPoint.position, playerStartPoint.rotation);
            player.Stats.ResetForRun();
        }
    }

    public void RestartStage()
    {
        foreach (Defenders defender in FindObjectsByType<Defenders>(FindObjectsSortMode.None)) Destroy(defender.gameObject);
        StartStage();
    }

    private void SpawnDefender()
    {
        nextSpawnTime = Time.time + spawnInterval;
        if (defenderPrefabs.Length == 0 || spawnPoints.Length == 0) return;
        Defenders prefab = defenderPrefabs[Random.Range(0, defenderPrefabs.Length)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (prefab != null && point != null) Instantiate(prefab, point.position, point.rotation);
    }
}