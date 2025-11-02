using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    public int playerScore = 0;
    public int highScore = 0;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverScreen;
    public GameObject welcomeScreen;
    
    public bool gameHasStarted = false;
    public bool isGameOver = false;

    private const string HIGH_SCORE_KEY = "HighScore";

    void Start()
    {
        // Carica il punteggio più alto salvato
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        UpdateHighScoreDisplay();
        
        // Mostra la schermata di benvenuto
        if (welcomeScreen != null)
        {
            welcomeScreen.SetActive(true);
        }
    }

    public void CloseWelcomeScreen()
    {
        // Questo metodo viene chiamato dal pulsante "GIOCA"
        // Chiude SOLO la schermata, NON avvia il gioco
        if (welcomeScreen != null)
        {
            welcomeScreen.SetActive(false);
        }
        // NON chiamare StartGame() qui!
    }

    public void StartGame()
    {
        // Questo metodo viene chiamato dal BirdScript al primo SPAZIO
        // Avvia effettivamente il gioco
        gameHasStarted = true;
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        // Aggiorna la visualizzazione
        scoreText.text = playerScore.ToString();

        // PRIMA controlla se il nuovo punteggio supera il record
        if (playerScore + scoreToAdd > highScore)
        {
            highScore = playerScore; // Aggiorna highScore al valore di playerScore
            SaveHighScore();
            UpdateHighScoreDisplay();
        }

        // POI incrementa il punteggio
        playerScore += scoreToAdd;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        
        // Assicurati che il punteggio più alto sia salvato al game over
        if (playerScore > PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0))
        {
            SaveHighScore();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + highScore.ToString();
        }
    }
}
