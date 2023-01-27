using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextbotRandomSpawn : PlayerRandomSpawn
{
    [SerializeField] CharacterController _target;
    [SerializeField] int _minRandDelay = 3;
    [SerializeField] int _maxRandDelay = 7;
    private NextbotManager nextbotManager;

    void Start()
    {
        nextbotManager = GetComponent<NextbotManager>();
        RandomSpawn(_minRandDelay, _maxRandDelay);
    }

    public void RandomSpawn(int minRandDelay, int maxRandDelay)
    {
        StopAllCoroutines();
        StartCoroutine(spawnAfterDelay(minRandDelay, maxRandDelay));
    }

    private IEnumerator spawnAfterDelay(int minRandDelay, int maxRandDelay)
    {
        float time = Random.Range(minRandDelay, maxRandDelay);
        yield return new WaitForSeconds(time);

        Vector3 randomPos = getRandomPos();
        while (Vector3.Distance(_target.transform.position, randomPos) < 60)
        {
            randomPos = getRandomPos();
        }
        GameObject nextbot = nextbotManager.SpawnNextbot(randomPos);
        nextbot.transform.position = new Vector3(randomPos.x, randomPos.y+5, randomPos.z);
    }
}
