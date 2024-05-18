using UnityEngine;

public class SwitchGameObjectsButton : MonoBehaviour
{
    public GameObject objectToActivate; // GameObject que se activará al hacer clic en el botón
    public GameObject objectToDeactivate; // GameObject que se desactivará al hacer clic en el botón

    public void OnButtonClick()
    {
        // Activa el GameObject que se debe activar
        objectToActivate.SetActive(true);
        
        // Desactiva el GameObject que se debe desactivar
        objectToDeactivate.SetActive(false);
        
        // Invoca el método para restablecer los GameObjects después de un cierto tiempo (12 segundos en este caso)
        Invoke("ResetGameObjects", 12f);
    }

    // Método para restablecer los GameObjects a su estado original
    private void ResetGameObjects()
    {
        // Desactiva el GameObject que se activó
        objectToActivate.SetActive(false);
        
        // Activa el GameObject que se desactivó
        objectToDeactivate.SetActive(true);
    }
}
