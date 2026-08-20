using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance = null;

	public GameObject[] enemyPrefab;

	List<Vector3> enemyPositions = new List<Vector3>();


	public float enemySpawnValueX = -54;
	public float enemySpawnValueZ = 90;

	public float startWait = 2; //time before first wave
	public float spawnWait = 20; //time between creating waves of enemies  --  Is there a way to make every other drop of the enemies spawn a new row?
	public float moveWait = 1;
	public int moveStep;

	GameObject enemyContainer = null;

	public enum MoveChoice { Right, Left, Down }
	public MoveChoice moveTowards = MoveChoice.Right;
	public bool goRight = true;
	public bool moving = false;

	//the distance enemies move
	public Vector3 spaceToMove = new Vector3(5, 0, 0);
	public Vector3 dropAmount = new Vector3 (0, 0, -7f);

	
	void Awake()
	{
        enemyContainer = new GameObject("EnemyContainer");
        PopulateList ();
	}

	void Start()
	{
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //StartCoroutine (CreateEnemyLine ());
	}

	void Update () 
	{
        if (moving == false)
            Movement();

        CreateEnemies();

    }

	void Movement()
	{
        moving = true;

        switch (moveTowards)
        {
            case MoveChoice.Right:
                MoveEnemy(spaceToMove);
                break;

            case MoveChoice.Left:
                MoveEnemy(-spaceToMove);
                break;

            case MoveChoice.Down:
                MoveDown ();
                //yield return new WaitForSeconds (moveWait);
                break;

            default:
                Debug.Log("How'd this happen?");
                break;
        }
    }
	IEnumerator CreateEnemyLine()
	{
		//wait to create the first lines
		enemyContainer = new GameObject("EnemyContainer");
        yield return new WaitForSeconds(startWait);
        
		while(true)
		{
			//enemyContainer.transform.parent = this.transform;
			//enemyContainer.transform.position = enemySpawn.position;

			for (int i = 0; i < 10; i++) 
			{
				GameObject enemyShip = Instantiate(enemyPrefab[ Random.Range(0, enemyPrefab.Length) ], enemyPositions[i], Quaternion.identity) as GameObject;
				enemyShip.transform.parent = enemyContainer.transform;
			}
			yield return new WaitForSeconds(spawnWait);
		}
	}

	void PopulateList()
	{
		for (int i = 0; i < 10; i++) 
		{
			enemyPositions.Add (new Vector3 (enemySpawnValueX, 0, enemySpawnValueZ));
			enemySpawnValueX += 12;
		}
		//enemySpawnValueX = -54;
	}

    void MoveEnemy(Vector3 move)
    {
        StartCoroutine(Wait(moveWait));
        Rigidbody[] enemies = enemyContainer.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in enemies)
        {
            child.transform.position += move;

            if (child.transform.position.x >= 78)
            {
                moveTowards = MoveChoice.Down;
            }
            if (child.transform.position.x <= -78)
            {
                moveTowards = MoveChoice.Down;
            }
        }

        moveStep++;
    }

    void MoveDown()
    {
        StartCoroutine(Wait(moveWait));
        Rigidbody[] enemies = enemyContainer.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in enemies)
        {
            child.transform.position += dropAmount;
        }
        if (goRight)
        {
            goRight = false;
            moveTowards = MoveChoice.Left;
        }
        else
        {
            goRight = true;
            moveTowards = MoveChoice.Right;
        }
        moveStep++;
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moving = false;

    }

    void CreateEnemies()
    {
        if (moveStep >= 22)
        {
            //spawn an enemy from the list at each of the locations from PopulateEnemyPositionList
            for (var i = 0; i < 10; i++)
            {
                GameObject enemyShip = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], enemyPositions[i], Quaternion.identity) as GameObject;
                enemyShip.transform.parent = enemyContainer.transform;

            }

            moveStep = 0;
        }
    }
}
