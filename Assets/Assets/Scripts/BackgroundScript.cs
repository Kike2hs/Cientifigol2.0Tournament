using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Sprite[] frames;
    public int fps = 10;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        int index = (int)(Time.time * fps) % frames.Length;
        spriteRenderer.sprite = frames[index];
    }
}
