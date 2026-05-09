using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject healthBar;
    public GameObject enemyHealthBar;

    public Question[] katakanaQuestions;
    public Question[] hiraganaQuestions;

    [SerializeField] private float timeBetweenQuestions;

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField] private TMP_Text factText;
    [SerializeField] private Button[] answerButtons;
  
    private int correctAnswerIndex;

    private bool isAnsweringQuestion = false;

    void Start()
    {
        LoadSelectedCategory();
        SetCurrentQuestion();
    }

    private void LoadSelectedCategory()
    {
        bool isHiraganaSelected = PlayerPrefs.GetInt("IsHiraganaSelected", 1) == 1;

        Question[] selectedQuestions = isHiraganaSelected ? hiraganaQuestions : katakanaQuestions;
        
        unansweredQuestions = selectedQuestions.ToList();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.question;

        
        List<string> answerChoices = new List<string>();    // Tworzê listê mo¿liwych odpowiedzi z poprawn¹ odpowiedzi¹ i trzema losowymi b³êdnymi odpowiedziami

        answerChoices.Add(currentQuestion.answers[currentQuestion.correctAnswerIndex]);  // Odajê poprawn¹ odpowiedŸ
        List<string> incorrectAnswers = currentQuestion.answers.ToList();
        
        incorrectAnswers.RemoveAt(currentQuestion.correctAnswerIndex);       // Usuwam poprawn¹ odpowiedŸ
        for (int i = 0; i < 3; i++)      // Dodajê resztê trzech przypadkowych odpowiedzi
        {
            int randomIndex = Random.Range(0, incorrectAnswers.Count);
            answerChoices.Add(incorrectAnswers[randomIndex]);
            incorrectAnswers.RemoveAt(randomIndex);
        }

        // Mieszanie
        int n = answerChoices.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            string temp = answerChoices[k];
            answerChoices[k] = answerChoices[n];
            answerChoices[n] = temp;
        }

        // Tekst dla przycisków
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = answerChoices[i];
        }

        // Poprawny indeks przycisku
        correctAnswerIndex = answerChoices.IndexOf(currentQuestion.answers[currentQuestion.correctAnswerIndex]);

    }

    public void CheckAnswer(int answerIndex)
    {
        if (isAnsweringQuestion)
        {
            Debug.Log("Wait for the next question!");
            return;
        }

        isAnsweringQuestion = true; // Gracz aktualnie odpowiada

        StartCoroutine(CheckAnswerCoroutine(answerIndex));
    }

    private IEnumerator CheckAnswerCoroutine(int answerIndex)
    {
        yield return new WaitForSeconds(0.3f);

        if (answerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct!");

            Attack();
            enemyHealthBar.GetComponent<EnemyHealthBar>().TakeDamage(1);
        }
        else
        {
            Debug.Log("Wrong answer");

            GetHit();
            healthBar.GetComponent<HealthBar>().TakeDamage(1);
        }

        yield return new WaitForSeconds(timeBetweenQuestions); // Poczekaj na pytanie

        isAnsweringQuestion = false;

        SetCurrentQuestion();
    }

    void Attack()
    {
        playerAnimator.SetTrigger("Samurai Attack");

        enemyAnimator.SetTrigger("Hit");
    }

    void GetHit()
    {
        playerAnimator.SetTrigger("Samurai Hit");

        enemyAnimator.SetTrigger("Attack 1");
    }
  
}

