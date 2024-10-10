using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 screenBounds;
    
  
   [SerializeField]  GameObject AreaPrefab;
    [SerializeField] GameObject menu;
    public int diametroDoCampo;
    public int[,] grade;
    [SerializeField] public GameObject maca;



    
    public Vector2 ScreenBounds { get => screenBounds; }
    #region Singleton
    static public GameManager instance;
    private void Awake()
    {
        instance = this;
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height));
       
    }
    #endregion
    private void Start()
    {
        GerarGrade();
    }
    public void IniciarJogo()
    {
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;
        menu.SetActive(false);

    }

    void GerarGrade()
    {
       grade = new int[diametroDoCampo,diametroDoCampo];
    }
    public void DefinirDIametro(string value)
    {
    diametroDoCampo = int.Parse(value);
    }
  
}
