using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonScript : MonoBehaviour, IPooledObject
{
	
	public Rigidbody2D rb;
	public float shrinkSpeed = 3f;
	private ObjectPool objectPool;
	
    // Start is called before the first frame update
    public void OnObjectSpawn()                          //--Every hexagon object should have the same rotation and position--//
    {
		objectPool = FindObjectOfType<ObjectPool>();
        rb.rotation = Random.Range(0f, 360f);                 //---Rotate the hexagon randomly--//                
		transform.localScale = Vector3.one * 10f;             
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;    //---Shrink the hexagon by reducing the scale--// 
			
	    if (transform.localScale.x <= 1f)                                     //---if its scales less than 1 it is inactive--//
		{
	     	this.gameObject.SetActive(false);
		}
		
    }
	
	
}
