using UnityEngine;
using UnityEngine.UI; // Asegúrate de importar UnityEngine.UI para acceder al componente Button

public class ButtonScript : MonoBehaviour
{
    public GameObject imageToShow;
    public PlayerControllerA playerA;
    public PlayerControllerB playerB;
    public float specialDuration = 12f;
    public float playerAScaleMultiplier = 2f;
    public float playerBSpeedMultiplier = 0.5f;
    
    // Agrega una referencia al componente Button
    public Button button;

    public void ShowImageForDuration()
    {
        imageToShow.SetActive(true);
        playerA.StartSpecialAbility(playerAScaleMultiplier);
        playerB.StartSpecialAbility(playerBSpeedMultiplier); // Aquí llamamos al método correspondiente en PlayerControllerB
        Invoke("HideImageAndResetAbilities", specialDuration); // Ocultar la imagen y restablecer las habilidades después de 12 segundos
        
        // Desactiva el componente Button después de que se ha presionado una vez
        button.interactable = false;
    }

    void HideImageAndResetAbilities()
    {
        imageToShow.SetActive(false);
        playerA.EndSpecialAbility(playerAScaleMultiplier);
        playerB.EndSpecialAbility(playerBSpeedMultiplier); // Aquí llamamos al método correspondiente en PlayerControllerB
    }
}
