using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

	[System.Serializable]                      //--class of Pool that contains properties of object which is pooled--//
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	
	public static ObjectPool Instance;    
	
	private void Awake()                      //---making an instance of this class to axis it from anywhere--//
	{
		Instance = this;
	}
	
   public List<Pool> pools;
	
   private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
	
	void Start()
	{
		foreach(Pool pool in pools)                            //-- Check in every object Pool in pools list--//
		{
			Queue<GameObject> poolQueue = new Queue<GameObject>();
			
			for(int i= 0; i <= pool.size; i++)                //--Instantiate the prefab in Pool until the Pool size--//
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				poolQueue.Enqueue(obj);                      //--Make the queue of the objects of pool size--//
			}
			
			objectPool.Add(pool.tag, poolQueue);            //--Add the queue to the objectPool dictionary with pool tag--//
		}
	}
	
	
	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)  
	{
		if(!objectPool.ContainsKey(tag))                  //--if object with the tag does'nt contain in objectPool return null--//
		{
			Debug.Log("Pool with tag " + tag + " doesn't exist");
			return null;
		}
		
		GameObject objectToSpawn = objectPool[tag].Dequeue();   //--Spawn the object from objectPool i.e queue of objects--//
		objectToSpawn.SetActive(true);                          
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;
		
		IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>(); 
		
		if(pooledObj != null)                 //--Get the interface to have same ratation and position to all objects--//
		{
			pooledObj.OnObjectSpawn();
		}
		
		objectPool[tag].Enqueue(objectToSpawn);   //--Return the object to objectPool--//
		
		return objectToSpawn;
		
	}
	
  
}
