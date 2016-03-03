using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class transition : MonoBehaviour {


	public int transition_number;

    bool has_entered = false;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (has_entered)
            {
                transition_manager.instance.transition(transition_number, gameObject);
                //print("");
                has_entered = false;

            }
           
        }

        //Debug.Log("has_entered   " + has_entered);
    }


	void OnTriggerEnter(Collider player){
		if (player.transform.CompareTag("player"))
        {
            has_entered = true;
        }
	}

    void OnTriggerExit(Collider player)
    {
        if (player.transform.CompareTag("player"))
        {
            has_entered = false;
        }
    }
}
