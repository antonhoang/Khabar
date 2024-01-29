using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalLevelManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image background;

    void Start()
    {
        int level = LevelSelectButton.selectedLevel;
        background.sprite = backgrounds[level];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
