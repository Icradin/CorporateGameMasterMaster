using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchControlsKit;

public class duct_tape_logic : conversation_base {

	public GameObject duct_tape_text;
	public GameObject tape_image;
	public GameObject duct_tape;

    //GameObject already_got_oil_text;


    public override void talk()
    {
        if (!game_manager.Instance.gotDuctTape)
        {
            Debug.Log("got duct tape");
            game_manager.Instance.score_system_analysis++;
            tape_image.gameObject.SetActive(true);
            turn_off();
            Destroy(gameObject);
          //  duct_tape_text.GetComponent<Text>().enabled = false;
            game_manager.Instance.gotDuctTape = true;
            game_manager.Instance.talked = false;
       
        }
        //else
        //{
        //    Debug.Log("already got oil");
        //    StartCoroutine("already_got_oil");
        //}
    }

    //IEnumerator already_got_oil()
    //{
    //    already_got_oil_text.SetActive(true);

    //    yield return new WaitForSeconds(2);
    //    game_manager.Instance.talked = true;
    //    Color textColor = already_got_oil_text.GetComponent<Text>().color;
    //    while (textColor.a > 0.1f)
    //    {
    //        textColor.a -= 0.05f;
    //        already_got_oil_text.GetComponent<Text>().color = textColor;
    //    }
    //    already_got_oil_text.SetActive(false);
    //    textColor.a = 1;
    //    already_got_oil_text.GetComponent<Text>().color = textColor;

    //}


}
