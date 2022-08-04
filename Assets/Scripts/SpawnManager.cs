using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject ArcherPrefab;
    public GameObject MeleePrefab;

    public Button ArcherButton;
    public Button MeleeButton;

    [SerializeField] GridSystem _gridSystem;
    private int _spawnPosCol;
    private int _spawnPosRow;
    private Vector3 _spawnPos;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Start()
    {

        _gridSystem = FindObjectOfType<GridSystem>();
        ArcherButton.onClick.AddListener(() => { SpawnArcher(); });
        MeleeButton.onClick.AddListener(() => { SpawnMelee(); });
    }


    public void SpawnArcher()
    {
        GetRandomSpawnPosition();

        foreach (var gridArray in _gridSystem.gridArray)
        {
            if (_gridSystem.gridArray[_spawnPosCol, _spawnPosRow].GetComponent<CollisionDetection>().IsPointEmpty)
            {
                _spawnPos = _gridSystem.gridArray[_spawnPosCol, _spawnPosRow].transform.position + offset;
            }
            else
            {
                Instantiate(ArcherPrefab, _gridSystem.gridArray[_spawnPosCol, _spawnPosRow].transform.position + offset, Quaternion.identity);
                break;
            }
        }

    }

    public void SpawnMelee()
    {

        foreach (var gridArray in _gridSystem.gridArray)
        {
            if (_gridSystem.gridArray[_spawnPosCol, _spawnPosRow].GetComponent<CollisionDetection>().IsPointEmpty)
            {
                _spawnPos = _gridSystem.gridArray[_spawnPosCol, _spawnPosRow].transform.position + offset;
            }
            else
            {
                Instantiate(MeleePrefab, _gridSystem.gridArray[_spawnPosCol, _spawnPosRow].transform.position + offset, Quaternion.identity);
                break;
            }
        }
    }

    void GetRandomSpawnPosition()
    {
        _spawnPosCol = Random.Range(0, _gridSystem.cols);
        _spawnPosRow = Random.Range(0, _gridSystem.rows);
        _spawnPos = _gridSystem.gridArray[_spawnPosCol, _spawnPosRow].transform.position;
    }

}
