using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class conversation_base : MonoBehaviour {


    public GameObject ui_text;



	// Use this for initialization
	public virtual void Start () {
 

        turn_off();


    }
	


    public void turn_off()
    {
       
         
               ui_text.SetActive(false);
    
       
    }

    public void turn_on()
    {
    
            ui_text.SetActive(true);
        
    }

    public virtual void talk()
    {

    }
}
