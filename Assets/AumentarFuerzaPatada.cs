using UnityEngine;

public class AumentarFuerzaPatada : MonoBehaviour
{
    public float fuerzaNormal = 10f; // Fuerza normal de la patada
    public float fuerzaDoble = 20f; // Fuerza de la patada al doble
    public float duracionDobleFuerza = 12f; // Duración en segundos de la fuerza doble
    public KeyCode botonAumentarFuerza = KeyCode.Space; // Tecla para aumentar la fuerza

    private bool fuerzaDuplicada = false;
    private PlayerControllerOpenheimer playerControllerOpenheimer;

    void Start()
    {
        playerControllerOpenheimer = GetComponent<PlayerControllerOpenheimer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(botonAumentarFuerza))
        {
            ActivarFuerzaDoble();
        }
    }

    void ActivarFuerzaDoble()
    {
        if (!fuerzaDuplicada)
        {
            fuerzaDuplicada = true;
            playerControllerOpenheimer.fuerzaPatadaNormal = fuerzaDoble; // Ajusta la fuerza de la patada al doble
            Invoke("RestaurarFuerzaNormal", duracionDobleFuerza); // Restaura la fuerza normal después de la duración especificada
        }
    }

    void RestaurarFuerzaNormal()
    {
        fuerzaDuplicada = false;
        playerControllerOpenheimer.fuerzaPatadaNormal = fuerzaNormal; // Restaura la fuerza de la patada a su valor normal
    }
}
