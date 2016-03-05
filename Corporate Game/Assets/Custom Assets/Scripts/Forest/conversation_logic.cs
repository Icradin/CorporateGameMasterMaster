using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TouchControlsKit;


public class conversation_logic : MonoBehaviour {

   
    public AudioSource audio_source;
    //references game objects
	public GameObject player, talk_to_david, tent_collider,
        water_collider, dirty_water_collider, fruit_collider, fish_collider, oil_collider, camp_fire;
    
	public GameObject talk_button, map_button, daily_tasks_button, goodbye_button, 
        back_button, what_happened_button, survive_button,
        give_water, give_fruit, give_fish, give_oil, give_dirty_water;

    public bool got_dirty_water = false, got_water = false, got_fruit = false, got_fish = false, got_oil = false;

    // daily tasks inventory images
    public GameObject inventory_water, inventory_fruit, inventory_fish, inventory_oil, map_1, map_2, map_3, map_4;

    float health_timer, hydration_timer, hunger_timer, moralle_timer;

   
    //skybox materials
    public Material morning_skybox, day_skybox, afternoon_skybox, night_skybox;
   
    //lights
    public Light main_light, night_light;
  
    //day and actions
    private int day_count = 0, action_count = 2, conversation_level = 1;

    private bool enable_daily_tasks = false;
  

    private ui_manager ui_manager;


    //Just hardcoded bool to know if the fire is ON or OFF.
    [HideInInspector]
    public bool fire_place_ON = false;


    public AudioClip greet_david;
    public AudioClip greet_david2;
    public AudioClip greet_david3;



    public AudioClip what_happened;
    public AudioClip survive_audio;

    public AudioClip gave_oil;
    public AudioClip gave_water;
    public AudioClip gave_dirty_water;
    public AudioClip gave_fruit;
    public AudioClip gave_fish;

    void Start()
    {
        ui_manager = FindObjectOfType<ui_manager>();
        

        camp_fire.GetComponent<ParticleSystem>().Stop();
        camp_fire.GetComponentInChildren<ParticleSystem>().Stop();

        StartCoroutine("change_time");
    }
    IEnumerator change_time()
    {
        yield return new WaitForSeconds(2);
        if (action_count == 0)
        {
            tent_collider.GetComponent<BoxCollider>().enabled = true;
            RenderSettings.skybox = night_skybox;
            main_light.enabled = false;
            night_light.enabled = true;
            //key_light_1.intensity = 0.1f;
            //key_light_1.intensity = 0.1f;

           
        }

        if (action_count == 1)
        {
            RenderSettings.skybox = afternoon_skybox;
            main_light.color = new Color(1.0f, 0.937f, 0.435f, 1.0f);
            main_light.intensity = 0.5f;
            //key_light_1.intensity = 0.5f;
            //key_light_1.intensity = 0.5f;
        }

        if (action_count == 2)
        {
            RenderSettings.skybox = day_skybox;
            main_light.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            main_light.intensity = 0.8f;
            main_light.enabled = true;
            night_light.enabled = false;
            //key_light_1.intensity = 0.5f;
            //key_light_1.intensity = 0.5f;
        }
    }
    void Update()
    {
       // health_timer = health_timer * Time.deltaTime*10;
       // hydration_timer = hydration_timer * Time.deltaTime;
       // hunger_timer = hunger_timer * Time.deltaTime;
      //  moralle_timer = hunger_timer * Time.deltaTime;

        //I need this tranform to be a RectTransfom but for some reason it doesn't work. 
        //the healthbar needs to move left constantly during the dame at a slow pace
        //but this formatting doesn't work for some reason.

       // hydration_bar.transform.position = new Vector3(-health_timer, 0.0f, 0.0f);

    }

    public void KillingPeople()
    {
        if (day_count == 1)
        {

        }

        if (day_count == 2)
        {

        }

        if (day_count == 3)
        {

        }
    }

    public void ActionCount()
    {
        action_count--;
        Debug.Log(action_count);
        if (action_count == 0)
        {
            water_collider.GetComponent<BoxCollider>().enabled = false;
            dirty_water_collider.GetComponent<BoxCollider>().enabled = false;
            fruit_collider.GetComponent<BoxCollider>().enabled = false;
            fish_collider.GetComponent<BoxCollider>().enabled = false;
            oil_collider.GetComponent<MeshCollider>().enabled = false;
            tent_collider.GetComponent<BoxCollider>().enabled = false;
        }
        StartCoroutine("change_time");
    }

    public void ResetActions()
    {
        action_count = 2;
        day_count++;
        water_collider.GetComponent<BoxCollider>().enabled = true;
        dirty_water_collider.GetComponent<BoxCollider>().enabled = true;
        fruit_collider.GetComponent<BoxCollider>().enabled = true;
        fish_collider.GetComponent<BoxCollider>().enabled = true;
        oil_collider.GetComponent<MeshCollider>().enabled = true;
        tent_collider.GetComponent<BoxCollider>().enabled = false;
        KillingPeople();
        StartCoroutine("change_time");

    }
    //Button Functions
    public void GiveWater()
    {

        stop_audio();
        audio_source.PlayOneShot(gave_water);

        ui_manager.IncreaseHydration();

        inventory_water.SetActive(false);
        got_dirty_water = false;
        got_water = false;
        //give_water.GetComponent<Image>().enabled = false;
        //give_water.GetComponent<Button>().enabled = false;
        give_water.gameObject.SetActive(false);

        //give_dirty_water.GetComponent<Image>().enabled = false;
        //give_dirty_water.GetComponent<Button>().enabled = false;
        give_dirty_water.gameObject.SetActive(false);

    }

    public void GiveDirtyWater()
    {

        stop_audio();
        audio_source.PlayOneShot(gave_dirty_water);

        ui_manager.IncreaseHydration();
        ui_manager.DecreaseHealth();

        inventory_water.SetActive(false);
        got_dirty_water = false;
        got_water = false;
        //give_dirty_water.GetComponent<Image>().enabled = false;
        //give_dirty_water.GetComponent<Button>().enabled = false;
        give_dirty_water.gameObject.SetActive(false);


        //give_water.GetComponent<Image>().enabled = false;
        //give_water.GetComponent<Button>().enabled = false;
        give_water.gameObject.SetActive(false);


    }

    public void GiveFruit()
    {

        stop_audio();
        audio_source.PlayOneShot(gave_fruit);

        ui_manager.IncreaseHunger();

        inventory_fruit.SetActive(false);
        got_fruit = false;
        //give_fruit.GetComponent<Image>().enabled = false;
        //give_fruit.GetComponent<Button>().enabled = false;
        give_fruit.gameObject.SetActive(false);

    }


    public void GiveFish()
    {

        stop_audio();
        audio_source.PlayOneShot(gave_fish);

        ui_manager.IncreaseHunger();
        ui_manager.DecreaseHealth();

        inventory_fish.SetActive(false);
        got_fish = false;
        //give_fish.GetComponent<Image>().enabled = false;
        //give_fish.GetComponent<Button>().enabled = false;
        give_fish.gameObject.SetActive(false);


    }


    public void GiveOil()
    {

        stop_audio();
        audio_source.PlayOneShot(gave_oil);

        inventory_oil.SetActive(false);

        got_oil = false;
        //give_oil.GetComponent<Image>().enabled = false;
        //give_water.GetComponent<Button>().enabled = false;
        give_oil.gameObject.SetActive(false);


        activate_fire(true);

        
    }
    public void activate_fire(bool active)
    {
        if(active)
        {

            camp_fire.GetComponent<ParticleSystem>().Play();
            camp_fire.GetComponentInChildren<ParticleSystem>().Play();
            fire_place_ON = true;
        }
        else
        {

            camp_fire.GetComponent<ParticleSystem>().Stop();
            camp_fire.GetComponentInChildren<ParticleSystem>().Stop();
            fire_place_ON = false;
        }
    }
    void stop_audio()
    {
        if (audio_source.isPlaying)
        {
            audio_source.Stop();
        }
    }

    bool inTalking = false;
    bool inDailyTasks = false;
    public void Talk()
    {
        inTalking = true;
        //talk_button.GetComponent<Image>().enabled = false;
        //talk_button.GetComponent<Button>().enabled = false;
        talk_button.gameObject.SetActive(false);

        //map_button.GetComponent<Image>().enabled = false;
        //map_button.GetComponent<Image>().enabled = false;
        map_button.gameObject.SetActive(false);

        //daily_tasks_button.GetComponent<Image>().enabled = false;
        //daily_tasks_button.GetComponent<Button>().enabled = false;
        daily_tasks_button.gameObject.SetActive(false);

        //what_happened_button.GetComponent<Image>().enabled = true;
        //what_happened_button.GetComponent<Button>().enabled = true;
        what_happened_button.gameObject.SetActive(true);

        //survive_button.GetComponent<Image>().enabled = true;
        //survive_button.GetComponent<Image>().enabled = true;
        survive_button.gameObject.SetActive(true);

        //dt_explanation_button.GetComponent<Image>().enabled = true;
        //dt_explanation_button.GetComponent<Image>().enabled = true;
        //dt_explanation_button.gameObject.SetActive(true);

        //back_button.GetComponent<Image>().enabled = true;
        //back_button.GetComponent<Image>().enabled = true;
        back_button.gameObject.SetActive(true);



        conversation_level = 2;

    }
    public void TellWhatHappened()
    {
        stop_audio();

        audio_source.PlayOneShot(what_happened);
    }
    public void SurvivingTalk()
    {
        stop_audio();

        audio_source.PlayOneShot(survive_audio);
    }
    public void ShowMap()
    {
        if (day_count == 0)
            map_1.GetComponent<Image>().enabled = true;
        if (day_count == 1)
            map_2.GetComponent<Image>().enabled = true;
        if (day_count == 2)
            map_3.GetComponent<Image>().enabled = true;
        if (day_count == 3)
            map_4.GetComponent<Image>().enabled = true;

        //talk_button.GetComponent<Image>().enabled = false;
        //talk_button.GetComponent<Button>().enabled = false;
        talk_button.gameObject.SetActive(false);

        //map_button.GetComponent<Image>().enabled = false;
        //map_button.GetComponent<Image>().enabled = false;
        map_button.gameObject.SetActive(false);

        //daily_tasks_button.GetComponent<Image>().enabled = false;
        //daily_tasks_button.GetComponent<Button>().enabled = false;
        daily_tasks_button.gameObject.SetActive(false);

        //goodbye_button.GetComponent<Image>().enabled = false;
        //goodbye_button.GetComponent<Button>().enabled = false;
        goodbye_button.gameObject.SetActive(false);

        //back_button.GetComponent<Image>().enabled = true;
        //back_button.GetComponent<Image>().enabled = true;
        back_button.gameObject.SetActive(true);

        conversation_level = 2;

    }

    public void DailyTasks()
    {
        inDailyTasks = true;
        //talk_button.GetComponent<Image>().enabled = false;
        //talk_button.GetComponent<Button>().enabled = false;
        talk_button.gameObject.SetActive(false);

        //map_button.GetComponent<Image>().enabled = false;
        //map_button.GetComponent<Image>().enabled = false;
        map_button.gameObject.SetActive(false);

        //daily_tasks_button.GetComponent<Image>().enabled = false;
        //daily_tasks_button.GetComponent<Button>().enabled = false;
        daily_tasks_button.gameObject.SetActive(false);

        //goodbye_button.GetComponent<Image>().enabled = false;
        //goodbye_button.GetComponent<Button>().enabled = false;
        goodbye_button.gameObject.SetActive(false);

        //back_button.GetComponent<Image>().enabled = true;
        //back_button.GetComponent<Button>().enabled = true;
        back_button.gameObject.SetActive(true);

        enable_daily_tasks = true;

        conversation_level = 2;

        if (got_dirty_water == true)
        {
            //give_dirty_water.GetComponent<Image>().enabled = true;
            //give_dirty_water.GetComponent<Button>().enabled = true;
            give_dirty_water.gameObject.SetActive(true);
        }

        if (got_water == true)
        {
            //give_water.GetComponent<Image>().enabled = true;
            //give_water.GetComponent<Button>().enabled = true;
            give_water.gameObject.SetActive(true);
        }

        if (got_fruit == true)
        {
            //give_fruit.GetComponent<Image>().enabled = true;
            //give_fruit.GetComponent<Button>().enabled = true;
            give_fruit.gameObject.SetActive(true);
        }

        if (got_fish == true)
        {
            //give_fish.GetComponent<Image>().enabled = true;
            //give_fish.GetComponent<Button>().enabled = true;
            give_fish.gameObject.SetActive(true);
        }

        if (got_oil == true)
        {
            //give_oil.GetComponent<Image>().enabled = true;
            //give_oil.GetComponent<Button>().enabled = true;
            give_oil.gameObject.SetActive(true);

        }


    }

    public void Back()
    {
       

        if (conversation_level == 2)
        {

            if(inDailyTasks)
            {

                give_dirty_water.gameObject.SetActive(false);
                give_water.gameObject.SetActive(false);
                give_fruit.gameObject.SetActive(false);
                give_fish.gameObject.SetActive(false);
                give_oil.gameObject.SetActive(false);
                inDailyTasks = false;

            }

            if(inTalking)
            {
                stop_audio();
               
                //dt_explanation_button.gameObject.SetActive(false);
                survive_button.gameObject.SetActive(false);
                what_happened_button.gameObject.SetActive(false);
                inTalking = false;

            }
            //talk_button.GetComponent<Image>().enabled = true;
            //talk_button.GetComponent<Button>().enabled = true;

            talk_button.gameObject.SetActive(true);

            //map_button.GetComponent<Image>().enabled = true;
            //map_button.GetComponent<Image>().enabled = true;

            map_button.gameObject.SetActive(true);


            //daily_tasks_button.GetComponent<Image>().enabled = true;
            //daily_tasks_button.GetComponent<Button>().enabled = true;

            daily_tasks_button.gameObject.SetActive(true);

             back_button.gameObject.SetActive(false);
            //what_happened_button.GetComponent<Image>().enabled = false;
            //what_happened_button.GetComponent<Button>().enabled = false;
          



            //survive_button.GetComponent<Image>().enabled = false;
            //survive_button.GetComponent<Image>().enabled = false;


            //dt_explanation_button.GetComponent<Image>().enabled = false;
            //dt_explanation_button.GetComponent<Image>().enabled = false;

          

            //back_button.GetComponent<Image>().enabled = false;
            //back_button.GetComponent<Image>().enabled = false;

          

            //goodbye_button.GetComponent<Image>().enabled = true;
            //goodbye_button.GetComponent<Button>().enabled = true;

            goodbye_button.gameObject.SetActive(true);


            //give_water.GetComponent<Image>().enabled = false;
            //give_water.GetComponent<Button>().enabled = false;

         

            //give_water.GetComponent<Image>().enabled = false;
            //give_water.GetComponent<Button>().enabled = false;


            //give_fruit.GetComponent<Image>().enabled = false;
            //give_fruit.GetComponent<Button>().enabled = false;



            //give_fish.GetComponent<Image>().enabled = false;
            //give_fish.GetComponent<Button>().enabled = false;



            //give_oil.GetComponent<Image>().enabled = false;
            //give_oil.GetComponent<Button>().enabled = false;



        }


        map_1.GetComponent<Image>().enabled = false;
        map_2.GetComponent<Image>().enabled = false;
        map_3.GetComponent<Image>().enabled = false;
        map_4.GetComponent<Image>().enabled = false;

        conversation_level = 1;

        enable_daily_tasks = false;


    }

    public void GoodBye()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        //call_cursor_test.ToggleCursorState();
        game_manager.Instance.scene_manager.GoodbyeDavid();
      
        game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = true;

        //talk_button.GetComponent<Image>().enabled = false;
        //talk_button.GetComponent<Button>().enabled = false;
        talk_button.gameObject.SetActive(false);

        //map_button.GetComponent<Image>().enabled = false;
        //map_button.GetComponent<Image>().enabled = false;
        map_button.gameObject.SetActive(false);

        //daily_tasks_button.GetComponent<Image>().enabled = false;
        //daily_tasks_button.GetComponent<Button>().enabled = false;
        daily_tasks_button.gameObject.SetActive(false);

        //goodbye_button.GetComponent<Image>().enabled = false;
        //goodbye_button.GetComponent<Button>().enabled = false;
        goodbye_button.gameObject.SetActive(false);

        //give_water.GetComponent<Image>().enabled = false;
        //give_water.GetComponent<Button>().enabled = false;
        give_water.gameObject.SetActive(false);

        //give_fruit.GetComponent<Image>().enabled = false;
        //give_fruit.GetComponent<Button>().enabled = false;
        give_fruit.gameObject.SetActive(false);

        //give_fish.GetComponent<Image>().enabled = false;
        //give_fish.GetComponent<Button>().enabled = false;
        give_fish.gameObject.SetActive(false);

        //give_oil.GetComponent<Image>().enabled = false;
        //give_oil.GetComponent<Button>().enabled = false;
        give_oil.gameObject.SetActive(false);

        //what_happened_button.GetComponent<Image>().enabled = false;
        //what_happened_button.GetComponent<Button>().enabled = false;
        what_happened_button.gameObject.SetActive(false);

        //survive_button.GetComponent<Image>().enabled = false;
        //survive_button.GetComponent<Button>().enabled = false;
        survive_button.gameObject.SetActive(false);

        //dt_explanation_button.GetComponent<Image>().enabled = false;
        //dt_explanation_button.GetComponent<Button>().enabled = false;
       // dt_explanation_button.gameObject.SetActive(false);
    }

    //public setter functions
    public void SetOil()
    {
        got_oil = true;
    }

    public void SetWater()
    {
        got_water = true;
    }

    public void SetDirtyWater()
    {
        got_dirty_water = true;
    }

    public void SetFruit()
    {
        got_fruit = true;
    }

    public void SetFish()
    {
        got_fish = true;
    }

 //   void OnMouseEnter()
 //   {
 //       //enables talk to david prompt
		
	//}



    AudioClip randomGreetingClip
    {
        get
        {
            AudioClip clipToReturn = null;
            int random = 0;
            random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    clipToReturn = greet_david;
                    break;
                case 1:
                    clipToReturn =  greet_david2;
                    break;
                case 2:
                    clipToReturn = greet_david3;
                    break;
            }
            return clipToReturn;
        }
    }

	void OnMouseOver ()
    {
        if (Vector3.Distance(transform.position, game_manager.Instance.Player.transform.position) < 5)
        {
            talk_to_david.GetComponent<Text>().enabled = true;
            if (Input.GetKeyDown(KeyCode.E) || TCKInput.GetButtonDown("use"))
            {

                audio_source.PlayOneShot(randomGreetingClip);
                //disables talk to david text 
                gameObject.GetComponent<BoxCollider>().enabled = false;

                //toggles cursor off
                // call_cursor_test.ToggleCursorState();
                game_manager.Instance.scene_manager.TalkToDavid();
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = false;
                //disables player movement and mouselook
                //  player.GetComponent<FirstPersonController>().enabled = false;




                //enables main conversation buttons
                //talk_button.GetComponent<Image>().enabled = true;
                //talk_button.GetComponent<Button>().enabled = true;

                talk_button.gameObject.SetActive(true);

                //map_button.GetComponent<Image>().enabled = true;
                //map_button.GetComponent<Image>().enabled = true;

                map_button.gameObject.SetActive(true);

                //daily_tasks_button.GetComponent<Image>().enabled = true;
                //daily_tasks_button.GetComponent<Button>().enabled = true;

                daily_tasks_button.gameObject.SetActive(true);

                //goodbye_button.GetComponent<Image>().enabled = true;
                //goodbye_button.GetComponent<Button>().enabled = true;


                goodbye_button.gameObject.SetActive(true);


                //checks daily tasks buttons and enables them if you have the items


            }
        }
        else
        {
            talk_to_david.GetComponent<Text>().enabled = false;
        }
       
	}

    void OnMouseExit()
    {
        //disables talk to david prompt
        talk_to_david.GetComponent<Text>().enabled = false;
    }

 
	

}
