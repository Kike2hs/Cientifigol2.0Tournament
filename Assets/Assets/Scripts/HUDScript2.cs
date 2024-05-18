using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScript2 : MonoBehaviour
{
    public TextMeshProUGUI textMarcadorA;
    public TextMeshProUGUI textMarcadorB;


    void Start() {
        InvokeRepeating("OutputTime", 1f, 1f);  //1s delay, repeat every 1s

        if(SystemInfo.deviceType != DeviceType.Handheld) {

        }
    }

    void Update() {
       
    }

    private void FixedUpdate() {
        textMarcadorA.text = $"{GAMESTATE.Instance.marcadorEquipoA}";
        textMarcadorB.text = $"{GAMESTATE.Instance.marcadorEquipoB}";
    }
}
