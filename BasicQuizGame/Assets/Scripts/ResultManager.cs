using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text trueText, falseText, scoreText;

    [SerializeField]
    private GameObject firstStar, secondStar, thirdStar;

    public void printResults(int trueCount, int falseCount, int score)
    {
        trueText.text = trueCount.ToString();
        falseText.text = falseCount.ToString();
        scoreText.text = score.ToString();
        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);
        if(trueCount == 1)
        {
            firstStar.SetActive(true);
        }
        else if(trueCount == 2)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        else
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
    }
}
