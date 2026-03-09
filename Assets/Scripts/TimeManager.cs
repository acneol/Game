using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    [Header("UI Elements")]
    public GameObject endGameScreen;
    public TextMeshProUGUI timerText;

    private float _timer = 0f;
    private bool _isGameFinished = false;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        SetupUI();
    }

    void SetupUI()
    {
        endGameScreen.SetActive(false);
        Time.timeScale = 1f;

        RectTransform panelRect = endGameScreen.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        RectTransform textRect = timerText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 1f);
        textRect.anchorMax = new Vector2(0.5f, 1f);
        textRect.pivot = new Vector2(0.5f, 1f);
        textRect.anchoredPosition = new Vector2(0, -30);
    }

    void Update()
    {
        if (!_isGameFinished)
        {
            _timer += Time.deltaTime;
            timerText.text = Mathf.FloorToInt(_timer).ToString() + " seconds";
        }

        // Această linie permite resetarea oricând apeși tasta R
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayAgain();
        }
    }

    public void FinishGame()
    {
        _isGameFinished = true;
        endGameScreen.SetActive(true);

        RectTransform textRect = timerText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = Vector2.zero;

        timerText.text = "Congratulations! You found the diamond in " + Mathf.FloorToInt(_timer) + " seconds\nPress 'R' to Play Again";

        // Opțional: poți lăsa timpul să curgă sau să îl îngheți
        // Time.timeScale = 0f; 
    }

    public void PlayAgain()
    {
        _isGameFinished = false;
        _timer = 0f;
        endGameScreen.SetActive(false);
        Time.timeScale = 1f;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Transform fullPlayer = playerObj.transform.root;
            PlayerMovementTutorial pm = playerObj.GetComponent<PlayerMovementTutorial>();
            Rigidbody rb = playerObj.GetComponent<Rigidbody>();
            CharacterController cc = fullPlayer.GetComponentInChildren<CharacterController>();

            // 1. Oprim tot ce ține de fizică
            if (pm != null) pm.ResetMovement();
            if (cc != null) cc.enabled = false;

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
            }

            // 2. Teleportarea brută la coordonatele tale
            Vector3 targetPos = new Vector3(-20.88f, 2.98f + 2.5f, 13.62f);
            fullPlayer.position = targetPos;

            foreach (Transform child in fullPlayer)
            {
                child.localPosition = Vector3.zero;
            }

            // --- CHEIA SUCCESULUI ---
            // Forțăm Unity să recunoască noua poziție în motorul de fizică IMEDIAT
            Physics.SyncTransforms();

            // 3. Reactivăm componentele
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.WakeUp();
            }
            if (cc != null) cc.enabled = true;

            SetupUI();
        }
    }
}