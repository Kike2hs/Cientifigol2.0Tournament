using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMESTATE  
{
    // Singleton
    private static GAMESTATE _instance;
    public static GAMESTATE Instance {
        get {
            if (_instance == null) {
                _instance = new GAMESTATE();
            }
            return _instance;
        }
    }

    public void agregarGol(bool equipoLocal) {
        if (equipoLocal) {
            marcadorEquipoB++; 
        } else {
            marcadorEquipoA++;
        }
    }

    //
    public int marcadorEquipoA { get; private set; } = 0;
    public int marcadorEquipoB { get; private set; } = 0;

    public bool isJugadorAUltimoTocaBalon { get; set; } = true;
    public float timerJugadorACongelado { get; set; } = 0f;
    public float timerJugadorBCongelado { get; set; } = 0f;
}
