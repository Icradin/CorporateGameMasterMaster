using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pause_menu_manager : MonoBehaviour {


    [SerializeField]
    GameObject pause_menu;
    public GameObject help_button;
    public GameObject tutorial_screen;
 //   public AudioSource audio_source;


    void Start()
    {
        pause_menu.SetActive(false);
        tutorial_screen.SetActive(false);
       // help_button.SetActive(false);
}
	public void DeactivatePauseMenu()
    {
        Debug.Log("hello");
        pause_menu.SetActive(false);
        help_button.SetActive(true);
        foreach (AudioSource audio_source in game_manager.Instance.all_audio_sources)
        {
            audio_source.UnPause();
        }
    }
    public void ActivatePauseMenu()
    {
        help_button.SetActive(false);
        pause_menu.SetActive(true);
        foreach (AudioSource audio_source in game_manager.Instance.all_audio_sources)
        {
            audio_source.Pause();
        }
    }

    public void EnableTutorial()
    {
        game_manager.Instance.scene_manager.SetState(GameState.NurseChoice);
        tutorial_screen.SetActive(true);
        foreach (AudioSource audio_source in game_manager.Instance.all_audio_sources)
        {
            audio_source.Pause();
        }
    }

    public void DisableTutorial()
    {

        game_manager.Instance.scene_manager.SetState(GameState.InGame);
        tutorial_screen.SetActive(false);
        foreach (AudioSource audio_source in game_manager.Instance.all_audio_sources)
        {
            audio_source.UnPause();
        }
    }
    

}
