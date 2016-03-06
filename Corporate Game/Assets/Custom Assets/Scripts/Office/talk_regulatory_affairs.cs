using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
public class talk_regulatory_affairs : talk_base {


    public GameObject transition_image;
    int regulatory_affairs_visits = 0;

    public AudioClip audio_meeting;
    public AudioClip audio_meeting_results;

    public AudioClip not_our_fault;
    public AudioClip not_our_fault2;

    public AudioClip cant_help_anymore;
	 // Use this for initialization
    override public void Start () {
        base.Start();

    }

    public override void talk()
    {
        if(boss_talk_progression == 3)
        {
            Debug.Log("advancing time 1 week --- start");
            audio_source.PlayOneShot(audio_meeting);
            StartCoroutine("advance_time", audio_meeting.length);
            return;
        }
      

        if (boss_talk_progression < 3)
        {
            if(regulatory_affairs_visits == 0)
            {
                audio_source.PlayOneShot(not_our_fault);
                StartCoroutine("not_ourt_fault_talking", not_our_fault.length);
                game_manager.Instance.score_proactivity--;
                game_manager.Instance.score_fore_sight--;
                return;
            }
            if (regulatory_affairs_visits == 1)
            {
                audio_source.PlayOneShot(not_our_fault2);
                StartCoroutine("not_ourt_fault_talking2", not_our_fault2.length);
                game_manager.Instance.score_proactivity--;
                game_manager.Instance.score_fore_sight--;
                return;
            }
        }
       if(boss_talk_progression > 3)
        {
            game_manager.Instance.score_proactivity--;
            game_manager.Instance.score_fore_sight--;
            audio_source.PlayOneShot(cant_help_anymore);
            StartCoroutine("cant_help_anymore_talking", cant_help_anymore.length);
            return;
        }
            
    }
    IEnumerator not_ourt_fault_talking(float time)
    {
        Debug.Log("not our fault ");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);
        regulatory_affairs_visits++;
        game_manager.Instance.talked = false;
        Debug.Log("not our fault  -- end");
    }
    IEnumerator not_ourt_fault_talking2(float time)
    {
        Debug.Log("not our fault 2");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);
        game_manager.Instance.talked = false;
        Debug.Log("not our fault 2 -- end");
    }

    IEnumerator cant_help_anymore_talking(float time)
    {
        Debug.Log(" cant help anymore ");
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);

        game_manager.Instance.talked = false;
        Debug.Log(" cant help anymore -- end ");
    }
    
    IEnumerator advance_time (float time)
    {
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;
        speech_bubble.enabled = true;
        yield return new WaitForSeconds(time);
        speech_bubble.enabled = false;
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(false);
        transition_image.SetActive(true);
        Debug.Log("advancing time 1 week --- 1 week later");
        yield return new WaitForSeconds(3);
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(1);
        transition_image.SetActive(false);
        transition_manager.instance.fade(false);
        yield return new WaitForSeconds(2);
        Debug.Log("advancing time 1 week --- playing second audio");
        speech_bubble.enabled = true;
        audio_source.PlayOneShot(audio_meeting_results);
        yield return new WaitForSeconds(audio_meeting_results.length);
        speech_bubble.enabled = false;
        game_manager.Instance.Player.GetComponent<FirstPersonController>().m_WalkSpeed = 10;
        boss_talk_progression++;
        game_manager.Instance.score_proactivity++;
        game_manager.Instance.score_system_analysis++;
        game_manager.Instance.talked = false;

    }
}
