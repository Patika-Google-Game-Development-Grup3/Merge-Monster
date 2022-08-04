using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    public int rows, cols;
    public float x_Space,z_Space;
    public GameObject gridPrefab;
    public GameObject characterPrafab;
    public GameObject GridParentObject;
    public Vector3 leftBottomLocation = Vector3.zero;
    public GameObject[,] gridArray;

    public int testRow;
    public int testCol;


    void Awake()
    {
        gridArray = new GameObject[cols, rows];

        InstantiateGrid();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestLocationGrid(testCol, testRow);
            Instantiate(characterPrafab, gridArray[5, 3].transform.position, Quaternion.identity);
        }
    }

    void InstantiateGrid()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(x_Space* (leftBottomLocation.x + i) , 0, z_Space* (leftBottomLocation.z + j)), Quaternion.identity);
                obj.transform.SetParent(GridParentObject.transform);

                x = i;
                y = j;
                gridArray[i, j] = obj;
            }
        }
    }

    void TestLocationGrid(int Col, int Row)
    {
        Debug.Log(gridArray[Col, Row].transform.position);
    }
}
