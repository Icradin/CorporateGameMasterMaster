using UnityEngine;
using System.Collections;

public class talk_brand_manager : talk_base {

    public AudioClip brand_reject;
    public AudioClip brand_reject2;
    public AudioClip brand_reject3;
    public AudioClip brand_reject4;
    public AudioClip brand_reject5;
    public AudioClip brand_reject6;
    int brand_visit = 0;


    // Use this for initialization
    override public void Start()
    {
        base.Start();

    }

    AudioClip randomRejectClip
    {
        get
        {
            AudioClip clipToReturn = null;
            int random = 0;
            random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    clipToReturn = brand_reject3;
                    break;
                case 1:
                    clipToReturn = brand_reject4;
                    break;
                case 2:
                    clipToReturn = brand_reject5;
                    break;
                case 3:
                    clipToReturn = brand_reject6;
                    break;
            }
            return clipToReturn;
        }
    }

    public override void talk()
    {
        if (brand_visit == 0)
        {
            audio_source.PlayOneShot(brand_reject);
            StartCoroutine("wait_to_finish_talking", brand_reject.length);
            brand_visit++;
        }
        else if(brand_visit == 1)
        {
            audio_source.PlayOneShot(brand_reject2);
            StartCoroutine("wait_to_finish_talking", brand_reject2.length);
            brand_visit++;
        }
        else if (brand_visit > 1)
        {
            AudioClip randomClip = randomRejectClip;
            audio_source.PlayOneShot(randomClip);
            StartCoroutine("wait_to_finish_talking", randomClip.length);
        }


    }

    IEnumerator wait_to_finish_talking(float time )
    {
        yield return new WaitForSeconds(time);
        game_manager.Instance.talked = false;

    }
}
