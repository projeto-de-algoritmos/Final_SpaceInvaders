using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public void StartSpawnRoutines() 
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine() 
    {
        while(_gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.0f, 7.0f), 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0,3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7.0f, 7.0f), 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }
}
