using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // Tiempo total en segundos
    private float currentTime; // Tiempo actual
    private TextMeshProUGUI textMeshPro; // Referencia al TextMeshPro
    public GameObject firstGameObjectToShow; // Primer GameObject a mostrar cuando el temporizador llegue a cero
    public GameObject secondGameObjectToShow; // Segundo GameObject a mostrar después de un segundo
    public GameObject thirdGameObjectToShow; // Tercer GameObject a mostrar después de un tiempo adicional
    public GameObject fourthGameObjectToShow; // Cuarto GameObject a mostrar después de un tiempo adicional
    public GameObject fifthGameObjectToShow; // Quinto GameObject a mostrar después de un tiempo adicional
    public GameObject objectToHide; // GameObject que se ocultará cuando el temporizador llegue a cero
    public AudioSource audioSource; // Referencia al AudioSource que contiene el sonido a reproducir

    private bool firstObjectShown = false;
    private bool objectsShownAfterTimer = false;
    private bool secondObjectShown = false;
    private bool thirdObjectShown = false;

    void Start()
    {
        // Obtener la referencia al TextMeshPro
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        // Establecer el tiempo inicial
        currentTime = totalTime;

        // Desactivar los GameObjects al inicio
        firstGameObjectToShow.SetActive(false);
        secondGameObjectToShow.SetActive(false);
        thirdGameObjectToShow.SetActive(false);
        fourthGameObjectToShow.SetActive(false);
        fifthGameObjectToShow.SetActive(false);

        // Ocultar el objeto si está configurado
        if (objectToHide != null)
        {
            // No ocultar el objeto al inicio
            // objectToHide.SetActive(false);
        }
    }

    void Update()
    {
        // Disminuir el tiempo
        currentTime -= Time.deltaTime;

        // Verificar si el tiempo ha llegado a cero
        if (currentTime <= 0f)
        {
            currentTime = 0f; // Asegurarse de que el tiempo no sea negativo
            // Activar el primer GameObject cuando el temporizador llegue a cero
            if (!firstObjectShown)
            {
                firstObjectShown = true;
                firstGameObjectToShow.SetActive(true);
                // Reproducir el sonido cuando el primer objeto aparezca
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }

            // Ocultar el objeto si está configurado
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }

            // Mostrar los objetos después de que el temporizador haya llegado a cero
            if (!objectsShownAfterTimer)
            {
                objectsShownAfterTimer = true;
                fourthGameObjectToShow.SetActive(true);
                fifthGameObjectToShow.SetActive(true);
                Time.timeScale = 0f; // Pausar el juego
                Invoke("ShowRemainingObjects", 1.5f); // Invocar la función después de 1 segundo

                        Time.timeScale = 1f; // Reanudar el juego

            }
        }

        // Actualizar el texto del TextMeshPro con el tiempo restante formateado en minutos y segundos
        textMeshPro.text = FormatTime(currentTime);
    }

    // Función para mostrar los objetos restantes después de que el temporizador llegue a cero
    void ShowRemainingObjects()
    {
        secondGameObjectToShow.SetActive(true);
        thirdGameObjectToShow.SetActive(true);
        secondObjectShown = true;
        thirdObjectShown = true;
    }

    // Función para formatear el tiempo en minutos y segundos
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
