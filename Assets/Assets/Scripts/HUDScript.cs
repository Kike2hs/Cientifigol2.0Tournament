using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public TextMeshProUGUI textMarcadorA;
    public TextMeshProUGUI textMarcadorB;
    public TextMeshProUGUI FPS;
    public GameObject KickBtn;
    public GameObject JumpBtn;
    public GameObject joysTick;

    void Start() {
        InvokeRepeating("OutputTime", 1f, 1f);  //1s delay, repeat every 1s

        if(SystemInfo.deviceType != DeviceType.Handheld) {
            KickBtn.SetActive(true);
            JumpBtn.SetActive(true);
            joysTick.SetActive(true);
        }
    }

    void Update() {
       
    }

    void OutputTime() {
        FPS.text = $"{(int)(1.0f / Time.smoothDeltaTime)} fps";
    }

    private void FixedUpdate() {
        textMarcadorA.text = $"{GAMESTATE.Instance.marcadorEquipoA}";
        textMarcadorB.text = $"{GAMESTATE.Instance.marcadorEquipoB}";
    }
}
