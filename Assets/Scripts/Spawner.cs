using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
	
	public float spawnRate = 1f;
	public float nextTimeToSpawn = 0f;
	private ObjectPool objectPool;
	
	void Start()
	{
		objectPool = ObjectPool.Instance;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextTimeToSpawn)      //--Spawn the hexagon gameObject from ObjectPool after every nextTimeToSpawn sec --//
		{
			objectPool.SpawnFromPool("Hexagon", Vector3.zero, Quaternion.identity);
			nextTimeToSpawn = Time.time + 1f  / spawnRate;
			
		}
    }
}
