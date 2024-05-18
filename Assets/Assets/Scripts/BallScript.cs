using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject echoBall;
    public float startTimeBtwSpawns;
    public PhysicsMaterial2D extraBouncing;
    public PhysicsMaterial2D normalBouncing;

    private float timeBtwSpawns;
    private Rigidbody2D rigidbodyBall;

    public float timeBouncing = 0f;

    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            var prefab = Instantiate(echoBall, transform.position, Quaternion.identity);
            Destroy(prefab, 0.5f);

            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

        // BOUNCING STATE
        if (timeBouncing > 0)
        {
            timeBouncing -= Time.deltaTime;
        }
        else if (timeBouncing < 0)
        {
            _removeBouncingExtra();
        }

        // ICE STATE
        if (GAMESTATE.Instance.timerJugadorACongelado > 0 || GAMESTATE.Instance.timerJugadorBCongelado > 0)
        {
            if (GAMESTATE.Instance.timerJugadorACongelado > 0)
            {
                GAMESTATE.Instance.timerJugadorACongelado -= Time.deltaTime;
            }
            else
            {
                GAMESTATE.Instance.timerJugadorBCongelado -= Time.deltaTime;
            }
        }
        else if (GAMESTATE.Instance.timerJugadorACongelado < 0 || GAMESTATE.Instance.timerJugadorBCongelado < 0)
        {
            _removePlayerIce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickBouncing = collision.gameObject.name.StartsWith("Bouncing");
        var pickIce = collision.gameObject.name.StartsWith("Ice");
        var pickSizeUp = collision.gameObject.name.StartsWith("SizeUp");

        if (pickBouncing || pickIce || pickSizeUp)
        {
            Destroy(collision.gameObject);
            FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.RECOGER_OBJETO, null);

            if (pickBouncing)
            {
                _addBouncingExtra();
            }
            else if (pickIce)
            {
                _addPlayerIce();
            }
            else if (pickSizeUp)
            {
                _increasePlayerSize();
            }
        }
    }

    private void _addPlayerIce()
    {
        if (GAMESTATE.Instance.isJugadorAUltimoTocaBalon)
        {
            GAMESTATE.Instance.timerJugadorBCongelado = 5f;
        }
        else
        {
            GAMESTATE.Instance.timerJugadorACongelado = 5f;
        }
    }

    private void _removePlayerIce()
    {
        if (GAMESTATE.Instance.timerJugadorACongelado < 0)
        {
            GAMESTATE.Instance.timerJugadorACongelado = 0f;
        }
        if (GAMESTATE.Instance.timerJugadorBCongelado < 0)
        {
            GAMESTATE.Instance.timerJugadorBCongelado = 0f;
        }
    }

    private void _addBouncingExtra()
    {
        this.rigidbodyBall.sharedMaterial = extraBouncing;
        this.GetComponent<SpriteRenderer>().color = Color.magenta;
        timeBouncing = 10f;
    }

    private void _removeBouncingExtra()
    {
        this.rigidbodyBall.sharedMaterial = normalBouncing;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        timeBouncing = 0f;
    }

    private void _increasePlayerSize()
    {
        GameObject player;
        if (GAMESTATE.Instance.isJugadorAUltimoTocaBalon)
        {
            player = GameObject.FindGameObjectWithTag("PlayerA");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("PlayerB");
        }

        if (player != null)
        {
            player.GetComponent<PlayerController>().IncreaseSize(10f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float magnitude = collision.relativeVelocity.magnitude > 400 ? 400f : collision.relativeVelocity.magnitude;
        float volume = magnitude / 400f;

        if (volume > 0.03)
        {
            FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.REBOTE_BALON, volume);
        }

        // Comprobar la etiqueta del GameObject que toca el balón
        if (collision.gameObject.CompareTag("PlayerA") || collision.gameObject.CompareTag("ZapatoA"))
        {
            GAMESTATE.Instance.isJugadorAUltimoTocaBalon = true;
        }
        else if (collision.gameObject.CompareTag("PlayerB") || collision.gameObject.CompareTag("ZapatoB"))
        {
            GAMESTATE.Instance.isJugadorAUltimoTocaBalon = false;
        }
    }
}
