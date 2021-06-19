using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public float moveSpeed = 600f;
	
	private float movement = 0f;
	private Vector2 startPos;
	public int pixelDistanceToDetect = 50;
	private bool fingerDown = false;
	private float sensitivity = 20f;	
	
	public Text ScoreText;
	private float score;
	private float pointIncreasePerSeconds;
	private bool isPlaying = true;
	
	public AudioSource DeathSound;
	public AudioSource BGMusic;
 

	
	void Start()
	{
		score = 0f;                                   
		pointIncreasePerSeconds = 1f;
	}
    // Update is called once per frame
    void Update()
    {
     
		//For Testing with Mouse Swipe
	
	
		if(fingerDown == false && Input.GetMouseButtonDown(0))    //-- if the mouse button is clicked --//
		{
			startPos = Input.mousePosition;
			fingerDown = true;
			
		}
		
		if(fingerDown)
		{
			if(Input.mousePosition.x <= startPos.x - pixelDistanceToDetect)     // -- if the mouse position is moved at +x-axis--//
			{
				fingerDown = false;
				Debug.Log("Swipe left");
				movement = sensitivity;
				
			}
			else if(Input.mousePosition.x >= startPos.x + pixelDistanceToDetect) // -- if the mouse position is moved at -x-axis--//
			{
				fingerDown = false;
				Debug.Log("Swipe Right");
				movement = -sensitivity;
			}
		}
		
		
		//Touch Inputs
		
		
		if(fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)  //-- if it is 1st touch--//
		{
			startPos = Input.touches[0].position;
			fingerDown = true;

		}
		
		if(fingerDown == true)                                                                        //--touch on +x-axis --//
		{
			if(Input.touches[0].position.x <= startPos.x - pixelDistanceToDetect)
			{
				fingerDown = false;
				Debug.Log("Swipe left");
				movement = sensitivity;
			}
			
			else if(Input.touches[0].position.x >= startPos.x + pixelDistanceToDetect)               //--touch on -x-axis --//
			{
				fingerDown = false;
				Debug.Log("Swipe Right");
				movement = -sensitivity;
			}
		
		}
		
		if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)                     //--if there is no touch--//
		{
			fingerDown = false;
		}
		
		
		
      //Score Value
	
		if(isPlaying)                                  //--if the player isPlaying i.e it is not dead increase the score--//
		{
			
			ScoreText.text = "" + (int)score;
			score += pointIncreasePerSeconds * Time.deltaTime;
			
		}

		
    }
	
	private void FixedUpdate()                                                 //-- rotate the player around the center ---//
	{
		transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
	}
	
	private void OnTriggerEnter2D(Collider2D collosion)            //--if it collide with the hexagon collider restart the scene--//
	{
		BGMusic.Stop();
		DeathSound.Play();
		isPlaying = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
}
