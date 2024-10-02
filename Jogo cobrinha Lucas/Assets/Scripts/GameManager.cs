using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 screenBounds;
    static public GameManager instance;
    float speed = 1;

    public float Speed { get => speed; set => speed = value; }
    public Vector2 ScreenBounds { get => screenBounds; }

    private void Awake()
    {
        instance = this;
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height));
    }
}
