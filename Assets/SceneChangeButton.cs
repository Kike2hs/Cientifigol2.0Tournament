using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public string sceneName = "Proof"; // Nombre de la escena a la que quieres avanzar

    // Método llamado cuando se hace clic en el botón
    public void OnButtonClick()
    {
        // Cargar la escena especificada
        SceneManager.LoadScene(sceneName);
    }
}
