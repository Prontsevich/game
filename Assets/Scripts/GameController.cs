using UnityEngine;

public class GameController : MonoBehaviour
{
    private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        
    }
}
