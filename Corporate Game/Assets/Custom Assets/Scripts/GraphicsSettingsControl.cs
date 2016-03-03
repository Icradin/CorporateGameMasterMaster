using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GraphicsSettingsControl : MonoBehaviour {

    [Header("Quality")]
    private string[] allQualityLevels;
    private int currentQualityLevel;

    [Tooltip("All quality toggles from the ui_canvas -> options -> graphhics_parent ")]
    public Toggle[] qualityToggles;



    void Awake()
    {
      
    
        allQualityLevels = QualitySettings.names;                    //store all available graphics settings                   
        currentQualityLevel = QualitySettings.GetQualityLevel();     //store current             
        qualityToggles[currentQualityLevel].isOn = true;             //set current quality button to active  
    }

    public void SetQualitySetting(string qualitySettingName)
    {
        for (int i = 0; i < allQualityLevels.Length; i++)
        {
            if (allQualityLevels[i] == qualitySettingName)
            {
                QualitySettings.SetQualityLevel(i, true);
                currentQualityLevel = i;
            }
        }
    }

    public void ToggleFunctionality(GameObject go)
    {
        //Switching text color with toggle background color , so it gives us the illusion that is clicked
        // also making the toggle inactive, so you cannot click it if its already activated :)
        Toggle toggle = go.GetComponent<Toggle>();

        if (toggle.isOn)
        {
            toggle.interactable = false;
        }
        else {
            toggle.interactable = true;
        }

        Color textColor = go.GetComponentInChildren<Text>().color;
        Color toggleColor = go.GetComponent<Image>().color;

        go.GetComponentInChildren<Text>().color = toggleColor;
        go.GetComponent<Image>().color = textColor;
    }
}
