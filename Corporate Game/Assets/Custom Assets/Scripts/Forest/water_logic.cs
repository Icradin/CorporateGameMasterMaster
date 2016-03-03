using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class water_logic : MonoBehaviour {

	public GameObject water_bottle_image;

	void OnMouseOver(){
		
		if (Input.GetKeyDown (KeyCode.E))
			water_bottle_image.GetComponent <Image> ().enabled = true;


			
	}


}
