using TMPro;
using UnityEngine;
public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager Instance { get; private set; }

    [Header("Player Settings")]
    [Tooltip("The Team ID that belongs to the human player")]
    [SerializeField] private int _playerTeamId = 1;

    [Header("Bases to Track")]
    [SerializeField] private Health _playerBaseHealth;
    [SerializeField] private Health _enemyBaseHealth;

    [Header("UI Panels")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _killsText;

    private float _sessionTime;
    private bool _isGameOver;
    private int _playerKillsCount;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Ensure endgame UI panels are hidden when the session starts
        if (_victoryPanel != null) _victoryPanel.SetActive(false);
        if (_defeatPanel != null) _defeatPanel.SetActive(false);

        // Subscribe to death events of both main structures
        if (_playerBaseHealth != null) _playerBaseHealth.onDie += HandlePlayerBaseDestroyed;
        if (_enemyBaseHealth != null) _enemyBaseHealth.onDie += HandleEnemyBaseDestroyed;
    }

    private void OnDestroy()
    {
        if (_playerBaseHealth != null) _playerBaseHealth.onDie -= HandlePlayerBaseDestroyed;
        if (_enemyBaseHealth != null) _enemyBaseHealth.onDie -= HandleEnemyBaseDestroyed;
    }

    private void Update()
    {
        if (_isGameOver) return;
        _sessionTime += Time.deltaTime;

        // Dynamically update the onscreen timer text with formatted time
        if (_timerText != null)
        {
            _timerText.text = "Time: " + GetFormattedTime();
        }
    }

    private void HandleEnemyBaseDestroyed()
    {
        EndGame(true);
    }

    private void HandlePlayerBaseDestroyed()
    {
        EndGame(false);
    }

    private void EndGame(bool isVictory)
    {
        _isGameOver = true;

        Time.timeScale = 0f;

        string formattedTime = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_sessionTime / 60), Mathf.FloorToInt(_sessionTime % 60));
        Debug.Log($"[GAME OVER] Match ended! Result: {(isVictory ? "Victory" : "Defeat")}. Total Time: {formattedTime} | Kills: {_playerKillsCount}");

        if (isVictory && _victoryPanel != null) _victoryPanel.SetActive(true);
        if (!isVictory && _defeatPanel != null) _defeatPanel.SetActive(true);
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_sessionTime / 60);
        int seconds = Mathf.FloorToInt(_sessionTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void RegisterUnitForScore(Health unitHealth)
    {
        if (unitHealth != null)
        {
            unitHealth.onDie += () => HandleUnitDeath(unitHealth);
        }
    }

    private void HandleUnitDeath(Health deadUnitHealth)
    {
        if (deadUnitHealth == null) return;

        TeamTag team = deadUnitHealth.GetComponent<TeamTag>();

        if (team != null && team.GetTeamId() != _playerTeamId)
        {
            RegisterPlayerKill();
        }

        deadUnitHealth.onDie -= () => HandleUnitDeath(deadUnitHealth);
    }
    private void RegisterPlayerKill()
    {
        _playerKillsCount++;
        if (_killsText != null)
        {
            _killsText.text = "Kills: " + _playerKillsCount;
        }
    }
    public int PlayerTeamId => _playerTeamId;
}