using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    GameObject[,] boardArray = new GameObject[20, 20];

    public GameObject gridSpace;

    [SerializeField]
    GameObject cam;

    // Use this for initialization
    void Start () {
        GenerateGrid();
        
	}

    // Update is called once per frame
    void Update()
    {
        Ray detectobject = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(detectobject, out hit, 100))
        {
            Debug.DrawLine(detectobject.origin, hit.point);
        }
    }

    void GenerateGrid()

    {

        for (int i = 0; i < boardArray.GetLength(0); i++)
        {
            for (int j = 0; j < boardArray.GetLength(1); j++)
            {

                boardArray[i, j] = Instantiate(gridSpace, new Vector3(j * 2, i * 2, 0), Quaternion.identity) as GameObject;
            }
        }
    }

}
