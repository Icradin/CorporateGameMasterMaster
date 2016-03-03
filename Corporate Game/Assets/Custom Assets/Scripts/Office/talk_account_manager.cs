using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class talk_account_manager : talk_base {

    int key_account_visits = 0;
    public AudioClip cant_help_talk;
    public AudioClip cant_help_talk2;
    public AudioClip all_could_do;
    public AudioClip success_talk;

    public AudioClip double_visit_talk;
    public AudioClip marketing_manager_talk;
    public AudioClip doctor_talking;


    public GameObject nurse_choice;
    public GameObject nurse;
    public GameObject doctor;
    public GameObject manager;
    bool doubleVisitDone = false;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        doctor.GetComponentInChildren<SpriteRenderer>().enabled = false;
        manager.GetComponentInChildren<SpriteRenderer>().enabled = false;
       
    }

    public override void talk()
    {

        if (boss_double_visit && !doubleVisitDone)
        {
            doubleVisitDone = true;
            Debug.Log("Load double visit scene !");
            audio_source.PlayOneShot(double_visit_talk);
            StartCoroutine("double_visit", double_visit_talk.length + 1f);
            return;
        }


        if (conversation_progression < 1)
        {
            if(key_account_visits == 0)
            {
                //PLAY first denial ayduo
              
                audio_source.PlayOneShot(cant_help_talk);
                StartCoroutine("cant_help", cant_help_talk.length);
            }
            else if ( key_account_visits ==1)
            {
                audio_source.PlayOneShot(cant_help_talk2);
                StartCoroutine("cant_help_insist", cant_help_talk2.length);
           
            }
            return;
        }

        if(conversation_progression == 1 && boss_talk_progression ==1)
        {
            audio_source.PlayOneShot(success_talk);
            StartCoroutine("success_talking", success_talk.length);
            return;
        }

       if(conversation_progression > 1)
        {
            audio_source.PlayOneShot(all_could_do);
            StartCoroutine("all_could_do_talking", all_could_do.length);
            return;
        }

        Debug.Log("i am busy leave me");
        game_manager.Instance.talked = false;
    }


    IEnumerator cant_help(float time)
    {
        Debug.Log(" cant help    -- start");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        Debug.Log(" cant help    -- end");
        key_account_visits++;
        game_manager.Instance.talked = false;
    }
    IEnumerator cant_help_insist(float time)
    {
        Debug.Log(" dude cant help sorry --- start");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        Debug.Log(" dude cant help sorry --- end");
        game_manager.Instance.talked = false;
    }
    IEnumerator success_talking(float time)
    {
        Debug.Log("boss progression increased  -- start ");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        boss_talk_progression++;
        speech_bubble.enabled = false;
        Debug.Log("boss progression increased  -- end ");
        game_manager.Instance.talked = false;
    }

    IEnumerator all_could_do_talking(float time)
    {
        Debug.Log("thats all i could do , yo    --- start");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);


        speech_bubble.enabled = false;
        Debug.Log("thats all i could do , yo    --- end");
        game_manager.Instance.talked = false;
    }




    IEnumerator double_visit(float firstAudio)
    {
        Debug.Log("waiting for first speech to finish ... ");
        yield return new WaitForSeconds(firstAudio);
        transition_manager.instance.transition(2, gameObject);
        yield return new WaitForSeconds(1);
        game_manager.Instance.Player.transform.LookAt(doctor.transform);
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;

        yield return new WaitForSeconds(1);
        audio_source.PlayOneShot(doctor_talking);
        doctor.GetComponentInChildren<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(doctor_talking.length + 1);
        doctor.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Debug.Log("waiting for marketing manager to talk");
        yield return new WaitForSeconds(3);
        Debug.Log("marketing manager talking ....");
        manager.GetComponentInChildren<SpriteRenderer>().enabled = true;
        audio_source.PlayOneShot(marketing_manager_talk);
        yield return new WaitForSeconds(marketing_manager_talk.length + 1);
        manager.GetComponentInChildren<SpriteRenderer>().enabled = false;
        game_manager.Instance.scene_manager.SetState(GameState.NurseChoice);
        nurse_choice.SetActive(true);

    }

    public void talk_to_nurse()
    {
        game_manager.Instance.scene_manager.SetState(GameState.InGame);
        nurse_choice.SetActive(false);
        nurse.GetComponent<NavMeshAgent>().SetDestination(game_manager.Instance.Player.transform.position);
        nurse.GetComponent<nusre_logic>().startLogic = true;
    }

    public void reject_talking()
    {
        nurse_choice.SetActive(false);
        game_manager.Instance.boss_lose = true;
        transition_manager.instance.transition(3, gameObject);
        game_manager.Instance.scene_manager.SetState(GameState.InGame);
        game_manager.Instance.talked = false;
    }
}
