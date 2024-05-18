using UnityEngine;
using TMPro;

public class EinsCurieScore : MonoBehaviour
{
    public TextMeshProUGUI einsteinScoreText;
    public TextMeshProUGUI curieScoreText;
    public TextMeshProUGUI einsteinGoalsText;
    public TextMeshProUGUI curieGoalsText;
    public TextMeshProUGUI einsteinGoalsAgainstText;
    public TextMeshProUGUI curieGoalsAgainstText;

    void Start()
    {
        // Cargar los puntajes y goles guardados al iniciar la escena
        UpdateScores();
    }

    void UpdateScores()
    {
        // Obtener los puntajes y goles de PlayerPrefs para Einstein y Curie
        int einsteinScore = PlayerPrefs.GetInt("PlayerPrefEinstein", 0);
        int curieScore = PlayerPrefs.GetInt("PlayerPrefCurie", 0);
        int einsteinGoals = PlayerPrefs.GetInt("PlayerPrefEinsteinGoles", 0);
        int curieGoals = PlayerPrefs.GetInt("PlayerPrefCurieGoles", 0);
        int einsteinGoalsAgainst = PlayerPrefs.GetInt("PlayerPrefEinsteinGolesContra", 0);
        int curieGoalsAgainst = PlayerPrefs.GetInt("PlayerPrefCurieGolesContra", 0);

        // Mostrar los puntajes y goles en los TextMeshProUGUI
        einsteinScoreText.text = einsteinScore.ToString();
        curieScoreText.text = curieScore.ToString();
        einsteinGoalsText.text = einsteinGoals.ToString();
        curieGoalsText.text = curieGoals.ToString();
        einsteinGoalsAgainstText.text = einsteinGoalsAgainst.ToString();
        curieGoalsAgainstText.text = curieGoalsAgainst.ToString();
    }
}
