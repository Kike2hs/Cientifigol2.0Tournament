using UnityEngine;

public class AumentarFuerzaDisparo : MonoBehaviour
{
    public GameObject balon; // Referencia al balón
    public float fuerzaNormal = 10f; // Fuerza normal del disparo
    public float fuerzaDoble = 20f; // Fuerza del disparo al doble
    public float duracionDobleFuerza = 12f; // Duración en segundos de la fuerza doble
    public KeyCode botonAumentarFuerza = KeyCode.Space; // Tecla para aumentar la fuerza (opcional)

    private bool fuerzaDuplicada = false;

    // Método para aumentar la fuerza del disparo
    public void ActivarFuerzaDoble()
    {
        if (!fuerzaDuplicada)
        {
            fuerzaDuplicada = true;
            Invoke("RestaurarFuerzaNormal", duracionDobleFuerza); // Restaurar la fuerza normal después de la duración especificada
        }
    }

    // Método para restaurar la fuerza normal del disparo
    private void RestaurarFuerzaNormal()
    {
        fuerzaDuplicada = false;
    }

    // Método para realizar el disparo con la fuerza correspondiente
    public void Disparar()
    {
        float fuerzaDisparo = fuerzaDuplicada ? fuerzaDoble : fuerzaNormal; // Verificar si la fuerza está duplicada
        // Aquí colocarías la lógica para disparar con la fuerza determinada (por ejemplo, aplicar fuerza al balón)
    }
}
