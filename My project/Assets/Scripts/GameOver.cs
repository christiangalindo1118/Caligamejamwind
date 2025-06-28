using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
   public TMP_Text textPuntos;
   public GameObject gameOverPanel;
   
   public void MostrarGameOver()
   {
      gameOverPanel.SetActive(true);
      textPuntos.text = (("Puntos: "));
   }
}


