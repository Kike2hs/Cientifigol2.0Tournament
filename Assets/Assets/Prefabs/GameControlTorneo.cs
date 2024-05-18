using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameControlTorneo : MonoBehaviour
{
    public GameObject[] characters;
    public Transform[] startPositions; // Array de posiciones iniciales para los otros personajes
    public TextMeshProUGUI[] startPositionTexts; // Array de TextMeshPro para las posiciones iniciales
    public Transform semifinal1Position; // Posición para el personaje que pasa a la primera semifinal
    public Transform semifinal2Position; // Posición para el personaje que pasa a la segunda semifinal
    public Transform semifinal3Position; // Posición para el personaje que pasa a la tercera semifinal
    public Transform semifinal4Position; // Posición para el personaje que pasa a la cuarta semifinal
    public string menuScene = "Character Selection Menu";
    private string selectedCharacterDataName = "SelectedCharacter";
    private string selectedCharacterNameDataName = "SelectedCharacterName"; 
    private int selectedCharacter;
    private GameObject playerObject;
    private string selectedCharacterName;
    private List<int> selectedIndices = new List<int>(); // Lista para mantener un registro de los índices de personajes ya seleccionados
    private GameObject[] instantiatedCharacters; // Array para almacenar los objetos instanciados

    public TextMeshProUGUI characterNameText; 
    public Transform startPositionStadium; // Nueva posición para el estadio
    public TextMeshProUGUI textStadium; // Nuevo TextMeshPro para el nombre del estadio
    public TextMeshProUGUI textSemifinal; // Nuevo TextMeshPro para el nombre de la semifinal

    public GameObject boton1; // Botón 1
    public GameObject boton2; // Botón 2

    void Start()
    {
        instantiatedCharacters = new GameObject[startPositions.Length]; // Inicializar el array de objetos instanciados

        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        if (PlayerPrefs.GetInt("TournamentSaved", 0) == 1)
        {
            LoadTournamentData();
        }
        else
        {
            InitializeNewTournament();
        }

        if (selectedCharacter >= 0 && selectedCharacter < characters.Length)
        {
            string characterName = characters[selectedCharacter].name;
            Debug.Log("Nombre del personaje: " + characterName); 

            // Instanciar el personaje seleccionado
            InstantiateSelectedCharacter();
        }
        else
        {
            Debug.LogError("Selected character index is out of bounds!");
        }

        // Asignar goles aleatorios si TORNEOPREF es 1
        if (PlayerPrefs.GetInt("TORNEOPREF", 0) == 1)
        {
            AssignRandomGoals();
        }

        // Esperar un momento para asegurarse de que los valores se han asignado
        StartCoroutine(WaitAndCompareGoals());

        // Asignar el nombre del prefab vinculado en startPositionStadium al textStadium
        UpdateStadiumText();

        // Mostrar/ocultar botones según el valor de TORNEOPREF
        UpdateButtons();
    }

    void InstantiateSelectedCharacter()
    {
        if (playerObject != null)
        {
            Destroy(playerObject);
        }

        playerObject = Instantiate(characters[selectedCharacter], startPositions[0].position, Quaternion.identity);
        instantiatedCharacters[0] = playerObject; // Almacenar la referencia del objeto instanciado
        selectedCharacterName = characters[selectedCharacter].name;

        // Asignar el nombre del personaje al TextMeshPro
        if (characterNameText != null)
        {
            characterNameText.text = selectedCharacterName;
        }
    }

    void InitializeNewTournament()
    {
        selectedIndices.Clear(); // Asegúrate de limpiar la lista de índices seleccionados

        // Reiniciar los valores de los goles en PlayerPrefs
        PlayerPrefs.SetInt("PlayerPrefEinsteinGoles", 0);
        PlayerPrefs.SetInt("PlayerPrefDarwinGoles", 0);

        // Reiniciar TORNEOPREF a 0
        PlayerPrefs.SetInt("TORNEOPREF", 0);

        // Instanciar el personaje seleccionado en la primera posición
        InstantiateSelectedCharacter();

        // Instanciar otros personajes en posiciones iniciales diferentes
        for (int i = 0; i < 7; i++)
        {
            int randomIndex = GetUniqueRandomIndex(); // Obtener un índice único aleatorio
            selectedIndices.Add(randomIndex); // Agregar el índice a la lista de índices seleccionados

            GameObject instantiatedCharacter = Instantiate(characters[randomIndex], startPositions[i + 1].position, Quaternion.identity);
            instantiatedCharacters[i + 1] = instantiatedCharacter; // Almacenar la referencia del objeto instanciado
        }

        // Guardar los datos del torneo inicializado
        SaveTournamentData();
    }

    int GetUniqueRandomIndex()
    {
        int randomIndex = Random.Range(0, characters.Length);
        while (selectedIndices.Contains(randomIndex) || randomIndex == selectedCharacter) // Verificar si el índice ya está en la lista o es el mismo que el personaje seleccionado por el usuario
        {
            randomIndex = Random.Range(0, characters.Length); // Si ya está en la lista o es el mismo que el personaje seleccionado por el usuario, obtener otro índice
        }
        return randomIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public string GetSelectedCharacterName()
    {
        return selectedCharacterName;
    }

    public void SaveTournamentData()
    {
        PlayerPrefs.SetInt("TournamentSaved", 1);
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacter);
        PlayerPrefs.SetFloat("PlayerX", playerObject.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerObject.transform.position.y);

        for (int i = 0; i < selectedIndices.Count; i++)
        {
            PlayerPrefs.SetInt("CharacterIndex" + i, selectedIndices[i]);
            PlayerPrefs.SetFloat("CharacterX" + i, startPositions[i + 1].position.x);
            PlayerPrefs.SetFloat("CharacterY" + i, startPositions[i + 1].position.y);
        }

        // Guardar la posición y el índice del personaje en semifinales, si existe
        if (PlayerPrefs.HasKey("SemifinalCharacterIndex"))
        {
            int semifinalCharacterIndex = PlayerPrefs.GetInt("SemifinalCharacterIndex");
            GameObject semifinalCharacter = instantiatedCharacters[semifinalCharacterIndex];
            PlayerPrefs.SetFloat("SemifinalCharacterX", semifinalCharacter.transform.position.x);
            PlayerPrefs.SetFloat("SemifinalCharacterY", semifinalCharacter.transform.position.y);
        }

        PlayerPrefs.Save();
    }

    public void LoadTournamentData()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacterIndex");
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");

        // Instanciar el personaje seleccionado en la posición guardada
        playerObject = Instantiate(characters[selectedCharacter], new Vector2(playerX, playerY), Quaternion.identity);
        instantiatedCharacters[0] = playerObject; // Almacenar la referencia del objeto instanciado

        selectedIndices.Clear();
        for (int i = 0; i < 7; i++)
        {
            int index = PlayerPrefs.GetInt("CharacterIndex" + i);
            selectedIndices.Add(index);

            float charX = PlayerPrefs.GetFloat("CharacterX" + i);
            float charY = PlayerPrefs.GetFloat("CharacterY" + i);
            GameObject instantiatedCharacter = Instantiate(characters[index], new Vector2(charX, charY), Quaternion.identity);
            instantiatedCharacters[i + 1] = instantiatedCharacter; // Almacenar la referencia del objeto instanciado
        }

        // Cargar y mover el personaje de semifinales, si existe
        if (PlayerPrefs.HasKey("SemifinalCharacterIndex"))
        {
            int semifinalCharacterIndex = PlayerPrefs.GetInt("SemifinalCharacterIndex");
            float semifinalCharX = PlayerPrefs.GetFloat("SemifinalCharacterX");
            float semifinalCharY = PlayerPrefs.GetFloat("SemifinalCharacterY");
            GameObject semifinalCharacter = instantiatedCharacters[semifinalCharacterIndex];
            semifinalCharacter.transform.position = new Vector2(semifinalCharX, semifinalCharY);
        }

        // Actualizar el nombre del prefab vinculado en startPositionStadium
        UpdateStadiumText();
    }

    public void OnSaveButtonClicked()
    {
        SaveTournamentData();
    }

    // Método para comparar los goles de dos posiciones y resaltar el prefab del mayor
    public void CompareAndHighlightGoals(int index1, int index2, Transform semifinalPosition)
    {
        if (startPositionTexts.Length > index1 && startPositionTexts.Length > index2)
        {
            // Asegurarse de que los textos sean válidos números enteros
            bool isGoals1Valid = int.TryParse(startPositionTexts[index1].text, out int goals1);
            bool isGoals2Valid = int.TryParse(startPositionTexts[index2].text, out int goals2);

            if (isGoals1Valid && isGoals2Valid)
            {
                if (goals1 > goals2)
                {
                    startPositionTexts[index1].color = Color.green;
                    startPositionTexts[index2].color = Color.white;
                    MovePrefabToSemifinal(index1, semifinalPosition);
                }
                else if (goals2 > goals1)
                {
                    startPositionTexts[index2].color = Color.green;
                    startPositionTexts[index1].color = Color.white;
                    MovePrefabToSemifinal(index2, semifinalPosition);
                }
                else
                {
                    // Opcional: Si los valores son iguales, puedes decidir qué hacer (por ejemplo, no resaltar ninguno o ambos)
                    startPositionTexts[index1].color = Color.yellow;
                    startPositionTexts[index2].color = Color.yellow;
                }
            }
            else
            {
                Debug.LogError("Failed to parse goals to integer values.");
            }
        }
        else
        {
            Debug.LogError("TextMeshPro indices are out of bounds!");
        }
    }

    void MovePrefabToSemifinal(int index, Transform semifinalPosition)
    {
        if (instantiatedCharacters[index] != null)
        {
            // Hacer una copia del prefab en su posición original
            Instantiate(instantiatedCharacters[index], instantiatedCharacters[index].transform.position, Quaternion.identity);

            // Mover el prefab original a la posición de semifinales
            instantiatedCharacters[index].transform.position = semifinalPosition.position;

            // Guardar la posición y el índice del personaje en semifinales
            PlayerPrefs.SetFloat("SemifinalCharacterX", semifinalPosition.position.x);
            PlayerPrefs.SetFloat("SemifinalCharacterY", semifinalPosition.position.y);
            PlayerPrefs.SetInt("SemifinalCharacterIndex", index);
        }
    }

    IEnumerator WaitAndCompareGoals()
    {
        // Espera un frame para asegurarse de que los valores se han asignado
        yield return null;

        // Realiza la comparación para cada llave de cuartos de final
        CompareAndHighlightGoals(0, 1, semifinal1Position);
        CompareAndHighlightGoals(2, 3, semifinal2Position);
        CompareAndHighlightGoals(4, 5, semifinal3Position);
        CompareAndHighlightGoals(6, 7, semifinal4Position);

        // Actualizar el nombre del prefab vinculado en las semifinales
        UpdateSemifinalText();
    }

    void AssignRandomGoals()
    {
        // Asigna valores aleatorios a los goles en PlayerPrefs
        int[] goals = { 1, 2, 3, 4, 5, 6, 7 };
        System.Random random = new System.Random();

        void SetRandomGoals(string keyA, string keyB, int indexA, int indexB)
        {
            List<int> remainingGoals = new List<int>(goals);
            int valueA = remainingGoals[random.Next(remainingGoals.Count)];
            remainingGoals.Remove(valueA);
            int valueB = remainingGoals[random.Next(remainingGoals.Count)];

            PlayerPrefs.SetInt(keyA, valueA);
            PlayerPrefs.SetInt(keyB, valueB);

            startPositionTexts[indexA].text = valueA.ToString();
            startPositionTexts[indexB].text = valueB.ToString();
        }

        SetRandomGoals("PlayerPrefAGolesSemis2", "PlayerPrefBGolesSemis2", 2, 3);
        SetRandomGoals("PlayerPrefAGolesSemis3", "PlayerPrefBGolesSemis3", 4, 5);
        SetRandomGoals("PlayerPrefAGolesSemis4", "PlayerPrefBGolesSemis4", 6, 7);
    }

    void UpdateStadiumText()
    {
        for (int i = 0; i < startPositions.Length; i++)
        {
            if (startPositions[i] == startPositionStadium && instantiatedCharacters[i] != null)
            {
                // Eliminar la palabra "(Clone)" del nombre del prefab
                textStadium.text = instantiatedCharacters[i].name.Replace("(Clone)", "").Trim();
                break;
            }
        }
    }

    void UpdateSemifinalText()
    {
        // Asignar el nombre del prefab ganador de la llave 2 vs 3 al TextMeshPro de la semifinal
        if (instantiatedCharacters[2] != null && instantiatedCharacters[3] != null)
        {
            int goals2 = int.Parse(startPositionTexts[2].text);
            int goals3 = int.Parse(startPositionTexts[3].text);

            if (goals2 > goals3)
            {
                textSemifinal.text = instantiatedCharacters[2].name.Replace("(Clone)", "").Trim();
            }
            else
            {
                textSemifinal.text = instantiatedCharacters[3].name.Replace("(Clone)", "").Trim();
            }
        }
    }

    public void SaveSemifinalPositions()
    {
        // Guardar las posiciones de las semifinales
        PlayerPrefs.SetFloat("Semifinal1X", semifinal1Position.position.x);
        PlayerPrefs.SetFloat("Semifinal1Y", semifinal1Position.position.y);

        PlayerPrefs.SetFloat("Semifinal2X", semifinal2Position.position.x);
        PlayerPrefs.SetFloat("Semifinal2Y", semifinal2Position.position.y);

        PlayerPrefs.SetFloat("Semifinal3X", semifinal3Position.position.x);
        PlayerPrefs.SetFloat("Semifinal3Y", semifinal3Position.position.y);

        PlayerPrefs.SetFloat("Semifinal4X", semifinal4Position.position.x);
        PlayerPrefs.SetFloat("Semifinal4Y", semifinal4Position.position.y);

        PlayerPrefs.Save();
    }

    // Nueva función para cambiar de escena de semifinal
    public void LoadSemifinalScene()
    {
        SaveSemifinalPositions(); // Guardar las posiciones antes de cargar la escena
        string sceneName = textSemifinal.text;
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or null!");
        }
    }

    // Nueva función para cambiar de escena del estadio
    public void LoadStadiumScene()
    {
        string sceneName = textStadium.text;
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("El nombre de la escena está vacío o es nulo!");
        }
    }

    void UpdateButtons()
    {
        int torneoPref = PlayerPrefs.GetInt("TORNEOPREF", 0);
        if (torneoPref == 0)
        {
            boton1.SetActive(true);
            boton2.SetActive(false);
        }
        else
        {
            boton1.SetActive(false);
            boton2.SetActive(true);
        }
    }
}
