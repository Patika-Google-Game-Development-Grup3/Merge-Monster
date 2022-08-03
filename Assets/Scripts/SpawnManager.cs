using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject ArcherPrefab;
    public GameObject MeleePrefab;

    public Button ArcherButton;
    public Button MeleeButton;

    private Vector3 offset = new Vector3(0, 0.5f, 0);

    [SerializeField] GridSystem gridSystem;
    public int spawnPosCol;
    public int spawnPosRow;
    public Vector3 spawnPos;


    private void Start()
    {

        gridSystem = FindObjectOfType<GridSystem>();
        ArcherButton.onClick.AddListener(() => { SpawnArcher(); });
        MeleeButton.onClick.AddListener(() => { SpawnMelee(); });
    }


    public void SpawnArcher()
    {

        spawnPosCol = Random.Range(0, gridSystem.cols);
        spawnPosRow = Random.Range(0, gridSystem.rows);
        spawnPos = gridSystem.gridArray[spawnPosCol, spawnPosRow].transform.position;



        if (gridSystem.gridArray[spawnPosCol, spawnPosRow].GetComponent<CollisionDetection>().IsPointEmpty)
        {
            Debug.Log("Collision Dedected");
        }
        else
        {
            Instantiate(ArcherPrefab, gridSystem.gridArray[spawnPosCol, spawnPosRow].transform.position + offset, Quaternion.identity);
        }

    }

    public void SpawnMelee()
    {

        spawnPosCol = Random.Range(0, gridSystem.cols);
        spawnPosRow = Random.Range(0, gridSystem.rows);
        spawnPos = gridSystem.gridArray[spawnPosCol, spawnPosRow].transform.position;



        if (gridSystem.gridArray[spawnPosCol, spawnPosRow].GetComponent<CollisionDetection>().IsPointEmpty)
        {
            Debug.Log("Collision Dedected");
        }
        else
        {
            Instantiate(MeleePrefab, gridSystem.gridArray[spawnPosCol, spawnPosRow].transform.position + offset, Quaternion.identity);
        }
    }


}
