using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTabBar : MonoBehaviour
{
    public TabScaling[] tabButtons;

    private void Start()
    {
        tabButtons[0].isInitial = true; 
    }

    public void OnTabClicked(TabScaling clickedTab)
    {
        // Deselect all tabs
        foreach (TabScaling tab in tabButtons)
        {
            tab.Deselect();
        }

        // Select the clicked tab
        clickedTab.Select();
        tabButtons[0].isInitial = false;
        SFXManager.instance.PlayButtonClickSound();
    }
}

