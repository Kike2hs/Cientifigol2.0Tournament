using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour
{
    public GameObject[] playerObjects;
    public int selectedCharacter = 0;
    public string gameScene = "Game Scene";
    private string selectedCharacterDataName = "SelectedCharacter";

    void Start()
    {
        HideAllCharacters();

        if (selectedCharacter >= 0 && selectedCharacter < playerObjects.Length)
        {
            playerObjects[selectedCharacter].SetActive(true);
        }
        else
        {
            Debug.LogError("Selected character index is out of bounds!");
        }
    }

    private void HideAllCharacters()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    public void NextCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerObjects.Length)
        {
            selectedCharacter = 0;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = playerObjects.Length - 1;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        // Guardar el índice del personaje seleccionado
        PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        
        // Resetear el estado del torneo
        PlayerPrefs.SetInt("TournamentSaved", 0);

        // Cargar la escena del juego
        SceneManager.LoadScene(gameScene);
    }
}
