using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float GameTime { get; private set; }

    public bool TabletCollected { get; private set; } = false;
    public bool BinocularsCollected { get; private set; } = false;

    [Header("UI Panels (Opening scene only)")]
    public GameObject instructionPanel;
    public GameObject mainMenuPanel;

    [Header("Pause & Game Over Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("Optional Fade Message (UI Text)")]
    public Text titleText;

    [Header("Countdown Timer")]
    public float countdownTime = 120f;
    private float remainingTime;
    public Text timerText;

    private bool isPaused = false;
    private bool gameOverTriggered = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        remainingTime = countdownTime;
    }

    void Update()
    {
        GameTime += Time.deltaTime;

        if (!isPaused && !gameOverTriggered)
        {
            // Cuenta regresiva
            remainingTime -= Time.deltaTime;

            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(remainingTime / 60f);
                int seconds = Mathf.FloorToInt(remainingTime % 60f);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }

            if (remainingTime <= 0f)
            {
                TriggerGameOver();
            }
        }

        // Pausar con tecla "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                ShowPausePanel();
            else
                ResumeGame();
        }
    }

    // ---------- RECOLECCIÓN ----------
    public void SetTabletCollected()
    {
        TabletCollected = true;
        Debug.Log("Tablet ha sido recolectada.");
    }

    public void SetBinocularsCollected()
    {
        BinocularsCollected = true;
        Debug.Log("Binoculares han sido recolectados.");
    }

    // ---------- MENÚ PRINCIPAL ----------
    public void StartGame()
    {
        HideAllPanels();
        SceneManager.LoadScene("Level1");
    }

    public void ShowInstructions()
    {
        HideAllPanels();
        instructionPanel?.SetActive(true);
    }

    public void HideAllPanels()
    {
        instructionPanel?.SetActive(false);
        mainMenuPanel?.SetActive(false);

        if (titleText != null)
        {
            StartCoroutine(FadeOutText(titleText, 1.5f));
        }
    }

    // ---------- FADE DE TEXTO ----------
    IEnumerator FadeOutText(Text textElement, float duration)
    {
        float elapsed = 0f;
        Color originalColor = textElement.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        textElement.gameObject.SetActive(false);
    }

    // ---------- PAUSA ----------
    public void ShowPausePanel()
    {
        isPaused = true;
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    // ---------- GAME OVER ----------
    public void TriggerGameOver()
    {
        if (gameOverTriggered) return;

        Debug.Log("Game Over triggered by timer.");
        gameOverTriggered = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // ---------- RETORNO A OPENING ----------
    public void ReturnToOpening()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Opening");
    }
}
