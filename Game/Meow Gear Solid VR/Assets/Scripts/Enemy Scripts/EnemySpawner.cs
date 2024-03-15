using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _spawnRadius;

    [SerializeField]
    private bool _isOn;

    [SerializeField]
    private int _maxSpawnNum;

    private bool canSpawn = false;
    private bool alreadySpawned = false;

    private void Start()
    {
        if (_isOn)
        {
            StartCoroutine(Spawner());
        }
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(_player.transform.position, transform.position)) < _spawnRadius && !alreadySpawned)
        {
            canSpawn = true;
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        Vector3 spawnPoint = new Vector3(transform.position.x + Random.Range(-4.0f, 4.0f), transform.position.y, transform.position.z);
        int counter = 0;
        while (true && counter < _maxSpawnNum)
        {
            yield return wait;
            if (canSpawn && counter < _maxSpawnNum)
            {
                Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
                counter++;
                spawnPoint.y = spawnPoint.y + Random.Range(-4.0f, 4.0f);
            }

        }

    }


}
