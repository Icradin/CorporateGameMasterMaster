using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchControlsKit;
public class player_stats : MonoBehaviour {

    public GameObject game_over_screen;
    public int health;
    public int hydration;
    public int hunger;
    public int morale;

    int health_max;
    int hydration_max;
    int hunger_max;
    int morale_max;

    public int image_offset;

    // Use this for initialization
    void Awake () {

        UpdateStats();
        health_max = health;
        hydration_max = hydration;
        hunger_max = hunger;
        morale_max = morale;


    }


    void UpdateStats()
    {
        switch(game_manager.Instance.DifficultyLevel)
        {
            case 1: //easy
                health = 5;
                hydration = 5;
                hunger = 5;
                morale = 5;
                image_offset = 42;
                print("easy");
                break;
            case 2: //medium
                health = 4;
                hydration = 4;
                hunger = 4;
                morale = 4;
                image_offset = 53;
                print("medium");
                break;
            case 3: //hard
                health = 3;
                hydration = 3;
                hunger = 3;
                morale = 3;
                image_offset = 70;
                print("hard");
                break;
        }
    }

    public bool CheckMaxMorale()
    {
        if (morale >= morale_max)
        {
            morale = morale_max;
            return  true;
        }
        else
        {
            return false;
        }

    }

    public bool CheckMaxHealth()
    {
        if (health >= health_max)
        {
            health = health_max;
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool CheckMaxHydration()
    {
        if (hydration >= hydration_max)
        {
            hydration = hydration_max;
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool CheckMaxHunger()
    {
        if (hunger >= hunger_max)
        {
            hunger = hunger_max;
            return true;
        }
        else
        {
            return false;
        }

    }
    bool dead = false;
    public void CheckDeathStats()
    {

        string deadText = " You died from : ";
        if (health <= 0)
        {
            deadText += "Health loss ";
            dead = true;
            print("You died from health loss ");

        }

        if (hydration <= 0)
        {
            deadText += "Dehydration ";
            dead = true;
            print("You died from dehydration ");
        }

        if (hunger <= 0)
        {
            deadText += "Hunger ";
            dead = true;
            print("You died from starving ");
        }
        if (morale <= 0)
        {
            deadText += "Bad Morale";
            dead = true;
            print("You died from loosing hope of living ");
        }

        if (dead)
            StartCoroutine("game_over", deadText);


    }

    IEnumerator game_over(string text)
    {
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(true);
        game_manager.Instance.disable_touch();
        yield return new WaitForSeconds(1);
        transition_manager.instance.fade(false);
        game_over_screen.SetActive(true);
        game_over_screen.GetComponentInChildren<Text>().text = text;
        yield return new WaitForSeconds(4);
        transition_manager.instance.fade(true);
        yield return new WaitForSeconds(2);
        game_manager.Instance.scene_manager.SetState(GameState.Menu);
        game_manager.Instance.scene_manager.SwitchToLevel(0);
    }
    
}
