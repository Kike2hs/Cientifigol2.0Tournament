using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("Menú"); // Asegúrate de que el nombre de la escena coincida exactamente
    }
}
