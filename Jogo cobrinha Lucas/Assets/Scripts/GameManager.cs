using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 screenBounds;
    static public GameManager instance;
    float speed = 1;
    public int diametroDoCampo;
    public int[,] grade;
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

    void GerarGrade()
    {
        grade = new int[diametroDoCampo,diametroDoCampo];
    }
    public void CameraSeguidora()
    {
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;
    }
        public void DefinirDIametro(string value)
        {
            diametroDoCampo = int.Parse(value);
        }
    public void IniciarJogo()
    {
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;  
        menu.SetActive(false);
        

    }
}
