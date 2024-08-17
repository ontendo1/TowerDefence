using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text healthText;

    [Space(6)]
    [SerializeField] private float defaultHealth;
    [SerializeField] private int defaultCurrency;

    [Space(6)]
    [SerializeField] private GameObject gameOverPanel;

    private float _health;
    public float Health
    {
        get => _health;

        set
        {
            _health = value;
            healthText.text = "Lives: " + _health;

            if (_health <= 0)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private int _score;
    public int Score
    {
        get => _score;

        set
        {
            _score = value;
            scoreText.text = "Score: " + _score;
        }
    }

    private int _currency;
    public int Currency
    {
        get => _currency;

        set
        {
            _currency = value;
            currencyText.text = "Currency: " + _currency;
        }
    }

    #region Singleton
    public static Player Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    void Start()
    {
        Score = 0;
        Currency = defaultCurrency;
        Health = defaultHealth;

        StartCoroutine(CurrencyIncreaser());
    }

    IEnumerator CurrencyIncreaser()
    {
        while (Health > 0)
        {
            yield return new WaitForSeconds(1);
            Currency += 1;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
