using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchControlsKit;

public class fuel_can_logic : conversation_base
{
    public AudioClip exit_talk;
    public AudioSource audio_source;
   // public GameObject fuel_can_text;
   // public GameObject fuel_can_text_duct_tape;
    public GameObject fuel_can_image;
    public GameObject oil_barrel;
    public GameObject oil_level;
    public GameObject barrel_choice;
    public GameObject game_win_image;
    public GameObject loading_next_level;
    public GameObject david;
    public GameObject already_got_oil_text;
    bool disablePromptText = false;
    private bool fuel_can_empty = false;
    private int timesUsed = 0;

    // private float oil_height = 1.0f;
    bool mouseOver = true;
    override public void Start()
    {
        base.Start();
        barrel_choice.SetActive(false);
        game_win_image.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeOil();
        }
    }
    override public void talk()
    {

        if (timesUsed == 3)
            fuel_can_empty = true;

        if (game_manager.Instance.gotDuctTape)
            {
               // fuel_can_text_duct_tape.GetComponent<Text>().enabled = false;
                game_manager.Instance.scene_manager.SetState(GameState.InteractBarrel);
                barrel_choice.SetActive(true);

            }
            else //otherwise do action if can get fuel
            {

                TakeOil();
            }
        
    }

    void OnMouseOver()
    {
    //    if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 5)
    //    {
    //        if (!disablePromptText)
    //        {
              
    //            if (!game_manager.Instance.gotDuctTape)
    //            {
    //                fuel_can_text.GetComponent<Text>().enabled = true;
    //            }
    //            else
    //            {
    //                fuel_can_text_duct_tape.GetComponent<Text>().enabled = true;
    //            }

    //        }

           
    //    }
    //    else
    //    {
    //        if (!game_manager.Instance.gotDuctTape)
    //        {
    //            fuel_can_text.GetComponent<Text>().enabled = false;
    //        }
    //        else
    //        {
    //            fuel_can_text_duct_tape.GetComponent<Text>().enabled = false;
    //        }
    //    }
        
    }
    public void SealLeakage()
    {
        game_manager.Instance.score_system_analysis++;
        barrel_choice.SetActive(false);
       
      //  game_win_image.SetActive(true);
       // game_manager.Instance.scene_manager.SetState(GameState.InGame);
        print(" WIn yaay");
        StartCoroutine("game_win");

       
    }
    IEnumerator game_win()
    {
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(true);
        game_manager.Instance.disable_touch();
        yield return new WaitForSeconds(2);
        transition_manager.instance.fade(false);
        game_win_image.SetActive(true);

        yield return new WaitForSeconds(1);
        audio_source.PlayOneShot(exit_talk);
        yield return new WaitForSeconds(exit_talk.length + 1);
       
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(1);
        game_win_image.SetActive(false);
        loading_next_level.SetActive(true);
        transition_manager.instance.fade(false);
        
        yield return new WaitForSeconds(2);
        transition_manager.instance.fade(true);
        OfficeLevel();
    }
    public void DisableChoice()
    {
      //  disablePromptText = false;
        barrel_choice.SetActive(false);
        game_manager.Instance.scene_manager.SetState(GameState.InGame);
        game_manager.Instance.talked = false;
    }

    void OfficeLevel()
    {
        game_manager.Instance.scene_manager.SwitchToLevel(2);
        game_manager.Instance.talked = false;
    }

    public void TakeOil()
    {
      //  disablePromptText = false;
      if(!fuel_can_empty)
        {
            barrel_choice.SetActive(false);
            game_manager.Instance.scene_manager.SetState(GameState.InGame);
            oil_level.transform.Translate(0, -0.6f, 0);
            timesUsed++;
            fuel_can_image.gameObject.SetActive(true);
            this.GetComponent<MeshCollider>().enabled = false;
            david.GetComponent<conversation_logic>().SetOil();
            game_manager.Instance.talked = false;
        }
        else //this situation will happen only if player has taken 3 times oil and then he finds duct tape..
                //then he will be given choice "seal barrel" or "take oil" so if he press take oil , we need to check
                //also here .. a bit dirty , but for now working .
        {
            barrel_choice.SetActive(false);
            game_manager.Instance.scene_manager.SetState(GameState.InGame);
            Debug.Log("empty brah");
            StartCoroutine("already_got_oil");
        }


    }

    IEnumerator already_got_oil()
    {
        already_got_oil_text.SetActive(true);

        yield return new WaitForSeconds(2);
        game_manager.Instance.talked = false;
        Color textColor = already_got_oil_text.GetComponent<Text>().color;
        while (textColor.a > 0.1f)
        {
            textColor.a -= 0.05f;
            already_got_oil_text.GetComponent<Text>().color = textColor;
        }
        already_got_oil_text.SetActive(false);
        textColor.a = 1;
        already_got_oil_text.GetComponent<Text>().color = textColor;
        game_manager.Instance.talked = false;

    }



}