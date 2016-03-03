using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum GameState
{
    Menu, InGame, Paused, TalkingWithDavid, InteractBarrel, NurseChoice
}
public class scene_manager : MonoBehaviour {
  
    GameState currentState;
   

    void Awake()
    {
        DontDestroyOnLoad(game_manager.Instance);
    }
    // Use this for initialization
    void Start () {
        currentState = game_manager.Instance.Current_State;

        if (currentState == GameState.InGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (currentState == GameState.Menu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update () {

     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case GameState.InGame:
                    SetState(GameState.Paused);
                    break;
                case GameState.Paused:
                    SetState(GameState.InGame);
                    break;
                default:
                    print(string.Format("Current state is '{0}'. If not expected state check build settings !!", currentState));
                    break;
            }
        }
    }




    public void SetState(GameState newGameState)
    {
        DisablePreviousState(currentState);
        switch (newGameState)
        {
            case GameState.Menu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.InGame:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1; //reset time scale
                break;
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0; // stop ime when paused.
                game_manager.Instance.pause_menu.ActivatePauseMenu();
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = false;

                break;
            case GameState.TalkingWithDavid:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.InteractBarrel:
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.NurseChoice:
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0; // stop ime when paused.
                break;
        }
        currentState = newGameState;

    }


    void DisablePreviousState(GameState previousState)
    {
        switch (previousState)
        {
            case GameState.Menu:
                break;
            case GameState.InGame:
                break;
            case GameState.Paused:
                game_manager.Instance.pause_menu.DeactivatePauseMenu();
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = true;
                break;
            case GameState.InteractBarrel:
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = true;
                break;
            case GameState.NurseChoice:
                game_manager.Instance.Player.GetComponent<FirstPersonController>().enabled = true;
                break;
        }
    }


    public void UnpauseGame()
    {
        SetState(GameState.InGame);
    }

    public void SwitchToLevel(int index)
    {
        game_manager.Instance.Current_State = GameState.InGame;
        SetState(GameState.InGame);
        //Application.LoadLevel(index);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        print("load level " + index);
    }

    public void SwitchToMainMenu()
    {
        game_manager.Instance.Current_State = GameState.Menu;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void CloseGame()
    {
        print("Application shut down !!");
        Application.Quit();
    }

    public void TalkToDavid()
    {
        SetState(GameState.TalkingWithDavid);
    }

    public void GoodbyeDavid()
    {
        SetState(GameState.InGame);
    }
}

