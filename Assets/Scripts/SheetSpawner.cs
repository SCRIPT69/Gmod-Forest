using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetSpawner : MonoBehaviour
{
    [SerializeField] int _sheetsNumber = 15;
    [SerializeField] float _spawnersRadius = 0.2f;
    [SerializeField] GameObject[] _sheetPrefabs;

    void Start()
    {
        SpawnSheets(_sheetsNumber);
    }

    public void SpawnSheets(int sheetsNumber)
    {
        List<Transform> childSpawnersList = new List<Transform>();
        int index = 0;
        foreach (Transform childSpawner in GetComponentsInChildren<Transform>())
        {
            if (index != 0)
            {
                childSpawnersList.Add(childSpawner);
            }
            else
            {
                index++;
            }
        }

        int spawnersNumber = childSpawnersList.Count;
        if (spawnersNumber < sheetsNumber)
        {
            throw new System.Exception($"Not enough spawners for spawning {sheetsNumber} sheets");
        }

        Transform[] randomChosenSpawners = new Transform[sheetsNumber];
        for (int i = 0; i < sheetsNumber; i++)
        {
            int randomSpawnerNumber = Random.Range(0, spawnersNumber - 1 - i);
            randomChosenSpawners[i] = childSpawnersList[randomSpawnerNumber];
            childSpawnersList.RemoveAt(randomSpawnerNumber);
        }

        foreach (Transform spawner in randomChosenSpawners)
        {
            int randomSheetPrefab = Random.Range(0, _sheetPrefabs.Length);
            GameObject sheet = Instantiate(_sheetPrefabs[randomSheetPrefab], spawner);
            spawner.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            sheet.transform.localPosition = new Vector3(0, 0, _spawnersRadius);
        }
    }
}
