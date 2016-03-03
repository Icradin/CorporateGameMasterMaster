using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class menu_manager : MonoBehaviour
{


    public GameObject options_menu;

    public Button start_button;
    public Button options_button;
    public Button exit_button;

    public Button back_to_main_menu_button;

    Slider difficulty_slider;
    // Use this for initialization
    void Start()
    {

        difficulty_slider = options_menu.GetComponentInChildren<Slider>();
        options_menu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void StopMainMenu()
    {
        start_button.interactable = false;
        options_button.interactable = false;
        exit_button.interactable = false;
    }
    private void StartMainMenu()
    {
        start_button.interactable = true;
        options_button.interactable = true;
        exit_button.interactable = true;
    }
    public void OnValueChanged()
    {

        game_manager.Instance.DifficultyLevel = (int)difficulty_slider.value;
        Debug.Log(game_manager.Instance.DifficultyLevel);
    }

    public void ActivateOptions()
    {
        options_menu.SetActive(true);
        StopMainMenu();
    }

    public void DisableOptions()
    {
        options_menu.SetActive(false);
        StartMainMenu();
    }
}