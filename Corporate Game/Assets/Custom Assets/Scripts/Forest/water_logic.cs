using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchControlsKit;

public class water_logic : MonoBehaviour {

	public GameObject water_bottle_image;

	void OnMouseOver(){
		
		if (Input.GetKeyDown (KeyCode.E) || TCKInput.GetButtonDown("use"))
			water_bottle_image.GetComponent <Image> ().enabled = true;


			
	}


}
