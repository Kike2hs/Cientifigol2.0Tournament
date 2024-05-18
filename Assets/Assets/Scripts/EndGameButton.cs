using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameButton : MonoBehaviour
{
    public TextMeshProUGUI playerATextMesh;
    public TextMeshProUGUI playerBTextMesh;
    public string playerAPrefName = "";
    public string playerBPrefName = "";
    public string playerAGoalsPrefName = ""; // PlayerPrefs para los goles del jugador A
    public string playerBGoalsPrefName = ""; // PlayerPrefs para los goles del jugador B
    public string playerAGoalsAgainstPrefName = ""; // PlayerPrefs para los goles en contra del jugador A
    public string playerBGoalsAgainstPrefName = ""; // PlayerPrefs para los goles en contra del jugador B

    public void EndGameAndIncrementScores()
    {
        if (string.IsNullOrEmpty(playerAPrefName) || string.IsNullOrEmpty(playerBPrefName) || string.IsNullOrEmpty(playerAGoalsPrefName) || string.IsNullOrEmpty(playerBGoalsPrefName) || string.IsNullOrEmpty(playerAGoalsAgainstPrefName) || string.IsNullOrEmpty(playerBGoalsAgainstPrefName))
        {
            Debug.LogError("PlayerPrefNames are not set!");
            return;
        }

        int playerAScore = int.Parse(playerATextMesh.text);
        int playerBScore = int.Parse(playerBTextMesh.text);

        // Reiniciar los valores de los goles a cero
        PlayerPrefs.SetInt(playerAGoalsPrefName, 0);
        PlayerPrefs.SetInt(playerBGoalsPrefName, 0);

        // Incrementar los goles de los jugadores A y B
        PlayerPrefs.SetInt(playerAGoalsPrefName, playerAScore);
        PlayerPrefs.SetInt(playerBGoalsPrefName, playerBScore);

        // Verificar y actualizar el valor de TORNEOPREF
        int torneoPref = PlayerPrefs.GetInt("TORNEOPREF", 0);
        if (torneoPref == 0)
        {
            PlayerPrefs.SetInt("TORNEOPREF", 1);
        }
        else if (torneoPref == 1)
        {
            PlayerPrefs.SetInt("TORNEOPREF", 2);
        }

        // Guardar los cambios
        PlayerPrefs.Save();

        // Redirigir al usuario a la escena "TORNEO"
        SceneManager.LoadScene("TORNEO");
    }

    int GetScoreIncrement(int playerScore, int opponentScore, bool isPlayerA)
    {
        if (playerScore > opponentScore)
        {
            return 3;
        }
        else if (opponentScore > playerScore)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
