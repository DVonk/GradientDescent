using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null;

    public bool sendData = true;
    public bool cheat = false;
    public Color scoreColorYes;
    public Color scoreColorNo;
    public int lives = 1;
    public int chosenAnswer = 0;
    public bool roomActive = false;
    public int currentPickupSFX = 1;
    public Text scoreText;
    public List<GameObject> liveContainers = new List<GameObject>();
    public Sprite playerSprite;
    public Sprite playerSpriteDe;

    private int teleporter = 0;
    private int sendDataCheat = 0;

    bool scoreTextFadedOut = false;

    public List<int> chosenAnswers = new List<int>();

    public Room activeRoom;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (cheat)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 20f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 2f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (lives < 10)
                    lives++;
                RefreshLives();

            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (lives > 0)
                    lives--;
                RefreshLives();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                BGMManager.instance.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            teleporter++;
            if (teleporter == 10)
                SceneManager.LoadScene("Results");
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            sendDataCheat++;
            if (sendDataCheat == 10)
            {
                SFXManager.instance.PlaySFX("Accept");
                sendData = false;
            }

        }

    }

    public void IncreaseScore(int number)
    {
        if (activeRoom.requiredScore > activeRoom.currentScore)
        {
            activeRoom.currentScore += number;

        }

        if (!activeRoom.followerRoom)
            scoreText.text = activeRoom.currentScore.ToString() + " / " + activeRoom.requiredScore;


        if (activeRoom.currentScore >= activeRoom.requiredScore)
        {
            if (!scoreTextFadedOut)
                scoreText.color = scoreColorYes;
            if (!activeRoom.roomDone)
            {
                lives++;
                RefreshLives();
                activeRoom.OpenGate();
            }
            activeRoom.roomDone = true;

        }
        else
        {
            if (activeRoom.followerRoom)
                scoreText.color = scoreColorYes;
            else if (!scoreTextFadedOut)
                scoreText.color = scoreColorNo;
        }

        if (activeRoom.noScoreTextRoom)
        {
            scoreText.color = new Color(0, 0, 0, 0);
        }
    }

    public void SetAnswer(int number)
    {
        chosenAnswer = number;
    }

    public void SetRoom(Room room)
    {
        activeRoom = room;
        IncreaseScore(0);

    }

    public void RefreshLives()
    {
        foreach (GameObject obj in liveContainers)
        {
            obj.GetComponent<Image>().sprite = playerSpriteDe;
        }

        if (lives >= 1)
            liveContainers[0].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 2)
            liveContainers[1].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 3)
            liveContainers[2].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 4)
            liveContainers[3].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 5)
            liveContainers[4].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 6)
            liveContainers[5].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 7)
            liveContainers[6].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 8)
            liveContainers[7].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 9)
            liveContainers[8].GetComponent<Image>().sprite = playerSprite;
        if (lives >= 10)
            liveContainers[9].GetComponent<Image>().sprite = playerSprite;



    }

    public void ToggleScoreVisible()
    {
        if (!scoreTextFadedOut)
        {
            scoreText.GetComponent<ScoreText>().StartCoroutine("FadeOut");
            scoreTextFadedOut = true;
        }
        else
        {
            scoreTextFadedOut = false;
            scoreText.GetComponent<ScoreText>().StartCoroutine("FadeIn");
        }

    }


}
