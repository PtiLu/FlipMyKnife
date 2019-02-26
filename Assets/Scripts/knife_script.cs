using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class knife_script : MonoBehaviour {

	public Rigidbody rb;
	private Vector2 startSwipe, endSwipe;
    public float force = 5f;
	public float torque = 25f;
	private float score;
	private float timeWhenWeStartedFlying;
	public Text scoreText;
	private int started;

	// Use this for initialization
	void Start () {
		score = 0f;
		started = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		}
		if(Input.GetMouseButtonUp(0)){
			endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Swipe();
		}
	}

	void Swipe (){

		rb.isKinematic = false;

		timeWhenWeStartedFlying = Time.time;
		
		Vector2 swipe = endSwipe - startSwipe;
		
		rb.AddForce(swipe * force, ForceMode.Impulse);

		//Define the way to tork
		if( swipe.x > .0f){
			rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);	
		}
		else{
			rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
		}

		float timeInAir= Time.time - timeWhenWeStartedFlying;
		if (!rb.isKinematic && timeInAir >= .01f){
			Debug.Log (score);
		}
		
		
	}
	void OnTriggerEnter(Collider col) {

		if(col.tag == "Wood"){
			rb.isKinematic = true;
			if(started == 0){
				score += 20f;
				scoreText.text = score.ToString();
			}
			else{
				started = 0;
			}
			
		}
		else{
			Restart();
			started = 1;
		}

	}

	void OnCollisionEnter() {

		float timeInAir= Time.time - timeWhenWeStartedFlying;
		
		if (!rb.isKinematic && timeInAir >= .01f){

			Debug.Log("Fail");
			Restart();
		}
		
		
	}


	void Restart(){

		score = 0f;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	
	}
}
   