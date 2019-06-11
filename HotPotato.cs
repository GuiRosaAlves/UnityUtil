using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotato : GameMode
{
    [SerializeField] private GameObject _potatoPrefab;
    [SerializeField] private Vector2 _minSpawnRange;
    [SerializeField] private Vector2 _maxSpawnRange;

    private GameObject _potato;
    
    public override void GameStarted()
    {
        SpawnPotato();
    }

    public override bool GameOverCondition()
    {
        return false;
    }

    public override void GameOver()
    {
        
    }

    public override void GamePaused()
    {
        
    }

    public void SpawnPotato()
    {
        if (_potato == null)
            _potato = Instantiate(_potatoPrefab);
        else
        {
            Destroy(_potato);
            _potato = Instantiate(_potatoPrefab);
            _potato.transform.position = new Vector3(Random.Range(_minSpawnRange.x, _maxSpawnRange.x), 0,
                Random.Range(_minSpawnRange.y, _maxSpawnRange.y));
        }
    }
}