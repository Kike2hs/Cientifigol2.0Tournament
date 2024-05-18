    using UnityEngine;

    public class PlayerControllerB : MonoBehaviour
    {
        [SerializeField] public LayerMask terrenoLayer;
        public GameObject balon;
        public float moveSpeed;
        public float jumpHeight;
        public bool isPowerActive = false; // Variable para verificar si el poder está activo

        private float originalJumpHeight; // Variable para almacenar la altura de salto original
        private Rigidbody2D rigidBody2D;
        private CircleCollider2D circleCollider2D;
        private SpriteRenderer spriteRenderer2D;
        private bool goJump;
        public ButtonB buttonB; // Referencia al script ButtonB

        void Start()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            spriteRenderer2D = GetComponent<SpriteRenderer>();
            originalJumpHeight = jumpHeight; // Almacenar la altura de salto original al inicio
        }

        void Update()
        {
            // Comprobar si el jugador está congelado
            var isJugadorCongelado = GAMESTATE.Instance.timerJugadorBCongelado != 0;
            spriteRenderer2D.color = isJugadorCongelado ? Color.cyan : Color.white;

            var posBalonX = balon.transform.position.x;
            var posBot = transform.position.x;

            if (posBalonX >= (posBot + balon.transform.localScale.x + 1) && !isJugadorCongelado)
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
            else if (posBalonX < posBot && !isJugadorCongelado)
            {
                rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y); // MOVERSE A LA IZQUIERDA, SIGUIENDO AL BALON
            }

            var distanceBalon = Vector3.Distance(transform.position, balon.transform.position);
            var posBalonY = balon.transform.position.y;

            if (isGrounded() && posBalonY > -6 && distanceBalon < 15)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
                FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.SALTO, 1f);
            }
        }

        public void StartSpecialAbility(float heightMultiplier)
        {
            jumpHeight *= heightMultiplier; // Multiplicar la altura del salto
            isPowerActive = true; // Activar el poder especial
        }

        public void EndSpecialAbility(float heightMultiplier)
        {
            jumpHeight = originalJumpHeight; // Restaurar la altura de salto original
            isPowerActive = false; // Desactivar el poder especial
        }

        public void Jump()
        {
            goJump = true;
        }

        public void ReleaseJump()
        {
            goJump = false;
        }

        private bool isGrounded()
        {
            var rayCast = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.down, circleCollider2D.bounds.extents.y + 1, terrenoLayer);
            Debug.DrawRay(circleCollider2D.bounds.center, Vector2.down * (circleCollider2D.bounds.extents.y + 1), rayCast.collider != null ? Color.green : Color.red);

            return rayCast.collider != null;
        }

        // Detectar si se presiona el botón para activar la habilidad especial
        void FixedUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                buttonB.ActivateSpecialJump();
            }
        }

        // Girar en el aire cuando se intenta dar una patada al balón
        public void KickBall()
        {
            if (isPowerActive && !isGrounded())
            {
                transform.Rotate(Vector3.forward * 360f);
            }
        }
    }
