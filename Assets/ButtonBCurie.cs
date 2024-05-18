using UnityEngine;
using System.Collections;

public class ButtonBCurie : MonoBehaviour
{
    public GameObject objetoActivar;
    public PlayerControllerCurie playerB; // Referencia al controlador del jugador B (PlayerControllerCurie)
    public PlayerControllerA playerA; // Referencia al controlador del jugador A (PlayerControllerA)
    public GameObject efectoPlayerA; // Referencia al GameObject que quieres activar cuando se afecta el tamaño del jugador A
    public float duracionActivacion = 12f;

    private bool activando = false;

    // Método para activar los efectos cuando se toca el balón
    public void ActivarObjeto()
    {
        if (!activando)
        {
            StartCoroutine(ActivarPorTiempo());
        }
    }

    private IEnumerator ActivarPorTiempo()
    {
        activando = true;
        objetoActivar.SetActive(true);

        // Esperar hasta que se toque el balón por alguno de los jugadores
        yield return new WaitUntil(() => playerB.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador() || playerA.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador());

        // Si el jugador B toca el balón, aumentar su tamaño
        if (playerB.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador())
        {
            playerB.StartSpecialAbility(2f); // Aumentar el tamaño del jugador B al doble durante el poder especial
        }

        // Si el jugador A toca el balón, reducir su tamaño a la mitad
        if (playerA.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador())
        {
            playerA.StartSpecialAbility(0.5f); // Reducir el tamaño del jugador A a la mitad durante el poder especial
            efectoPlayerA.SetActive(true); // Activar el efecto para el jugador A
        }

        // Esperar la duración de la activación
        yield return new WaitForSeconds(duracionActivacion);

        // Desactivar los efectos de cambio de tamaño
        if (playerB.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador())
        {
            playerB.EndSpecialAbility(2f); // Restaurar el tamaño original del jugador B
        }
        if (playerA.GetComponent<BalónTocadoDetector>().BalónTocadoPorJugador())
        {
            playerA.EndSpecialAbility(1f); // Restaurar el tamaño original del jugador A (que es 1)
            efectoPlayerA.SetActive(false); // Desactivar el efecto para el jugador A
        }

        // Desactivar el objeto que muestra la activación
        objetoActivar.SetActive(false);
        activando = false;
    }
}
