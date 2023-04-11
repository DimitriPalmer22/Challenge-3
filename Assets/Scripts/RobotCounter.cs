using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotCounter : MonoBehaviour
{
    public static RobotCounter Instance { get; private set; }

    private TMP_Text countText;
    private int robotCount;
    private int robotsFixed = 0;

    public TMP_Text winText;
    public bool finalLevel;

    [HideInInspector] public bool gameLost = false;
    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool gameOver => gameWon || gameLost;

    private void Awake()
    {
        if (Instance != this) Instance = this;

        countText = GetComponent<TMP_Text>();

        GetRobotCount();
        UpdateText();
        winText.gameObject.SetActive(false);
    }

    private void GetRobotCount()
    {
        robotCount = GameObject.FindGameObjectsWithTag("Enemy").Count();
    }

    public void AddFixedRobot()
    {
        robotsFixed++;
        UpdateText();

        // win
        if (robotsFixed >= robotCount)
        {
            gameWon = true;
            if (finalLevel) DisplayEndMessage("You Win!\nGame Created By Dimitri Palmer\nPress R To Restart!");
            else DisplayEndMessage("You Win! Talk to Jambi to go to Level 2!");
            BackgroundMusicController.Instance.PlayWinMusic();
        }
    }

    private void UpdateText()
    {
        countText.text = $"Robots Fixed: {robotsFixed} / {robotCount}";
    }

    public void DisplayEndMessage(string text)
    {
        winText.gameObject.SetActive(true);
        winText.text = text;
    }

}
