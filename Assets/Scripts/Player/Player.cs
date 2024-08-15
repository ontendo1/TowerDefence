using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text healthText;

    [Space(6)]
    [SerializeField] private float defaultHealth;

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
                //Write Game Over Code
            }
        }
    }

    #region Singleton
    public static Player Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    void Start()
    {
        Health = defaultHealth;
    }
}
