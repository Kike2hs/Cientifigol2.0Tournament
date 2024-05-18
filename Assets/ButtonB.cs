using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonB : MonoBehaviour
{
    public PlayerControllerB playerControllerB;
    public List<GameObject> itemsToActivate; // Lista de GameObjects a activar
    public float activationDuration = 12f;
    public float jumpHeightMultiplier = 2f;
    private float originalJumpHeight; // Variable para almacenar la altura de salto original
    public Button button; // Referencia al componente Button

    private bool buttonPressed = false; // Variable para controlar si el botón ha sido presionado

    public void ActivateSpecialJump()
    {
        // Verificar si el botón ya ha sido presionado
        if (!buttonPressed)
        {
            // Almacenar la altura de salto original
            originalJumpHeight = playerControllerB.jumpHeight;

            // Activar cada objeto en la lista
            foreach (var item in itemsToActivate)
            {
                item.SetActive(true);
            }

            // Activar la habilidad especial de salto del jugador
            playerControllerB.StartSpecialAbility(jumpHeightMultiplier);

            // Desactivar los objetos y la habilidad especial después de la duración especificada
            Invoke("DeactivateSpecialJump", activationDuration);

            // Desactivar el botón después de presionarlo una vez
            button.interactable = false;

            // Marcar el botón como presionado
            buttonPressed = true;
        }
    }

    void DeactivateSpecialJump()
    {
        // Desactivar cada objeto en la lista
        foreach (var item in itemsToActivate)
        {
            item.SetActive(false);
        }

        // Desactivar la habilidad especial de salto del jugador
        playerControllerB.EndSpecialAbility(jumpHeightMultiplier);

        // Restaurar la altura de salto original
        playerControllerB.jumpHeight = originalJumpHeight;
    }
}
