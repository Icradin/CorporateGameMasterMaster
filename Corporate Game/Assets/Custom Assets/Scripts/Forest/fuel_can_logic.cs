using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fuel_can_logic : MonoBehaviour
{
    public AudioClip exit_talk;
    public AudioSource audio_source;
    public GameObject fuel_can_text;
    public GameObject fuel_can_text_duct_tape;
    public GameObject fuel_can_image;
    public GameObject oil_barrel;
    public GameObject oil_level;
    public GameObject barrel_choice;
    public GameObject game_win_image;
    public GameObject loading_next_level;
    public GameObject david;

    bool disablePromptText = false;
    private bool fuel_can_empty = false;
    private int timesUsed = 0;

    // private float oil_height = 1.0f;
    bool mouseOver = true;
    void Start()
    {
        barrel_choice.SetActive(false);
        game_win_image.SetActive(false);
    }
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 5)
        {

           
        }

    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.G))
        //    oil_level.transform.Translate(0, -0.6f, 0);
    }
    void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 5)
        {
            if (!disablePromptText)
            {
                if (timesUsed == 3)
                    fuel_can_empty = true;
                if (!game_manager.Instance.gotDuctTape)
                {
                    fuel_can_text.GetComponent<Text>().enabled = true;
                }
                else
                {
                    fuel_can_text_duct_tape.GetComponent<Text>().enabled = true;
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                disablePromptText = true;

                if (game_manager.Instance.gotDuctTape)
                {
                    fuel_can_text_duct_tape.GetComponent<Text>().enabled = false;
                    game_manager.Instance.scene_manager.SetState(GameState.InteractBarrel);
                    barrel_choice.SetActive(true);

                }
                else if (!fuel_can_empty) //otherwise do action if can get fuel
                {

                    TakeOil();
                }
            }
        }
        else
        {
            if (!game_manager.Instance.gotDuctTape)
            {
                fuel_can_text.GetComponent<Text>().enabled = false;
            }
            else
            {
                fuel_can_text_duct_tape.GetComponent<Text>().enabled = false;
            }
        }
        
    }
    public void SealLeakage()
    {
      
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
        disablePromptText = false;
        barrel_choice.SetActive(false);
        game_manager.Instance.scene_manager.SetState(GameState.InGame);
    }

    void OfficeLevel()
    {
        game_manager.Instance.scene_manager.SwitchToLevel(2);
    }

    public void TakeOil()
    {
        disablePromptText = false;
        barrel_choice.SetActive(false);
        game_manager.Instance.scene_manager.SetState(GameState.InGame);
        oil_level.transform.Translate(0, -0.6f, 0);
        timesUsed++;
        fuel_can_image.gameObject.SetActive(true);
        fuel_can_text.GetComponent<Text>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
        david.GetComponent<conversation_logic>().SetOil();

    }
    void OnMouseExit()
    {

        if (!game_manager.Instance.gotDuctTape)
        {
            fuel_can_text.GetComponent<Text>().enabled = false;
        }
        else
        {
            fuel_can_text_duct_tape.GetComponent<Text>().enabled = false;
        }
    }


}