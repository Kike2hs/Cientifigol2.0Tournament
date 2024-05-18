using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] characterImages; // Nueva lista de imágenes desactivadas
    public Transform startPosition; // Variable para la posición inicial
    public string menuScene = "Character Selection Menu";
    private string selectedCharacterDataName = "SelectedCharacter";
    private int selectedCharacter;
    private GameObject playerObject;
    private string selectedCharacterName; // Nuevo: para almacenar el nombre del personaje

    public TextMeshProUGUI characterNameText1; // Referencia al primer componente TextMeshPro en la interfaz de usuario
    public TextMeshProUGUI characterNameText2; // Referencia al segundo componente TextMeshPro en la interfaz de usuario

    void Start()
    {
        PlayerController.FindPlayerA();
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
        if (selectedCharacter >= 0 && selectedCharacter < characters.Length)
        {
            // Obtener el nombre del prefab usando el nombre del GameObject asociado al prefab
            string characterName = characters[selectedCharacter].name;
            Debug.Log("Nombre del personaje: " + characterName); // Para depuración

            // Instanciar el personaje en la posición inicial
            playerObject = Instantiate(characters[selectedCharacter], startPosition.position, Quaternion.identity);
            selectedCharacterName = characterName; // Guardar el nombre del personaje

            // Activar la imagen correspondiente al índice del prefab seleccionado
            ActivateCharacterImage(selectedCharacter);
        }
        else
        {
            Debug.LogError("Selected character index is out of bounds!");
        }

        // Asignar el nombre del personaje a ambos TextMeshPro
        if (characterNameText1 != null)
        {
            characterNameText1.text = selectedCharacterName;
        }
        if (characterNameText2 != null)
        {
            characterNameText2.text = selectedCharacterName;
        }
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

    private void ActivateCharacterImage(int characterIndex)
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            if (i == characterIndex)
            {
                characterImages[i].SetActive(true);
            }
            else
            {
                characterImages[i].SetActive(false);
            }
        }
    }
}
