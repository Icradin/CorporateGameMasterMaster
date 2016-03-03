using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class forest_scene_audio_logic : MonoBehaviour {


    public AudioClip enter_talk;


  


    AudioSource audio_source;

    // Use this for initialization
    void Start () {    
        audio_source = GetComponent<AudioSource>();
        StartCoroutine("first_audio",1);
	}



    IEnumerator first_audio(float time)
    {

        //yield return new WaitForSeconds(time);
        //game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;
        //audio_source.PlayOneShot(enter_talk);
        //yield return new WaitForSeconds(enter_talk.length + 1);
        transition_manager.instance.fade(false);
        yield return new WaitForSeconds(0.5f);
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 10;
    }
	
	
}
