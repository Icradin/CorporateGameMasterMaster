using UnityEngine;
using System.Collections;

public class talk_boss : talk_base
{
    public GameObject win_screen;
    public GameObject game_over_screen;
    bool bossImrpessed = false;

    public AudioClip talk_account_manager;
    public AudioClip boss_talk_double_visit;
    public AudioClip boss_impressed;
    public AudioClip boss_disappointed;
    public AudioClip boss_win;
    public AudioClip boss_talk_other;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        win_screen.SetActive(false);
    }
    public override void talk()
    {



        if (boss_talk_progression == 1 && conversation_progression == 0)
        {
            // play boss marketing audio .. informs that plaer can talk to account manager bout the problem
            Debug.Log("maybe talk to account manager ... begin");
            audio_source.PlayOneShot(talk_account_manager);
            StartCoroutine("talk_to_account_manager", talk_account_manager.length);
            return;
        }

        if (boss_talk_progression == 2 && boss_lose == false && !boss_double_visit)
        {
            //play audio to inform that player can do a double vist 

            Debug.Log("boss progress - tells you can do double visit ---- start talk");
            audio_source.PlayOneShot(boss_talk_double_visit);
            StartCoroutine("double_visist_talking", boss_talk_double_visit.length);
            return;
        }

        if (boss_talk_progression == 3 && !bossImrpessed)
        {
            Debug.Log("boss impressed --- start");

            audio_source.PlayOneShot(boss_impressed);
            StartCoroutine("boss_impressed_talking", boss_impressed.length);


            return;
        }

        if (boss_talk_progression == 2 && boss_lose == true)
        {
            Debug.Log("boss is dissapointed .. you loose your job..");
            audio_source.PlayOneShot(boss_disappointed);
            StartCoroutine("game_over", boss_disappointed.length + 1);
            return;
        }



        if (boss_talk_progression == 4)
        {
            audio_source.PlayOneShot(boss_win);
            StartCoroutine("game_win", boss_win.length);
            return;
        }
        if (nothing_to_talk)
        {
            Debug.Log("boss tells u to talk to somebody else --- begin");
            audio_source.PlayOneShot(boss_talk_other);
            StartCoroutine("talk_to_somebody_else", boss_talk_other.length);

            return;
        }

        Debug.Log("Bosscurrently buzy !!");
        game_manager.Instance.talked = false;

    }
    IEnumerator game_win(float time)
    {
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(1);
        win_screen.SetActive(true);
        transition_manager.instance.fade(false);

    }

    
    IEnumerator game_over(float audiolenght)
    {
        yield return new WaitForSeconds(audiolenght);
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(1);

        game_over_screen.SetActive(true);
        game_manager.Instance.scene_manager.SetState(GameState.InteractBarrel);//so we dont move , mouse move is enable , etc...
                                                                                //using this one just for the fucntionality
        Debug.Log("gameover");
    }


    IEnumerator talk_to_account_manager(float audiolenght)
    {
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(audiolenght);
        Debug.Log("over ... acc manager.");
        nothing_to_talk = true;
        conversation_progression++;
        speech_bubble.enabled = false;
        game_manager.Instance.talked = false;
    }

    IEnumerator double_visist_talking(float audiolenght)
    {
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(audiolenght);
        Debug.Log("boss progress - tells you can do double visit ---- end talk");
        conversation_progression++;
        boss_double_visit = true;
        nothing_to_talk = true;
        speech_bubble.enabled = false;
        game_manager.Instance.talked = false;
    }
    IEnumerator boss_impressed_talking(float audiolenght)
    {
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(audiolenght);
        Debug.Log("boss impressed --- end");
        bossImrpessed = true;
        conversation_progression++;
        nothing_to_talk = true;
        game_manager.Instance.talked = false;
    }
    IEnumerator talk_to_somebody_else(float audiolenght)
    {
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(audiolenght);
        Debug.Log("boss tells u to talk to somebody else --- end");
        nothing_to_talk = false;
        speech_bubble.enabled = false;
        game_manager.Instance.talked = false;
    }

}
