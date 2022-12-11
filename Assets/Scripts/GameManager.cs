using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _enemysPositions;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _timeBetweenEnemies;
    [SerializeField]
    private TextMeshProUGUI _pointsText;

    private float _enemiesTimer;
    private int _points;

    void Start()
    {
        
    }

    void Update()
    {
        if (_enemiesTimer <= 0)
        {
            _enemiesTimer = _timeBetweenEnemies;
            CreateEnemy();
        }
        else
        {
            _enemiesTimer -= Time.deltaTime;
        }
    }

    private void CreateEnemy()
    {
        int enemyPosition = Random.Range(0, _enemysPositions.Length);

        Instantiate(_enemyPrefab, _enemysPositions[enemyPosition].position, _enemysPositions[enemyPosition].rotation);
    }

    public void EnemyDestroyed()
    {
        _points++;
        _pointsText.text = _points.ToString();
    }
}
