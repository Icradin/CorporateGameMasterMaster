using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class talk_marketing : talk_base
{

    public GameObject transition_image;


    public AudioClip marketing_1;
    public AudioClip marketing_2;


    public AudioClip marketing_already_visited;

  

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        transition_image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    IEnumerator advance_time(float firstAudio)
    {
        speech_bubble.enabled = true;
        Debug.Log("Advancing time 2 weeks .. waiting for first speech  ... ");
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;
        yield return new WaitForSeconds(firstAudio); //audio finished
        speech_bubble.enabled = false;              //disable speech buble and wait 1 sec , so thigns go slower
        yield return new WaitForSeconds(1);
        transition_manager.instance.transition(1, gameObject); //do transition , while doing wait 1 sec and set 2 weeks later image to ture
        Debug.Log("transition happening , transition iamge true ");
        yield return new WaitForSeconds(1);
        transition_image.SetActive(true);

        yield return new WaitForSeconds(2); // show it for 2 sec

        Debug.Log("disabling transition");

        transition_manager.instance.fade(true); // fade out

        yield return new WaitForSeconds(1);  //wait fade out

        transition_image.SetActive(false); //disable 2 weeks later image

        transition_manager.instance.fade(false); //fade in

        yield return new WaitForSeconds(2);

        Debug.Log("playing audio 2 after advanced time -> waiting audio "); //playing other audio 
        speech_bubble.enabled = true;
        audio_source.PlayOneShot(marketing_2);

        yield return new WaitForSeconds(marketing_2.length);
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 10;
        speech_bubble.enabled = false;
        boss_talk_progression++;


        game_manager.Instance.talked = false;

        Debug.Log("finalized first  talk. boss talk progresion increased and marketing visits ++");

    }



  


    override public void talk()
    {
        if (conversation_progression == 0 && boss_talk_progression == 0)
        {
          
            audio_source.PlayOneShot(marketing_1);
            StartCoroutine("advance_time", marketing_1.length);

            // --- make newvariable that is marketing visits -- unclear

            return;
        }
        else
        {
         
            audio_source.PlayOneShot(marketing_already_visited);
            StartCoroutine("already_visited", marketing_already_visited.length);
        }
    }


    IEnumerator already_visited(float firstAudio)
    {
        Debug.Log("already visited");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(firstAudio);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);
        game_manager.Instance.talked = false;
    }
}
