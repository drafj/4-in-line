using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject puzzlePiece;
    private GameObject[,] grid; 

    void Start()
    {
        grid = new GameObject[width,height];
        for (int x = 0;  x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;
                Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;
                grid [x, y] = go;
            }
        }
    }

    void Update()
    {
        Vector3 mposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(Vector3.zero, mposition);
        int x = (int) (mposition.x + 0.5f);
        int y = (int) (mposition.x + 0.5f);
        if (x >= 0 && y >= 0 && x < width && y > height)
        {
        GameObject go = grid[x,y];
        go.GetComponent<Renderer>().material.SetColor("_color", Color.red);
        }
    }
}
