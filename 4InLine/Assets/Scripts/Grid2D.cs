using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    public int ancho;
    public int alto;
    public GameObject puzzlePiece;
    private GameObject[,] cub; 
    public Color jugadorUno;
    public Color jugadorDos;
    public bool turnos;
    public int rule1;
    public int rule2;


    void Start()
    {
        cub = new GameObject[ancho,alto];
        for (int x = 0;  x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                GameObject cube = GameObject.Instantiate(puzzlePiece) as GameObject;
                Vector3 position = new Vector3(x, y, 0);
                cube.transform.position = position;

                cube.GetComponent<Renderer>().material.color = Color.black;

                cub [x, y] = cube;
            }
        }
    }

    void Update()
    {
        Vector3 mposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selec(mposition);
    }
    
    public void selec(Vector3 posicion)
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
                        colorActual = jugadorUno;
                    else
                        colorActual = jugadorDos;
                    cube.GetComponent<Renderer>().material.color = colorActual;
                    turnos = !turnos;
                    verificacionHorizontal(x, y, colorActual);
                    verificacionVertical(x, y, colorActual);
                    verificacionDiagArriba(x, y, colorActual);
                    verificacionDiagAbajo(x, y, colorActual);
                    rule34(rule1, rule2);
                }
            }
        }

        void verificacionHorizontal(int i, int j, Color colorActual)
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
                            Debug.Log("horizontal");
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

        void verificacionVertical(int i, int j, Color colorActual)
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
                            Debug.Log("vertical");
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

        void verificacionDiagArriba(int i, int j, Color colorActual)
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
                            Debug.Log("diagonal arriba");
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

        void verificacionDiagAbajo(int i, int j, Color colorActual)
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
                            Debug.Log("diagonal abajo");
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

        void rule34(int rule1, int rule2)
        {
            if (rule1 == 2)
            {
                Debug.Log("win1");
            }
            else if (rule2 == 2)
            {
                Debug.Log("win2");
            }
        }
    }

}
