using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_manager : MonoBehaviour {

    //Singleton for game manager.
    private static game_manager _instance;
    public static game_manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<game_manager>();
                _instance.tag = "GameManager";
            }
            return _instance;
        }
    }

    private GameState current_state;
    public GameState Current_State
    {
        get { return current_state; }
        set { current_state = value; }
    }

    public pause_menu_manager pause_menu;
    public scene_manager scene_manager;
    private GameObject _player;
    public GameObject Player
    {
        get
        {
            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("player");
            return _player;
        }
    }

    //set by UI options
    private int difficulty_level;
    public int DifficultyLevel
    {
        get { return difficulty_level; }
        set { difficulty_level = value; }
    }
    public bool gotDuctTape = false;

    public int conversation_progression = 0;
    public int boss_talk_progression = 0;
    public bool nothing_to_talk = false;
    public bool boss_lose = false;
    public bool boss_double_visit = false;

    public bool talked = false;

    private List<AudioSource> _audio_sources;

    public GameObject movement_control;
    public GameObject pause_button;
    public GameObject use_button;

    public List<AudioSource> all_audio_sources
    {
        get
        {
            if(_audio_sources == null)
            {
                AudioSource[] allAudios = FindObjectsOfType<AudioSource>();
                _audio_sources = new List<AudioSource>(allAudios);
            }
            return _audio_sources;
        }
    }

    void Awake()
    {
        difficulty_level = 1;
        if (Application.loadedLevel != 0)
        {
            current_state = GameState.InGame;
            FindRefferenceToGameObject();
        }  

    }
    public void disable_touch()
    {
        movement_control.SetActive(false);
        pause_button.SetActive(false);
        use_button.SetActive(false);
    }
    public void enable_touch()
    {
        movement_control.SetActive(true);
        pause_button.SetActive(true);
        use_button.SetActive(true);
    }

    void OnLevelWasLoaded()
    {
        if(current_state == GameState.InGame)
        {
            FindRefferenceToGameObject();
        }
        _audio_sources.Clear();
        _audio_sources = null;
    }

    void FindRefferenceToGameObject()
    {
        pause_menu = FindObjectOfType<pause_menu_manager>();
        scene_manager = FindObjectOfType<scene_manager>();


        movement_control = GameObject.Find("joystick");
        pause_button = GameObject.Find("pause_button");
        use_button = GameObject.Find("use_button");
    }





}
