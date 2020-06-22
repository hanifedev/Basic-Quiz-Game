using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text questionText;

    public Question[] questions;
    
    private static List<Question> notAnsweredQuestions;
  
    private Question currentQuestion;

    [SerializeField]
    private Text trueText,falseText;

    void Start()
    {
        if(notAnsweredQuestions == null || notAnsweredQuestions.Count == 0)
        {
            notAnsweredQuestions = questions.ToList<Question>();
        }
        selectRandomQuestion();
        Debug.Log("Geçerli soru : " + currentQuestion.question + " cevabı : " + currentQuestion.answer);
    }

    void selectRandomQuestion()
    {
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
            Debug.Log("Doğru butona bastınız");
        }
        else
        {
            Debug.Log("Yanlış cevapladınız");
        }

        StartCoroutine(wait());
    }

    
    public void clickFalseButton()
    {
        if(!currentQuestion.answer)
        {
            Debug.Log("Doğru butona bastınız");
        }
        else
        {
            Debug.Log("Yanlış cevapladınız");
        }

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        notAnsweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
