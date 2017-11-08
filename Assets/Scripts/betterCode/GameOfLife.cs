using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{

    public enum States
    {
        Idle, Running
    }

    public Cells cellPrefab;

    public float updateInterval = 0.1f;

    [HideInInspector] public Cells[,] cells; 
    [HideInInspector] public States state = States.Idle;
    [HideInInspector] public int sizeX; 
    [HideInInspector] public int sizeY; 

    private Action cellUpdates; 
    private Action cellApplyUpdates; 

    private IEnumerator coroutine; 

    void Awake()
    {
        Init(50, 50); 

        Run(); 
    }

    public void Init(int x, int y)
    {
        if (cells != null)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    GameObject.Destroy(cells[i, j].gameObject);
                }
            }
        }

        // clear actions
        cellUpdates = null;
        cellApplyUpdates = null;

        coroutine = null;

        sizeX = x;
        sizeY = y;
        SpawnCells(sizeX, sizeY);
    }
    
    public void UpdateCells()
    {
        cellUpdates();
        cellApplyUpdates();
    }

    public void SpawnCells(int x, int y)
    {
        cells = new Cells[x, y]; 
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Cells c = Instantiate(cellPrefab, new Vector3((float)i, (float)j, 0f), Quaternion.identity) as Cells; 
                cells[i, j] = c;
                c.Init(this, i, j);
                c.SetRandomState();
                
                cellUpdates += c.CellUpdate;
                cellApplyUpdates += c.CellApplyUpdate;
            }
        }

        // get and set references to neighbours for every cell
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                cells[i, j].neighbours = GetNeighbours(i, j);
            }
        }
    }
    
    public Cells[] GetNeighbours(int x, int y)
    {
        Cells[] result = new Cells[8];
        result[0] = cells[x, (y + 1) % sizeY]; // top
        result[1] = cells[(x + 1) % sizeX, (y + 1) % sizeY]; // top right
        result[2] = cells[(x + 1) % sizeX, y % sizeY]; // right
        result[3] = cells[(x + 1) % sizeX, (sizeY + y - 1) % sizeY]; // bottom right
        result[4] = cells[x % sizeX, (sizeY + y - 1) % sizeY]; // bottom
        result[5] = cells[(sizeX + x - 1) % sizeX, (sizeY + y - 1) % sizeY]; // bottom left
        result[6] = cells[(sizeX + x - 1) % sizeX, y % sizeY]; // left
        result[7] = cells[(sizeX + x - 1) % sizeX, (y + 1) % sizeY]; // top left
        return result;
    }

    public void Run()
    {
        state = States.Running;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = RunCoroutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator RunCoroutine()
    {
        while (state == States.Running)
        {
            UpdateCells(); 
            yield return new WaitForSeconds(updateInterval);
        }
    }
}