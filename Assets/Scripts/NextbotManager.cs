using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextbotManager : MonoBehaviour
{
    [SerializeField] GameObject _nextbotPrefab;
    private GameObject _nextbot;
    private bool _isNextbotSpawned;

    public GameObject SpawnNextbot(Vector3 position)
    {
        if (_isNextbotSpawned)
        {
            throw new System.Exception("Nextbot is already spawned");
        }
        _nextbot = Instantiate(_nextbotPrefab, position, Quaternion.identity);
        _isNextbotSpawned = true;
        return _nextbot;
    }

    public void DeleteNextbot()
    {
        if (_isNextbotSpawned)
        {
            Destroy(_nextbot);
            _isNextbotSpawned = false;
        }
    }

    public void DisappearForTime(int minRandTime, int maxRandTime)
    {
        DeleteNextbot();
        GetComponent<NextbotRandomSpawn>().RandomSpawn(minRandTime, maxRandTime);
    }
}
