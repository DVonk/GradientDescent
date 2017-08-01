using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WWWManager : MonoBehaviour
{
    public bool sendData = true;

    public Color yesColor;
    public Color noColor;

    public Text c1;
    public Text c2;
    public Text c3;
    public Text c4;
    public Text c5;
    public Text errorText;
    public Text count;
    public Text thankYou;

    public Text quitButton;

    public int playerCount;
    public int choice1Yes;
    public int choice2Yes;
    public int choice3Yes;
    public int choice4Yes;
    public int choice5Yes;

    IEnumerator Start()
    {
        sendData = GameManager.instance.sendData;

        string url = "http://randomwebsite.com";
        if (sendData)
        {
            url += "c1=" + (GameManager.instance.chosenAnswers[0] == 1 ? "1" : "0");
            url += "&c2=" + (GameManager.instance.chosenAnswers[1] == 1 ? "1" : "0");
            url += "&c3=" + (GameManager.instance.chosenAnswers[2] == 1 ? "1" : "0");
            url += "&c4=" + (GameManager.instance.chosenAnswers[3] == 1 ? "1" : "0");
            url += "&c5=" + (GameManager.instance.chosenAnswers[4] == 1 ? "1" : "0");
            url += "&sendData=1";
        }


        WWW www = new WWW(url);
        yield return www;

        if (www.error.Length == 0)
        {
            string[] data = www.text.Split(' ');

            playerCount = int.Parse(data[0]);
            choice1Yes = int.Parse(data[1]);
            choice2Yes = int.Parse(data[2]);
            choice3Yes = int.Parse(data[3]);
            choice4Yes = int.Parse(data[4]);
            choice5Yes = int.Parse(data[5]);

            StartCoroutine("ShowResults");
        }
        else
        {
            Debug.Log(www.error);
            errorText.gameObject.SetActive(true);
            errorText.text = www.error;

            StartCoroutine("ThankYou");
        }

    }


    IEnumerator ShowResults()
    {
        Debug.Log(playerCount + " " + choice1Yes + " " + choice2Yes + " " + choice3Yes + " " + choice4Yes + " " + choice5Yes + " ");

        int c1Percent, c2Percent, c3Percent, c4Percent, c5Percent;
        if (GameManager.instance.chosenAnswers[0] == 1)
        {
            c1Percent = Mathf.RoundToInt((float)choice1Yes / playerCount * 100);
            c1.text = "You and " + c1Percent + "% of players said yes before it even began.";
            c1.color = yesColor;
        }
        else
        {
            c1Percent = Mathf.RoundToInt((float)(playerCount - choice1Yes) / playerCount * 100);
            c1.text = "You and " + c1Percent + "% of players said no before it even began.";
            c1.color = noColor;
        }


        if (GameManager.instance.chosenAnswers[1] == 1)
        {
            c2Percent = Mathf.RoundToInt((float)choice2Yes / playerCount * 100);
            c2.text = "You and " + c2Percent + "% of players trusted the stranger.";
            c2.color = yesColor;
        }
        else
        {
            c2Percent = Mathf.RoundToInt((float)(playerCount - choice2Yes) / playerCount * 100);
            c2.text = "You and " + c2Percent + "% of players did not trust the stranger.";
            c2.color = noColor;
        }
  

        if (GameManager.instance.chosenAnswers[2] == 1)
        {
            c3Percent = Mathf.RoundToInt((float)choice3Yes / playerCount * 100);
            c3.text = "You and " +c3Percent + "% of players rescued the sister against her will.";
            c3.color = yesColor;
        }
        else
        {
            c3Percent = Mathf.RoundToInt((float)(playerCount - choice3Yes) / playerCount * 100);
            c3.text = "You and " + c3Percent + "% of players left the sister alone.";
            c3.color = noColor;
        }
     

        if (GameManager.instance.chosenAnswers[3] == 1)
        {
            c4Percent = Mathf.RoundToInt((float)choice4Yes / playerCount * 100);
            c4.text = "You and " + c4Percent + "% of players gave the killer a second chance.";
            c4.color = yesColor;
        }          
        else
        {
            c4Percent = Mathf.RoundToInt((float)(playerCount - choice4Yes) / playerCount * 100);
            c4.text = "You and " + c4Percent + "% of players didn't give the killer a second chance.";
            c4.color = noColor;
        }
          

        if (GameManager.instance.chosenAnswers[4] == 1)
        {
            c5Percent = Mathf.RoundToInt((float)choice5Yes / playerCount * 100);
            c5.text = "You and " + c5Percent + "% of players wanted to save themselves.";
            c5.color = yesColor;
        }        
        else
        {
            c5Percent = Mathf.RoundToInt((float)(playerCount - choice5Yes) / playerCount * 100);
            c5.text = "You and " + c5Percent + "% of players didn't want to save themselves.";
            c5.color = noColor;
        }
   
        count.text = "Total Players: " + playerCount.ToString();

        yield return new WaitForSeconds(7f);
        while (c1.color.a < 1)
        {
            c1.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        while (c2.color.a < 1)
        {
            c2.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        while (c3.color.a < 1)
        {
            c3.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        while (c4.color.a < 1)
        {
            c4.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        while (c5.color.a < 1)
        {
            c5.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        while (count.color.a < 1)
        {
            count.color += new Color(0, 0, 0, Time.deltaTime / 2.5f);
            yield return null;
        }

        yield return new WaitForSeconds(1f);


        while (thankYou.color.a < 1)
        {
            thankYou.color += new Color(0, 0, 0, Time.deltaTime / 3);
            yield return null;
        }

        while (quitButton.color.a < 1)
        {
            quitButton.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return null;
        }


    }

    IEnumerator ThankYou()
    {
        while (thankYou.color.a < 1)
        {
            thankYou.color += new Color(0, 0, 0, Time.deltaTime / 3);
            yield return null;
        }

        while (quitButton.color.a < 1)
        {
            quitButton.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return null;
        }
    }
}
