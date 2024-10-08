using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 screenBounds;
    static public GameManager instance;
    float speed = 1
    [SerializeField] GameObject AreaPrefab;
    [SerializeField] GameObject menu;
   

    public float Speed { get => speed; set => speed = value; }
    public Vector2 ScreenBounds { get => screenBounds; }

    private void Awake()
    {
        instance = this;
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height));
       
    }
    private void Start()
    {
        menu = GameObject.Find("Menu Window");
        GerarGrade();
    }
    public void IniciarJogo()
    {
        Camera.main.transform.position = new Vector3(Obstacle.instance.diametroDoCampo / 2f - 0.5f, Obstacle.instance.diametroDoCampo / 2f - 0.5f, -10);
        Camera.main.orthographicSize = Obstacle.instance.diametroDoCampo / 2f;
        menu.SetActive(false);

    }

    void GerarGrade()
    {
        grade = new int[Obstacle.instance.diametroDoCampo, Obstacle.instance.diametroDoCampo];
    }
    public void DefinirDIametro(string value)
    {
    Obstacle.instance.diametroDoCampo = int.Parse(value);
    }
  
}
