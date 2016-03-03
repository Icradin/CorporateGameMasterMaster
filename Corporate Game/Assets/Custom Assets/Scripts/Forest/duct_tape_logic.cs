using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class duct_tape_logic : MonoBehaviour {

	public GameObject duct_tape_text;
	public GameObject tape_image;
	public GameObject duct_tape;




    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 4)
        {
            if (!game_manager.Instance.gotDuctTape)
            {
                duct_tape_text.GetComponent<Text>().enabled = true;

            }
        }
    }

	void OnMouseOver(){
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 4)
        {
            if (Input.GetKeyDown(KeyCode.E) && !game_manager.Instance.gotDuctTape)
            {
               
                tape_image.gameObject.SetActive(true);
                Destroy(gameObject);
                duct_tape_text.GetComponent<Text>().enabled = false;
                game_manager.Instance.gotDuctTape = true;
            }
        }
        else
        {
            duct_tape_text.GetComponent<Text>().enabled = false;
        }
			
	}

	void OnMouseExit (){
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 4)
        {
            duct_tape_text.GetComponent<Text>().enabled = false;
        }
	}


}
