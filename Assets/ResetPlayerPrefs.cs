using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Método para reiniciar los valores de PlayerPrefs a 0
    public void ResetValues()
    {
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefEinstein, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefEinsteinGoles, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefEinsteinGolesContra, 0);

        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefDarwin, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefDarwinGoles, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefDarwinGolesContra, 0);

        // Reiniciar valores para otros jugadores si es necesario
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefCurie, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefCurieGoles, 0);
        PlayerPrefs.SetInt(PlayerPrefsManager.PlayerPrefCurieGolesContra, 0);

        // Guardar los cambios en PlayerPrefs
        PlayerPrefs.Save();
    }
}
