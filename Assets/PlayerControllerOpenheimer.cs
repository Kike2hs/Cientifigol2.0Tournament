using UnityEngine;

public class PlayerControllerOpenheimer : MonoBehaviour
{
    public GameObject balon;
    public float moveSpeed;
    public float fuerzaPatadaNormal = 10f; // Fuerza normal de la patada
    public float fuerzaPatadaDoble = 20f; // Fuerza de la patada al doble
    public float duracionDobleFuerza = 12f; // Duración en segundos de la fuerza doble
    public KeyCode botonAumentarFuerza = KeyCode.Space; // Tecla para aumentar la fuerza

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer2D;
    private bool fuerzaDuplicada = false;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer2D = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Verificar si se presionó la tecla para aumentar la fuerza de la patada
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
            Invoke("RestaurarFuerzaNormal", duracionDobleFuerza); // Restaurar la fuerza normal después de la duración especificada
        }
    }

    void RestaurarFuerzaNormal()
    {
        fuerzaDuplicada = false;
    }

    void FixedUpdate()
    {
        // Mover al jugador siguiendo al balón
        MoverSeguirBalon();
    }

    void MoverSeguirBalon()
    {
        var posBalonX = balon.transform.position.x;
        var posBot = transform.position.x;

        if (posBalonX >= (posBot + balon.transform.localScale.x + 1))
        { // MOVERSE A LA DERECHA, SIGUIENDO AL BALON

            if (posBalonX < 15)
            {     // SEGUIR AL BALON, SOLO SI ESTA EN SU PARTE DE LA CANCHA
                rigidBody2D.velocity = new Vector2(moveSpeed, rigidBody2D.velocity.y); // X
            }
            else if (posBalonX > 0 && (transform.position.x > -25))
            {
                rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y); // X
            }

        }
        else if (posBalonX < posBot)
        {
            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y); // MOVERSE A LA IZQUIERDA, SIGUIENDO AL BALON
        }
    }

    // Patear el balón
    public void KickBall()
    {
        float fuerzaPatada = fuerzaDuplicada ? fuerzaPatadaDoble : fuerzaPatadaNormal; // Verificar si la fuerza está duplicada
        balon.GetComponent<Rigidbody2D>().velocity = new Vector2(fuerzaPatada, 0); // Aplicar fuerza al balón
    }
}
