using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Camara : MonoBehaviour
{

    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 100;

    void Start()
    {
        var _camera = GetComponent<Camera>();
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        _camera.orthographicSize = desiredHalfHeight;
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        
    }
}
