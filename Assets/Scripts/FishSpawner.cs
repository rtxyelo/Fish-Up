using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _fishPrefabs = new();

    [SerializeField]
    private List<Transform> _spawnPositions = new();

    private bool SpawnAllow = true;

    private readonly float _spawnTime = 2.0f;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        _gameController.IsGameOver.AddListener(GameOver);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (SpawnAllow)
        {
            int spawnIndex = Random.Range(0, _spawnPositions.Count);
            int fishIndex = Random.Range(0, _fishPrefabs.Count);

            GameObject ball = Instantiate(_fishPrefabs[fishIndex], _spawnPositions[spawnIndex].transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private void GameOver()
    {
        SpawnAllow = false;

        gameObject.SetActive(false);
    }
}
