using UnityEngine;

public class BalónTocadoDetector : MonoBehaviour
{
    private bool balónTocado = false;

    public bool BalónTocadoPorJugador()
    {
        return balónTocado;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador ha tocado el balón
        if (collision.gameObject.CompareTag("BALON"))
        {
            balónTocado = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reiniciar la bandera cuando el jugador deja de tocar el balón
        if (collision.gameObject.CompareTag("BALON"))
        {
            balónTocado = false;
        }
    }
}
