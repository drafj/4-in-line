using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid2D : MonoBehaviour
{
    public int ancho;//variable que da el ancho del numero de fichas
    public int alto;//variable que da el ancho del numero de fichas

    public GameObject puzzlePiece;//variable donde se establece la pieza que se usara
    private GameObject[,] cub;//array para crear las piezas del 4 en linea
    private bool juego = true;//booleana para terminar el juego

    public Color jugadorUno;//el color del jugador uno
    public Color jugadorDos;//el color del jugador dos
    public bool turnos;//variable que establece los turnos

    public int rule1;//variable de la regla propia para hacer que gane el jugador uno
    public int rule2;//variable de la regla propia para hacer que gane el jugador dos

    public GameObject cartelWin;//carteles de victoria
    public Material winMaterial;
    public GameObject gana1;
    public GameObject gana2;
    public GameObject ganates;

    void Start()
    {
        juego = false;

        gana1 = GameObject.Find("Text1");//se desactivan los carteles de victoria para despues activarlos al ganar
        gana2 = GameObject.Find("Text2");
        ganates = GameObject.Find("ganates");
        gana1.SetActive(false);
        gana2.SetActive(false);
        ganates.SetActive(false);
    } 
    
    public void CubeCreator()
    {
        juego = true;

        cub = new GameObject[ancho, alto];//las siguientes lineas de codigo crean las piezas del cuatro en linea
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                GameObject cube = GameObject.Instantiate(puzzlePiece) as GameObject;
                Vector3 position = new Vector3(x, y, 0);
                cube.transform.position = position;

                cube.GetComponent<Renderer>().material.color = Color.black;

                cub[x, y] = cube;


            }
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (juego==true)//condicional que activa y desactiva el juego
        {
            Vector3 mposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selec(mposition);
        }
    }
    
    public void selec(Vector3 posicion)//controla los turnos de los jugadores y hace que funcione la regla principal del cuatro en linea
    {
        int x = (int) (posicion.x + 0.5f);        
        int y = (int) (posicion.y + 0.5f);

        if (Input.GetButtonDown("Fire1"))
        {
            if (x >= 0 && y >= 0 && x < ancho && y < alto)
            {
                GameObject cube = cub[x,y];
                if (cube.GetComponent<Renderer>().material.color == Color.black)
                {
                    Color colorActual = Color.clear;
                    if (turnos)
                        colorActual = jugadorDos;
                    else
                        colorActual = jugadorUno;
                    cube.GetComponent<Renderer>().material.color = colorActual;
                    turnos = !turnos;
                    verificacionHorizontal(x, y, colorActual);
                    verificacionVertical(x, y, colorActual);
                    verificacionDiagArriba(x, y, colorActual);
                    verificacionDiagAbajo(x, y, colorActual);
                    rule34(rule1, rule2);//llama la funcion de la regla propia para que funcione
                }
            }
        }

        void verificacionHorizontal(int i, int j, Color colorActual)//verifica si hay cuatro en linea horizontalmente
        {
            int contadorPrincipal = 0;
            for (int a = i-3; a <= i+3; a++)
            {
                if (a >= 0 && a < ancho)
                {
                    GameObject cube = cub[a, y];

                    if (cube.GetComponent <Renderer>().material.color == colorActual)
                    {
                        contadorPrincipal++;
                        if (contadorPrincipal == 4)
                        {

                            if (colorActual == jugadorUno)
                                rule1++;
                            else
                                rule2++;
                        }
                    }
                }
                else
                    contadorPrincipal = 0;
            }
        }

        void verificacionVertical(int i, int j, Color colorActual)//verifica si hay cuatro en linea verticalmente
        {
            int contadorPrincipal = 0;
            int e = 0;
            if (j >= 3)
                e = j - 3;
            else          
                e = j;    
            for (int b = j-3; b <= j+3; b++)
            {
                if (b >= 0 && b < alto)
                {
                    GameObject cube = cub[x, b];

                    if (cube.GetComponent <Renderer>().material.color == colorActual)
                    {
                        contadorPrincipal++;
                        if (contadorPrincipal == 4)
                        {
                           
                            if (colorActual == jugadorUno)
                                rule1++;
                            else
                                rule2++;
                        }
                    }
                    else
                        contadorPrincipal = 0;
                }
            }
        }

        void verificacionDiagArriba(int i, int j, Color colorActual)//verifica una de las diagonales
        {
            

            int contadorPrincipal = 0;
            int b = j-3;
            for (int a = i - 3; a <= i + 3; a++)
            {
                if (a >= 0 && a < ancho && b >= 0 && b < alto)
                {
                    GameObject cube = cub[a, b];

                    if (cube.GetComponent<Renderer>().material.color == colorActual)
                    {

                        contadorPrincipal++;
                        if (contadorPrincipal == 4)
                        {
                            
                            if (colorActual == jugadorUno)
                                rule1++;
                            else
                                rule2++;
                        }
                    }
                    else
                    {
                        contadorPrincipal = 0;
                    }
                }
                b++;
            }
        }

        void verificacionDiagAbajo(int i, int j, Color colorActual)//verifica la otra diagonal
        {
            int contadorPrincipal = 0;
            int b = j+3;
            for (int a = i-3;a <= i+3; a++)
            {
                if (a >= 0 && a < ancho && b >= 0 && b < alto)
                {
                    GameObject cube = cub[a, b];

                    if (cube.GetComponent<Renderer>().material.color == colorActual)
                    {
                        contadorPrincipal++;

                        if (contadorPrincipal == 4)
                        {
                            
                            if (colorActual == jugadorUno)
                                rule1++;
                            else
                                rule2++;
                        }
                    }
                    else
                        contadorPrincipal = 0;
                }
                b--;
            }
        }

        void rule34(int rule1, int rule2)//funcion de la regla propia de mi 4 en linea que consiste en que debes ganar dos veces
        {
            if (rule1 == 2)
            {
                cartelWin = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cartelWin.transform.localScale = new Vector3(18.03424f, 14.96514f, 17.4f);
                cartelWin.transform.localPosition = new Vector3(4.62f, 4.42f, -0.6f);
                cartelWin.GetComponent<Renderer>().material = winMaterial;

                gana1.SetActive(true);
                ganates.SetActive(true);

                juego = false;
            }
            else if (rule2 == 2)
            {
                cartelWin = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cartelWin.transform.localScale = new Vector3(18.03424f, 14.96514f, 17.4f);
                cartelWin.transform.localPosition = new Vector3(4.62f, 4.42f, -0.6f);
                cartelWin.GetComponent<Renderer>().material = winMaterial; ;

                gana2.SetActive(true);
                ganates.SetActive(true);

                juego = false;
            }
        }
    }

}
