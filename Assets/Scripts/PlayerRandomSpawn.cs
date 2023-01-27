using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomSpawn : MonoBehaviour
{
    [SerializeField] protected float _minX = 181;
    [SerializeField] protected float _maxX = 858;
    [SerializeField] protected float _minZ = 159;
    [SerializeField] protected float _maxZ = 775;
    [SerializeField] protected float _maximumY = 10;

    private float _bodySize = 2.9f;//fucking crutch

    [SerializeField] protected LayerMask _groundMask;

    void Start()
    {
        RandomSpawn();
    }

    public void RandomSpawn()
    {
        Vector3 randPos = getRandomPos();
        gameObject.transform.position = randPos;
        gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    protected Vector3 getRandomPos()
    {
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        float y = getY(x, z);
        return new Vector3(x, y + _bodySize/2, z); // using crutch not to fall under the ground
    }

    protected float getY(float x, float z)
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(x, _maximumY, z), Vector3.down, out hit, 30, _groundMask);
        return hit.point.y;
    }
}
