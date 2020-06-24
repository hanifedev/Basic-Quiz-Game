using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text questionText;

    public Question[] questions;
    
    private static List<Question> notAnsweredQuestions;
  
    private Question currentQuestion;

    [SerializeField]
    private Text trueText,falseText;

    [SerializeField]
    private GameObject trueButton, falseButton;

    [SerializeField]
    private GameObject resultPanel;
    
    int trueCount, falseCount;

    int totalPoint;

    ResultManager resultManager;

    void Start()
    {
        trueCount = 0;
        falseCount = 0;
        totalPoint = 0;
        if(notAnsweredQuestions == null || notAnsweredQuestions.Count == 0)
        {
            notAnsweredQuestions = questions.ToList<Question>();
        }
        selectRandomQuestion();
        Debug.Log("Geçerli soru : " + currentQuestion.question + " cevabı : " + currentQuestion.answer);
    }

    void selectRandomQuestion()
    {
        falseButton.GetComponent<RectTransform>().DOLocalMoveX(222f,.2f);
        trueButton.GetComponent<RectTransform>().DOLocalMoveX(-228f,.2f);
        int randomQuestionIndex = Random.Range(0, notAnsweredQuestions.Count);
        currentQuestion = notAnsweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.question;

        if(currentQuestion.answer)
        {
            trueText.text = "DOĞRU CEVAPLADINIZ";
            falseText.text = "YANLIŞ CEVAPLADINIZ";
        }
        else
        {
            trueText.text = "YANLIŞ CEVAPLADINIZ";
            falseText.text = "DOĞRU CEVAPLADINIZ";
        }
    }

    public void clickTrueButton()
    {
        if(currentQuestion.answer)
        {
            trueCount++;
            totalPoint += 100;
        }
        else
        {
            falseCount++;
        }

        falseButton.GetComponent<RectTransform>().DOLocalMoveX(1000f, .2f);   
        StartCoroutine(wait());
    }

    
    public void clickFalseButton()
    {
        if(!currentQuestion.answer)
        {
            trueCount++;
            totalPoint += 100;           
        }
        else
        {
            falseCount++;
        }
        trueButton.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .2f);   
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        notAnsweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(1f);
        if(notAnsweredQuestions.Count <= 0)
        {
            resultPanel.SetActive(true);
            resultManager = Object.FindObjectOfType<ResultManager>();
            resultManager.printResults(trueCount,falseCount,totalPoint);
        }
        else
        {
            selectRandomQuestion();
        }
    }
}
