using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class GameController : MonoBehaviour
{
    static private GameController s;

    [Header("Set in Unity")]
    public Text uiLevel;
    public Text uiShots;
    public Text uiButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamically Set")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode;
    public string showing = "Show Slingshot";

    void Start()
    {
        s = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }



    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        LineController.s.Clear();
        GoalController.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }



    void UpdateGUI()
    {
        uiLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uiShots.text = "Shots Taken: " + shotsTaken;
    }




    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && GoalController.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }



    void NextLevel()
    {
        level++;

        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }



    public void SwitchView(string eView = "")
    {                
        if (eView == "")
        {
            eView = uiButton.text;
        }

        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                CameraController.POI = null;
                uiButton.text = "Show Castle";
                break;


            case "Show Castle":
                CameraController.POI = s.castle;
                uiButton.text = "Show Both";
                break;


            case "Show Both":
                CameraController.POI = GameObject.Find("ViewBoth");
                uiButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        s.shotsTaken++;
    }
}
