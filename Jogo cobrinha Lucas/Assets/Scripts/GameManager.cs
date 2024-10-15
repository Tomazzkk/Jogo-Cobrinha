using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 screenBounds;
    
    float speed = 1;
   
    [SerializeField] GameObject menu;
    public int diametroDoCampo;
    public int[,] grade;
    [SerializeField] public GameObject maca;
    [SerializeField] int numeroDeMaca;
   
     
    
    

    public float Speed { get => speed; set => speed = value; }
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

        Camera.main.transform.position = new Vector3(diametroDoCampo / 2.0f - 2.0f, diametroDoCampo / 2.0f - 11.0f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;
        menu.SetActive(false);

    }

    void GerarGrade()
    {
       grade = new int [diametroDoCampo,diametroDoCampo];
        for (int i = 0; i < diametroDoCampo; i++)
        {
            for (int j = 0; j < diametroDoCampo; j++)
            {
               i = Random.Range(diametroDoCampo, diametroDoCampo);
               j = Random.Range(diametroDoCampo, diametroDoCampo);
                Instantiate(maca, new Vector2(i, j), Quaternion.identity);
                
            }
        }
    }
    public void DefinirDIametro(string value)
    {
    diametroDoCampo = int.Parse(value);
    }
  
  
}
