using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text gameOverScoreText;

    [Space(6)]
    [SerializeField] private PlayState playState;

    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject bowPrefab;
    [SerializeField] private GameObject catapultPrefab;
    [SerializeField] private GameObject cannonPrefab;

    [Header("Weapon Indicator Prefabs")]
    [SerializeField] private GameObject bowIndicatorObj;
    [SerializeField] private GameObject catapultIndicatorObj;
    [SerializeField] private GameObject cannonIndicatorObj;

    [Space(6)]
    [SerializeField] private float defaultHealth;
    [SerializeField] private int defaultCurrency;

    [Space(6)]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject buyWeaponButtons;

    [Space(6)]
    [Header("Economy")]
    [SerializeField] private int bowPrice = 20;
    [SerializeField] private int catapultPrice = 30;
    [SerializeField] private int cannonPrice = 50;


    private WeaponType selectedWeapon;
    private GameObject weaponIndicator;
    private Camera _mainCamera;
    private GameObject hitObject;

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
                gameOverScoreText.text = "YOUR SCORE: " + Score;
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
            currencyText.text = "Currency: " + _currency + "$";
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

    #region Start
    void Start()
    {
        Score = 0;
        Currency = defaultCurrency;
        Health = defaultHealth;

        StartCoroutine(CurrencyIncreaser());

        _mainCamera = Camera.main;
    }

    #endregion

    #region Game Cycle
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

    #endregion

    #region Update

    void Update()
    {
        if (playState == PlayState.SelectingWeapon)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("WeaponPlace")) && weaponIndicator != null)
            {
                hitObject = hit.collider.gameObject;

                if (!hitObject.GetComponent<WeaponPlacementValidator>())
                {
                    weaponIndicator.transform.position = new Vector3(hitObject.transform.position.x, hitObject.transform.position.y + .2f, hitObject.transform.position.z);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                WeaponPlacementValidator weaponPlacementValidator = hitObject.
                AddComponent<WeaponPlacementValidator>();

                GameObject spawendWeapon = InstantiateWeapon(weaponIndicator.transform);

                if (spawendWeapon.TryGetComponent(out Weapon weapon))
                {
                    weapon.placementValidator = weaponPlacementValidator;
                }

                ReduceThePrice();
                ReturnToIdleState();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReturnToIdleState();
            }
        }
    }

    #endregion

    #region Weapon Placement 

    private void ReturnToIdleState()
    {
        weaponIndicator.SetActive(false);
        weaponIndicator = null;

        playState = PlayState.Idle;
        buyWeaponButtons.SetActive(true);
    }

    public enum PlayState
    {
        Idle,
        SelectingWeapon,
    }

    public enum WeaponType
    {
        Bow,
        Catapult,
        Cannon
    }


    GameObject InstantiateWeapon(Transform spawningTransform)
    {
        string weaponNameToSpawn = string.Empty;
        GameObject weaponPrefabToSpawn = null;

        switch (selectedWeapon)
        {
            case WeaponType.Bow: weaponNameToSpawn = "BowWeapon"; weaponPrefabToSpawn = bowPrefab; break;
            case WeaponType.Catapult: weaponNameToSpawn = "CatapultWeapon"; weaponPrefabToSpawn = catapultPrefab; break;
            case WeaponType.Cannon: weaponNameToSpawn = "CannonWeapon"; weaponPrefabToSpawn = cannonPrefab; break;
        }

        return ObjectPoolManager.Instance.SpawnFromPool(weaponNameToSpawn, spawningTransform.position, Quaternion.identity, weaponPrefabToSpawn);
    }

    void InstantiateWeaponIndicator()
    {
        GameObject indicatorToInstantiate = null;
        buyWeaponButtons.SetActive(false);

        switch (selectedWeapon)
        {
            case WeaponType.Bow: indicatorToInstantiate = bowIndicatorObj; break;
            case WeaponType.Catapult: indicatorToInstantiate = catapultIndicatorObj; break;
            case WeaponType.Cannon: indicatorToInstantiate = cannonIndicatorObj; break;
        }

        weaponIndicator = indicatorToInstantiate;
        weaponIndicator.SetActive(true);

        playState = PlayState.SelectingWeapon;
    }

    public void BuyBow()
    {
        if (Currency < bowPrice) return;

        selectedWeapon = WeaponType.Bow;
        InstantiateWeaponIndicator();
    }

    public void BuyCatapult()
    {
        if (Currency < catapultPrice) return;

        selectedWeapon = WeaponType.Catapult;
        InstantiateWeaponIndicator();
    }

    public void BuyCannon()
    {
        if (Currency < cannonPrice) return;

        selectedWeapon = WeaponType.Cannon;
        InstantiateWeaponIndicator();
    }

    public void ReduceThePrice()
    {
        switch (selectedWeapon)
        {
            case WeaponType.Bow: Currency -= bowPrice; break;
            case WeaponType.Catapult: Currency -= catapultPrice; break;
            case WeaponType.Cannon: Currency -= cannonPrice; break;
        }
    }

    #endregion
}