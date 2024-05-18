using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool habilitarMusica  = true;
    private bool habilitarSonidos = true;

    void Start()
    {
        GameObject.Find("Sonidos").GetComponent<Button>().onClick.AddListener(onclickSonido);
        GameObject.Find("Musica").GetComponent<Button>().onClick.AddListener(onClickMusica);
        GameObject.Find("Ayuda").GetComponent<Button>().onClick.AddListener(onclickAyuda);
        GameObject.Find("NuevoTorneo").GetComponent<Button>().onClick.AddListener(onClikNuevoTorneo);
    }

    void Update()
    {

    }

    void onClikNuevoTorneo() {
        FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.BOTON_CLICK, 1);
        SceneManager.LoadScene("Juego");
    }

    void onclickSonido() {
        this.habilitarSonidos = !habilitarSonidos;
        GameObject.Find("IconoSonido").GetComponent<RawImage>().color = this.habilitarSonidos ? new Color(0.30f, 0.62f, 1) : Color.grey;
        FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.BOTON_CLICK, 1);
    }

    void onClickMusica(){
        this.habilitarMusica = !habilitarMusica;
        GameObject.Find("IconoMusica").GetComponent<RawImage>().color = this.habilitarMusica ? new Color(0.30f, 0.62f, 1) : Color.grey;
        FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.BOTON_CLICK, 1);
    }

    void onclickAyuda() {
        FindObjectOfType<SoundManagerScript>().playSound(TipoSonido.BOTON_CLICK, 1);
    }

}
