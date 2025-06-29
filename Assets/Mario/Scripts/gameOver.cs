using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
  public TMP_Text textPuntos;
  
  public GameObject gameOverPanel;

  public void MostrarGameOver()
  {
    Time.timeScale = 0;
    gameOverPanel.SetActive(true);
    textPuntos.text = "Puntos: 0";
  }

  public void ReiniciarJuego()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void IrAlMenu()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene("Opening");
  }
}
