using UnityEngine;

public class GridSystem : MonoBehaviour
{
   
    public int rows, cols;
    public float x_Space,z_Space;
    public GameObject gridPrefab;
    public GameObject GridParentObject;
    public Vector3 leftBottomLocation = Vector3.zero;
    public GameObject[,] gridArray;

    void Awake()
    {
        gridArray = new GameObject[cols, rows];

        InstantiateGrid();

    }

    void Update()
    {
        
    }

    void InstantiateGrid()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(x_Space* (leftBottomLocation.x + i) , 0, z_Space* (leftBottomLocation.z + j)), Quaternion.identity);
                obj.transform.SetParent(GridParentObject.transform);

                gridArray[i, j] = obj;
            }
        }
    }

  
}
