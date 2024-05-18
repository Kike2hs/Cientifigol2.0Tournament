using Mirror;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
    [SerializeField] public LayerMask terrenoLayer;
    public GameObject balon;
    public float moveSpeed;
    public float jumHeight;

    private Joystick joystickController;
    private Rigidbody2D rigidBody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer2D;
    private bool goJump;

    void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer2D = GetComponent<SpriteRenderer>();

        joystickController = GameObject.FindObjectOfType<Joystick>();
    }

    void Update() {
        // Comprobar si el jugador esta congelado
        var isJugadorCongelado = GAMESTATE.Instance.timerJugadorACongelado != 0;
        spriteRenderer2D.color = isJugadorCongelado ? Color.cyan : Color.white;

        if (joystickController != null && joystickController.Horizontal != 0 && !isJugadorCongelado) {
            rigidBody2D.velocity = new Vector2(joystickController.Horizontal < 0 ? -moveSpeed : moveSpeed, rigidBody2D.velocity.y);
        } else if (Input.GetKey(KeyCode.A) && !isJugadorCongelado) {
            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
        }  else if (Input.GetKey(KeyCode.D) && !isJugadorCongelado) {
            rigidBody2D.velocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);
        }  else {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }

        if (isGrounded() && (Input.GetKey(KeyCode.W) || goJump)){
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumHeight);
            FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.SALTO, 1f);
        }
    }

    public void StartSpecialAbility(float scaleMultiplier)
    {
        transform.localScale *= scaleMultiplier;
    }

    public void EndSpecialAbility(float scaleMultiplier)
    {
        transform.localScale /= scaleMultiplier;
    }

    public void jump() { goJump = true; }
    public void releaseJump() { goJump = false; }

    private bool isGrounded() {
        var rayCast = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.down, circleCollider2D.bounds.extents.y + 1, terrenoLayer);
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.down * (circleCollider2D.bounds.extents.y + 1), rayCast.collider != null ? Color.green : Color.red);

        return rayCast.collider != null;
    }
}
