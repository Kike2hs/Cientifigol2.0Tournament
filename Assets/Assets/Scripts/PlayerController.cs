using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public LayerMask terrenoLayer;
    public GameObject balon;
    public float moveSpeed;
    public float jumHeight;
    public bool isJugadorA;

    private Joystick joystickController;
    private Rigidbody2D rigidBody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer2D;
    private bool goJump;
    private Vector3 originalScale;
    private bool isSizeIncreased = false;

    private static PlayerController playerAInstance;

    void Awake() {
        if (isJugadorA) {
            playerAInstance = this;
        }
    }

    void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer2D = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        if (isJugadorA) {
            AssignControlsToPlayerA();
        }
    }

    void Update() {
        if (isJugadorA) {
            var isJugadorCongelado = GAMESTATE.Instance.timerJugadorACongelado != 0;
            spriteRenderer2D.color = isJugadorCongelado ? Color.cyan : Color.white;

            if (joystickController != null && joystickController.Horizontal != 0 && !isJugadorCongelado) {
                rigidBody2D.velocity = new Vector2(joystickController.Horizontal < 0 ? -moveSpeed : moveSpeed, rigidBody2D.velocity.y);
            } else if (Input.GetKey(KeyCode.A) && !isJugadorCongelado) {
                rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
            } else if (Input.GetKey(KeyCode.D) && !isJugadorCongelado) {
                rigidBody2D.velocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);
            } else {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }

            if (isGrounded() && (Input.GetKey(KeyCode.W) || goJump)) {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumHeight);
                FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.SALTO, 1f);
            }
        } else {
            var isJugadorCongelado = GAMESTATE.Instance.timerJugadorBCongelado != 0;
            spriteRenderer2D.color = isJugadorCongelado ? Color.cyan : Color.white;

            var posBalonX = balon.transform.position.x;
            var posBot = this.transform.position.x;

            if (posBalonX >= (posBot + balon.transform.localScale.x + 1) && !isJugadorCongelado) {
                if (posBalonX < 15) {
                    rigidBody2D.velocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);
                } else if (posBalonX > 0 && (this.transform.position.x > -25)) {
                    rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
                }
            } else if (posBalonX < posBot && !isJugadorCongelado) {
                rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
            }

            var distanceBalon = Vector3.Distance(this.transform.position, balon.transform.position);
            var posBalonY = balon.transform.position.y;

            if (isGrounded() && posBalonY > -6 && distanceBalon < 15) {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumHeight);
                FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.SALTO, 1f);
            }
        }
    }

    public void JumpButtonDown() {
        if (isJugadorA) {
            goJump = true;
        }
    }

    public void JumpButtonUp() {
        if (isJugadorA) {
            goJump = false;
        }
    }

    private bool isGrounded() {
        var rayCast = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.down, circleCollider2D.bounds.extents.y + 1, terrenoLayer);
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.down * (circleCollider2D.bounds.extents.y + 1), rayCast.collider != null ? Color.green : Color.red);

        return rayCast.collider != null;
    }

    public void IncreaseSize(float duration) {
        if (!isSizeIncreased) {
            StartCoroutine(IncreaseSizeCoroutine(duration));
        }
    }

    private IEnumerator IncreaseSizeCoroutine(float duration) {
        isSizeIncreased = true;
        transform.localScale = originalScale * 2;
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
        isSizeIncreased = false;
    }

    public static void AssignControlsToPlayerA() {
        if (playerAInstance != null) {
            var joystick = GameObject.FindObjectOfType<Joystick>();
            var jumpButton = GameObject.Find("JumpButton").GetComponent<EventTrigger>();
            var kickButton = GameObject.Find("KickButton").GetComponent<EventTrigger>();

            if (joystick != null) {
                playerAInstance.joystickController = joystick;
            }

            if (jumpButton != null) {
                EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
                pointerDownEntry.callback.AddListener((data) => { playerAInstance.JumpButtonDown(); });
                jumpButton.triggers.Add(pointerDownEntry);

                EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
                pointerUpEntry.callback.AddListener((data) => { playerAInstance.JumpButtonUp(); });
                jumpButton.triggers.Add(pointerUpEntry);
            }

            if (kickButton != null) {
                var footScript = playerAInstance.GetComponentInChildren<FootScript>();
                if (footScript != null) {
                    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
                    pointerDownEntry.callback.AddListener((data) => { footScript.kickBall(); });
                    kickButton.triggers.Add(pointerDownEntry);

                    EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
                    pointerUpEntry.callback.AddListener((data) => { footScript.releaseFoot(); });
                    kickButton.triggers.Add(pointerUpEntry);
                }
            }
        }
    }

    public static void FindPlayerA() {
        GameObject playerA = GameObject.FindWithTag("PlayerA");
        if (playerA != null) {
            playerAInstance = playerA.GetComponent<PlayerController>();
            AssignControlsToPlayerA();
        }
    }
}
