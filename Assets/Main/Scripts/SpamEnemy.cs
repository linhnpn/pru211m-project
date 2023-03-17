using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpamEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemy_0;
    [SerializeField]
    GameObject enemy_1;
    [SerializeField]
    GameObject enemy_2;
    [SerializeField]
    GameObject enemy_3;
    [SerializeField]
    GameObject enemy_4;

    [SerializeField]
    int MaxEnemyCount;

    [SerializeField]
    float GeneralSpawnTime;

    [SerializeField]
    float PrepareTime;

    float TimeSpawnNormalEnemy;
    float TimeSpawnBoss;

    [SerializeField]
    Tilemap tilemap;

    float screenLeft, screenTop, screenRight, screenBottom;
    float TimeNormalEnemy;
    float TimeBoss;

    int HardIncreasedCount;
    public float GeneralHardBonus;
    public float GeneralSpawnTimeBonus;
    public float EnemyHpBonusAsPercent;
    public float EnemyDamageBonusAsPercent;
    public float EnemyMovemonetSpeedBonusAsPercent;

    List<GameObject> NormalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;

        HardIncreasedCount = 0;

        NormalEnemies = new List<GameObject>{
                enemy_0,
                enemy_1,
                enemy_2,
                enemy_4
        };

        TimeSpawnNormalEnemy = GeneralSpawnTime * 0.5f;
        TimeSpawnBoss = GeneralSpawnTime * 10;

        TimeNormalEnemy = -PrepareTime;
        TimeBoss = -PrepareTime;

        // GeneralSpawnTime is 3, then GeneralHardBonus is 3.33
        GeneralHardBonus = 10 / GeneralSpawnTime;
        GeneralSpawnTimeBonus = GeneralHardBonus/25;
        EnemyHpBonusAsPercent = GeneralHardBonus * 2;
        EnemyDamageBonusAsPercent = GeneralHardBonus * 2;
        EnemyMovemonetSpeedBonusAsPercent = GeneralHardBonus/2;
}

    // Update is called once per frame
    void Update()
    {
        TimeNormalEnemy += Time.deltaTime;
        TimeBoss += Time.deltaTime;

        SpawnEnemyWhenAllowed(ref TimeNormalEnemy, TimeSpawnNormalEnemy, null);
        SpawnEnemyWhenAllowed(ref TimeBoss, TimeSpawnBoss, enemy_3);


    }

    Vector3 GetRandomWorldPosition()
    {
        Vector3Int RandomPosition = new Vector3Int(
            Random.Range(tilemap.cellBounds.min.x + 1, tilemap.cellBounds.max.x - 1), 
            Random.Range(tilemap.cellBounds.min.y + 1, tilemap.cellBounds.max.y - 1),
            0);
        Vector3 RandomWorldPosition = tilemap.CellToWorld(RandomPosition) + new Vector3(0.5f, 0.5f, 0);
        return RandomWorldPosition;
    }

    void SpawnEnemyWhenAllowed(ref float time, float timeSpawn, GameObject enemyGameObject)
    {
        int CurrentEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (time >= timeSpawn && CurrentEnemiesCount < MaxEnemyCount)
        {
            GameObject enemy = Instantiate(enemyGameObject == null ? GetRandomNormalEnemy() : enemyGameObject);

            if (enemy.name.Contains("Enemy 4"))
            {
                enemy.GetComponent<EnemyBeAttacked>().MaxHealth *= 1 + HardIncreasedCount * (EnemyHpBonusAsPercent / 100);
                enemy.GetComponent<EnemyShoot>().Damage *= 1 + HardIncreasedCount * (EnemyDamageBonusAsPercent / 100);
            }
            else
            {
                enemy.GetComponent<EnemyMovement>().speed *= 1 + HardIncreasedCount * (EnemyMovemonetSpeedBonusAsPercent / 100);
                enemy.GetComponent<EnemyAttack>().damage *= 1 + HardIncreasedCount * (EnemyDamageBonusAsPercent / 100);
                enemy.GetComponent<EnemyBeAttacked>().MaxHealth *= 1 + HardIncreasedCount * (EnemyHpBonusAsPercent / 100);
            }

            enemy.transform.position = GetRandomWorldPosition();
            time = 0;
        }
    }

    GameObject GetRandomNormalEnemy()
    {
        GameObject enemy;
        int index = Random.Range(0, NormalEnemies.Count + 1);
        if(index == 4) enemy = NormalEnemies[3];
        else enemy = NormalEnemies[index];
        Debug.Log(GeneralSpawnTime + " " + HardIncreasedCount + " " + (1 + HardIncreasedCount * (EnemyHpBonusAsPercent / 100)));
        return enemy;
    }

    public void IncreaseHard()
    {
        //GeneralHardBonus = 10/3
        //GeneralSpawnTimeBonus = 10/3/25 = 2/15
        //GeneralSpawnTime = 3
        HardIncreasedCount++;

        GeneralSpawnTime = (GeneralSpawnTime / (GeneralSpawnTimeBonus + GeneralSpawnTime)) * GeneralSpawnTime;

        TimeSpawnNormalEnemy = GeneralSpawnTime * 0.5f;
    }
}
