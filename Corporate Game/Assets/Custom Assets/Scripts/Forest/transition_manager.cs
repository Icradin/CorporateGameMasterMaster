using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class transition_manager : MonoBehaviour {


	public static transition_manager instance = null;

	public GameObject player;
	public GameObject [] transition_array;
	public GameObject destination_transition;
	public Graphic fade_image;
	public float fade_time;


	void Awake (){
		if (instance == null)
			instance = this;

		else if (instance != null)
			Destroy (gameObject);

		if (player == null)
			player = GameObject.FindGameObjectWithTag ("player");

		if (transition_array.Length == 0)
			transition_array = GameObject.FindGameObjectsWithTag ("transition");

		//fade (false);
	}


	public void transition(int passed_transition_number, GameObject passed_transition_object){
		
		for (int i = 0; i < transition_array.Length; i++) {
			
			if (transition_array [i].GetComponent<transition> ().transition_number == passed_transition_number && transition_array [i] != passed_transition_object) {
				destination_transition = transition_array [i].gameObject;
			}
		}

		StartCoroutine (transition_fade ());

	}

	IEnumerator transition_fade(){
		fade (true);

		yield return new WaitForSeconds (fade_time);

		fade (false);

		player.transform.position = destination_transition.transform.position;
	}


	public void fade(bool fade){
		if (fade == true)
			fade_image.CrossFadeAlpha (1, fade_time, false);

		else if (fade == false)
			fade_image.CrossFadeAlpha (0, fade_time, false);
			
	}


}
