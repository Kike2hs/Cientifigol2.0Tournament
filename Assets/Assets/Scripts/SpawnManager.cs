using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objects;

    private float timeSpawnObject = 0.0f;
    private Dictionary<GameObject, float> objectsTTL = new Dictionary<GameObject, float>();

    void Start()  {
        timeSpawnObject = Random.Range(3f, 7f);
        
    }

    void Update() {
        // SPAWN OBJECT EACH 8-10 SECONDS
        if (timeSpawnObject > 0) {
            timeSpawnObject -= Time.deltaTime;
        }

        if(timeSpawnObject < 0) {
            SpawnObject();
        }

        checkRemoveObject();
    }

    void SpawnObject() {
        timeSpawnObject      = Random.Range(8f, 10f);

        var gameObject         = objects[Random.Range(0, objects.Length)];
        var posicionEnPantalla = new Vector3(Random.Range(-55, 55), Random.Range(5, 28), 0);
        var tiempoEnPantalla   = Random.Range(8f, 15f);

        objectsTTL.Add(Instantiate(gameObject, posicionEnPantalla, gameObject.transform.rotation), tiempoEnPantalla);
    }

    void checkRemoveObject() {
        foreach (var entryKey in objectsTTL.Keys.ToList()) {
            objectsTTL[entryKey] -= Time.deltaTime;
            if (objectsTTL[entryKey] < 0) {
                objectsTTL.Remove(entryKey);
                Destroy(entryKey);
            }
        }
    }
}
