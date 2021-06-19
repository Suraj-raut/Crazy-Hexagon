using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	
	public AudioSource Click;
	
   	public void ToGame()
	{
		Click.Play();
		SceneManager.LoadScene("Game");
	}
}
