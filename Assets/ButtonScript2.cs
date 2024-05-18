using UnityEngine;
using UnityEngine.UI;

public class ButtonScript2 : MonoBehaviour
{
    public GameObject imageToShow;
    public PlayerControllerA playerA;
    public PlayerControllerCurie playerCurie; // Cambio de PlayerControllerB a PlayerControllerCurie
    public float specialDuration = 12f;
    public float playerAScaleMultiplier = 2f;
    public float playerCurieSpeedMultiplier = 0.5f; // Cambio de playerBSpeedMultiplier a playerCurieSpeedMultiplier
    
    public Button button;

    public void ShowImageForDuration()
    {
        imageToShow.SetActive(true);
        playerA.StartSpecialAbility(playerAScaleMultiplier);
        playerCurie.StartSpecialAbility(playerCurieSpeedMultiplier); // Modificación para usar PlayerControllerCurie
        Invoke("HideImageAndResetAbilities", specialDuration);
        button.interactable = false;
    }

    void HideImageAndResetAbilities()
    {
        imageToShow.SetActive(false);
        playerA.EndSpecialAbility(playerAScaleMultiplier);
        playerCurie.EndSpecialAbility(playerCurieSpeedMultiplier); // Modificación para usar PlayerControllerCurie
    }
}
