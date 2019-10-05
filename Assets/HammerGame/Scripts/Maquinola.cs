using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Maquinola : MonoBehaviour
{
    //Hay que tener una lista de spawners a ser utilizados
    //Lista de prefabs de los enemigos
    //Llevar el score de los bichos matados

    public List<Spawner> spawners = new List<Spawner>();
    public Spawner bigSpawn;
    public List<GameObject> enemies = new List<GameObject>();
    public int timeBetweenSpawns;
    public ScoreManager scoreManager;
    public TextMesh score;
    private Animator _animator;    

    public int prob_enemyBase;
    public int prob_pincho;

    public float timeToCloseSmallDoors;
    public float timeToCloseBigDoors;

    private spawnMode _spawnMode = spawnMode.smallDoor;
    private void Start()
    {
        InitGame();
    }


    public void InitGame()
    {
        //Inicia la maquina.
        _animator = GetComponent<Animator>();
        StartCoroutine(SpawnDirector());
        RegisterSpawners();
    }

    private void RegisterSpawners()
    {
        foreach (Spawner sp in spawners)
        {
            sp.OnKilledEnemy += AddScore;
        }

        bigSpawn.OnKilledEnemy += AddScore;
    }

    private void UnregisterSpawners()
    {
        foreach (Spawner sp in spawners)
        {
            sp.OnKilledEnemy -= AddScore;
        }
        bigSpawn.OnKilledEnemy -= AddScore;
    }

    private void AddScore(Enemy.EnemyType eType)
    {
        Debug.Log(eType + " a eso le pegue");
        scoreManager.AddScore(eType);
        score.text = scoreManager.currentScore.ToString();
    }

    private IEnumerator SpawnDirector()
    {
        while(true)
        {
            switch (_spawnMode)
            {
                case spawnMode.smallDoor:
                {
                    SpawnInAllHoles();
                    break;
                }
                case spawnMode.bigDoor:
                {
                    SpawnBigDoor();
                    break;
                }
            }
            //SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnBigDoor()
    {
        

        _animator.SetTrigger("BigOpen");
        
       
    }

    public void SpawnBig()
    {
        int maxChanceAmount = prob_pincho + prob_enemyBase;
        
        int rng = Random.Range(0, maxChanceAmount);
        if (rng < prob_enemyBase)
        {
            bigSpawn.SpawnEnemy(enemies[3]);
        }
        else
        {
            bigSpawn.SpawnEnemy(enemies[2]);
        }
        
        
    }

    void SpawnInAllHoles()
    {
        int maxChanceAmount = prob_pincho + prob_enemyBase;

        _animator.SetTrigger("SmallOpen");
        
        foreach (Spawner sp in spawners)
        {
            int rng = Random.Range(0, maxChanceAmount);

            GameObject chosenEnemy;
            if (rng <= prob_enemyBase)
            {
                chosenEnemy = enemies[0];
            }
            else
            {
                chosenEnemy = enemies[1];
            }
            
            sp.SpawnEnemy(chosenEnemy);
        }
    }

    private void SpawnEnemy()
    {
        //Debe elegir uno de los tantos spawners disponibles y le ordena spawnear uno ahi
        int rgnSpawn = Random.Range(0, spawners.Count);
        int rgnEnemies = Random.Range(0, enemies.Count);

        if(!spawners[rgnSpawn].IsSpawnerOccupied())
            spawners[rgnSpawn].SpawnEnemy(enemies[rgnEnemies]);
    }

    #region Animations

    public void SmallDoorsOpened()
    {
        StartCoroutine(_CountToCloseSmallDoors());
    }

    private IEnumerator _CountToCloseSmallDoors()
    {
        yield return new WaitForSeconds(timeToCloseSmallDoors);
        
        _animator.SetTrigger("SmallClose");
    }

    public void BigDoorOpened()
    {
        StartCoroutine(_CountToCloseBigDoors());
    }
    
    private IEnumerator _CountToCloseBigDoors()
    {
        yield return new WaitForSeconds(timeToCloseBigDoors);
        
        _animator.SetTrigger("BigClose");
    }
    
    #endregion
    
    public void SmallDoorsClosed()
    {
        DecideIfBigDoorOrSmallDoor();
    }
    
    public void BigDoorClosed()
    {
        DecideIfBigDoorOrSmallDoor();
    }
    

    private void DecideIfBigDoorOrSmallDoor()
    {
        if (scoreManager.currentScore % 3 == 0 && scoreManager.currentScore	!= 0)
        {
            ChangeSpawnMode(spawnMode.bigDoor);
        }
        else
        {
            ChangeSpawnMode	(spawnMode.smallDoor);
        }
    }

    void ChangeSpawnMode(spawnMode mode)
    {
        _spawnMode = mode;
    }

    private enum spawnMode
    {
        smallDoor,
        bigDoor
    }
}
