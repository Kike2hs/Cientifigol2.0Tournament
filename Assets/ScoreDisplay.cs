using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI einsteinScoreText;
    public TextMeshProUGUI darwinScoreText;
    public TextMeshProUGUI einsteinGoalsText; // Nuevo campo para mostrar los goles de Einstein
    public TextMeshProUGUI darwinGoalsText; // Nuevo campo para mostrar los goles de Darwin
    public TextMeshProUGUI einsteinGoalsAgainstText; // Nuevo campo para mostrar los goles en contra de Einstein
    public TextMeshProUGUI darwinGoalsAgainstText; // Nuevo campo para mostrar los goles en contra de Darwin
    public TextMeshProUGUI torneoPrefText; // Nuevo campo para mostrar el valor de TORNEOPREF

    public List<GameObject> objectsToShowOrHide; // Lista de objetos a mostrar u ocultar

    void Start()
    {
        // Verificar el valor de TORNEOPREF y mostrar/ocultar los objetos
        int torneoPrefValue = PlayerPrefs.GetInt("TORNEOPREF", 0);
        SetObjectsVisibility(torneoPrefValue);

        // Mostrar el valor de TORNEOPREF en el TextMeshProUGUI
        torneoPrefText.text = "" + torneoPrefValue.ToString();

        // Cargar los puntajes y goles guardados al iniciar la escena
        UpdateScores();
    }

    void UpdateScores()
    {
        // Obtener los puntajes y goles de PlayerPrefs para Einstein y Darwin
        int einsteinScore = PlayerPrefs.GetInt("PlayerPrefEinstein", 0);
        int darwinScore = PlayerPrefs.GetInt("PlayerPrefDarwin", 0);
        int einsteinGoals = PlayerPrefs.GetInt("PlayerPrefEinsteinGoles", 0); // Nuevo PlayerPrefs para los goles de Einstein
        int darwinGoals = PlayerPrefs.GetInt("PlayerPrefDarwinGoles", 0); // Nuevo PlayerPrefs para los goles de Darwin
        int einsteinGoalsAgainst = PlayerPrefs.GetInt("PlayerPrefEinsteinGolesContra", 0); // Nuevo PlayerPrefs para los goles en contra de Einstein
        int darwinGoalsAgainst = PlayerPrefs.GetInt("PlayerPrefDarwinGolesContra", 0); // Nuevo PlayerPrefs para los goles en contra de Darwin

        // Mostrar los puntajes y goles en los TextMeshProUGUI
        einsteinScoreText.text = einsteinScore.ToString();
        darwinScoreText.text = darwinScore.ToString();
        einsteinGoalsText.text = einsteinGoals.ToString(); // Mostrar los goles de Einstein
        darwinGoalsText.text = darwinGoals.ToString(); // Mostrar los goles de Darwin
        einsteinGoalsAgainstText.text = einsteinGoalsAgainst.ToString(); // Mostrar los goles en contra de Einstein
        darwinGoalsAgainstText.text = darwinGoalsAgainst.ToString(); // Mostrar los goles en contra de Darwin
    }

    void SetObjectsVisibility(int torneoPrefValue)
    {
        bool shouldShow = torneoPrefValue == 1;

        foreach (GameObject obj in objectsToShowOrHide)
        {
            obj.SetActive(shouldShow);
        }
    }
}
