using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject tanker;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject soldier;
    [SerializeField] Text levelText;

    private bool gameOver = false;
    private int currentLevel;
    private float generatedSpawnTime = 1;
    private float currentSpawnTime = 0;
    private GameObject newEnemy;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }

    public bool GameOver
    {
        get {return gameOver; }
    }

    public GameObject Player
    {
        get {return player; }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        StartCoroutine(spawn());
        currentLevel = 1;
    }

    
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
    }

    public void PlayerHit(int currentHP)
    {
        if(currentHP > 0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }

    IEnumerator spawn()
    {
        //check the spawn time is greater than the current
        //if there are less enemies on screen than the current level, randomly select a spawn point and spawn a random enemy
        
        if(currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
            if(enemies.Count < currentLevel)
            {
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];
                int randomEnemy = Random.Range(0, 3);
                if(randomEnemy == 0)
                {
                    newEnemy = Instantiate(soldier) as GameObject;
                } else if(randomEnemy == 1)
                {
                    newEnemy = Instantiate(ranger) as GameObject; 
                } else if(randomEnemy == 2)
                {
                    newEnemy = Instantiate(tanker) as GameObject;
                }

                newEnemy.transform.position = spawnLocation.transform.position;
            }

            //if we have killed the same number of enemies as the current level, clear out the enemies and
            //killed enemies arrays, increment the current level by I, and start over

            if(killedEnemies.Count == currentLevel)
            {
                enemies.Clear();
                killedEnemies.Clear();

                yield return new WaitForSeconds(3f);
                currentLevel++;
                levelText.text = "Level" + currentLevel;
            }
        }

        yield return null;
        StartCoroutine(spawn());
    }
}
