using UnityEngine;
using static WindBlock;

public class WindSystemGenerator : MonoBehaviour
{
    private enum SystemType { Random, Current}
    [SerializeField] private SystemType type;
    [SerializeField] private Vector2Int systemSize;
    [SerializeField] private GameObject WindBlock;
    private WindBlock[,] windBlockGrid;

    public void Start()
    {
        windBlockGrid = new WindBlock[systemSize.x, systemSize.y];
        switch (type)
        {
            case SystemType.Random:
                RandomGeneration();
                break;
            case SystemType.Current:
                CurrentGeneration();
                break;
        }
    }

    public void RandomGeneration()
    {
        for (int i = 0; i < systemSize.y; i++)
        {
            for (int j = 0; j < systemSize.x; j++)
            {
                GameObject newBlock = Instantiate(WindBlock, transform.position + new Vector3(j * WindBlock.transform.localScale.x+ WindBlock.transform.localScale.x/2, i * WindBlock.transform.localScale.y+ WindBlock.transform.localScale.y/2, 0), Quaternion.identity);
                newBlock.transform.parent = transform;
                WindBlock windBlock= newBlock.GetComponent<WindBlock>();
                windBlock.direction = (WindDirection)Random.Range(0,4);
                windBlock.windForce = Random.Range(0,4);
            }
        }
    }
    public void CurrentGeneration()
    {
        for (int i = 0; i < systemSize.y; i++)
        {
            for (int j = 0; j < systemSize.x; j++)
            {
                GameObject newBlock = Instantiate(WindBlock, transform.position + new Vector3(j * WindBlock.transform.localScale.x + WindBlock.transform.localScale.x / 2, i * WindBlock.transform.localScale.y + WindBlock.transform.localScale.y / 2, 0), Quaternion.identity);
                newBlock.transform.parent = transform;
                WindBlock windBlock = newBlock.GetComponent<WindBlock>();
                windBlockGrid[j,i] = windBlock;
            }
        }
        for (int row = 0; row <= systemSize.y - 1; row++)  // Start from the bottom row
        {
            if ( row % 2 == 0)  // Odd rows (0, 2, 4...) go left to right
            {
                for (int col = 0; col < systemSize.x; col++)
                {
                    Debug.Log($"({row}, {col})");
                }
            }
            else  // Even rows (1, 3, 5...) go right to left
            {
                for (int col = systemSize.x - 1; col >= 0; col--)
                {
                    Debug.Log($"({row}, {col})");
                }
            }
        }
    }
}
