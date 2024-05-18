using UnityEngine;

public class PlayerManager1 : MonoBehaviour
{
    public GameObject jugadorA;

    void Start()
    {
        // Verificar si el jugador A no está instanciado
        if (jugadorA == null)
        {
            // Buscar dinámicamente el prefab del jugador A por etiqueta
            GameObject jugadorAPrefab = Resources.Load<GameObject>("PlayerAPrefab");

            if (jugadorAPrefab == null)
            {
                // Si no se encuentra por etiqueta, intenta buscar por nombre
                jugadorAPrefab = GameObject.FindGameObjectWithTag("PlayerA");
            }

            // Instanciar al jugador A desde el prefab si se encontró
            if (jugadorAPrefab != null)
            {
                jugadorA = Instantiate(jugadorAPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Player A prefab not found!");
            }
        }
        else
        {
            Debug.Log("jugadorA already exists"); // Opcional: añadir un mensaje de depuración
        }
    }
}
