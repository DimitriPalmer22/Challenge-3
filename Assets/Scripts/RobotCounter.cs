using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotCounter : MonoBehaviour
{

    public static RobotCounter Instance { get; private set; }

    private TMP_Text text;
    private int robotCount;
    private int robotsFixed = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null) Instance = this;

        text = GetComponent<TMP_Text>();

        GetRobotCount();
        UpdateText();
    }

    private void GetRobotCount()
    {
        robotCount = GameObject.FindGameObjectsWithTag("Enemy").Count();
    }

    public void AddFixedRobot()
    {
        robotsFixed++;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = $"Robots Fixed: {robotsFixed} / {robotCount}";
    }

}
