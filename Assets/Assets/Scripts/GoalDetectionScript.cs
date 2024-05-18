using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetectionScript : MonoBehaviour
{
    private GameObject jugadorB; // Eliminamos la referencia al jugador A

    public GameObject balon;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D balonRigidbody2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        balonRigidbody2D = balon.GetComponent<Rigidbody2D>();

        // No necesitamos la lógica para instanciar al jugador A aquí
        // Esa responsabilidad será manejada por el PlayerManager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BALON"))
        {
            //spriteRenderer.color = Color.green;
            FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.GOL, null);
            var isGolPorteriaLocal = this.gameObject.CompareTag("GOL_DETECTOR_A");
            GAMESTATE.Instance.agregarGol(isGolPorteriaLocal);

            // No necesitamos la lógica para resetear la posición del jugador A aquí
            // Esa responsabilidad será manejada por el PlayerManager

            // No necesitas hacer nada con jugadorB porque no lo has asignado dinámicamente
            balon.transform.position = new Vector3(0, 15, 1);

            //balonRigidbody2D.velocity   = new Vector3(25, 50, 0);
            var randomPoder = Random.Range(30f, 20f);
            balonRigidbody2D.velocity = new Vector3(isGolPorteriaLocal ? randomPoder : -randomPoder, Random.Range(10f, 40f), 0);

            // Quitar congelamiento a jugadores
            GAMESTATE.Instance.timerJugadorACongelado = 0f;
            GAMESTATE.Instance.timerJugadorBCongelado = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BALON"))
        {
            spriteRenderer.color = Color.red;
        }
    }
}
