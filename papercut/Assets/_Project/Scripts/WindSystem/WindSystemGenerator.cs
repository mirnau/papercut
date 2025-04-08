using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using static WindBlock;

public class WindSystemGenerator : MonoBehaviour
{
    private enum SystemType { Random, S_Current}
    [SerializeField] private SystemType type;
    [SerializeField] private Vector2Int systemSize;
    [SerializeField] private GameObject WindBlock;
    private WindBlock[,] windBlockGrid;
    [Tooltip("Will be 8f by default if unchanged")]
    [SerializeField] private float maxSystemWindForce;
    [Tooltip("How much the wind force can vary from block to block")]
    [SerializeField] private float systemWindVariation;

    public void Start()
    {
        windBlockGrid = new WindBlock[systemSize.x, systemSize.y];
        
        switch (type)
        {
            case SystemType.Random:
                RandomGeneration();
                break;
            case SystemType.S_Current:
                S_CurrentGeneration();
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
                if(maxSystemWindForce!=0) windBlock.maxWindForce = maxSystemWindForce;

                windBlock.direction = (WindDirection)Random.Range(0,4);
                windBlock.windForce = Random.Range(0, windBlock.maxWindForce);
                
            }
        }
    }
    public void S_CurrentGeneration()
    {
        for (int i = 0; i < systemSize.y; i++)
        {
            for (int j = 0; j < systemSize.x; j++)
            {
                GameObject newBlock = Instantiate(WindBlock, transform.position + new Vector3(j * WindBlock.transform.localScale.x + WindBlock.transform.localScale.x / 2, i * WindBlock.transform.localScale.y + WindBlock.transform.localScale.y / 2, 0), Quaternion.identity);
                newBlock.transform.parent = transform;
                WindBlock windBlock = newBlock.GetComponent<WindBlock>();
                if (maxSystemWindForce != 0)
                {
                    windBlock.maxWindForce = maxSystemWindForce;
                }
                windBlockGrid[j,i] = windBlock;

                windBlockGrid[j, i].VariateWindForce(systemWindVariation);

            }
        }
        windBlockGrid[0,0].WindForce = Random.Range(0, windBlockGrid[0, 0].maxWindForce/3);
        Debug.Log("Hi");
        for (int row = 0; row <= systemSize.y - 1; row++)  // Start from the bottom row
        {
            if ( row % 2 == 0)  // Odd rows (0, 2, 4...) go left to right
            {
                for (int col = 0; col < systemSize.x; col++)
                {
                    if(col == systemSize.x - 1)
                    {
                        Debug.Log("Up");
                        windBlockGrid[col, row].direction=WindDirection.U;
                        windBlockGrid[col, row].windForce *= 2;
                    }
                    else windBlockGrid[col, row].direction = WindDirection.R;

                }
            }
            else  // Even rows (1, 3, 5...) go right to left
            {
                for (int col = systemSize.x - 1; col >= 0; col--)
                {
                    if (col == 0)
                    {
                        windBlockGrid[col, row].direction = WindDirection.U;
                        windBlockGrid[col, row].windForce *= 2;
                    }
                    else windBlockGrid[col, row].direction = WindDirection.L;

                }
            }
        }
    }
}
