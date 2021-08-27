using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuizDataScriptable> quizData;

    [SerializeField] private float timeLimite = 30f;
    private List<Question> questions;
    private Question selectedQuestion;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus{get{ return gameStatus;}}

    private int scoreCOunt = 0;
    private float currentTimer;
    private int lifeRemaining = 3;
    // Start is called before the first frame update

    
    public void StartGame(int index)
    {
        scoreCOunt = 0;
        currentTimer = timeLimite;
        lifeRemaining = 3;
        questions = new List<Question>();

        for(int i = 0; i< quizData[index].questions.Count; i++)
        {
            questions.Add(quizData[index].questions[i]);
        }
        
        SelectQuestion();
        gameStatus = GameStatus.Play;
    }
    private void update()
    {
        if(gameStatus == GameStatus.Play)
        {
            currentTimer -= Time.deltaTime;
            SetTime(currentTimer);
        }
    }

    private void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("mm':'ss");

        if(currentTimer <= 0)
        {
            gameStatus = GameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);   
        }
    }

 
    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        quizUI.SetQuestion(selectedQuestion);
        questions.RemoveAt(val);
    }

    public bool Answer(string Answered)
    {
        bool correctAns = false;
        if(Answered == selectedQuestion.correctAns)
        {
            //correct
            correctAns = true;
            scoreCOunt += 50;
            quizUI.ScoreText.text = "Score:"+ scoreCOunt;
        }
        else
        {
            //wrong
            lifeRemaining--;
            quizUI.ReduceLife(lifeRemaining);

            if(lifeRemaining<0)
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }

        if(gameStatus == GameStatus.Play)
        {
            if(questions.Count > 0)
            {
            Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }
        

        return correctAns;

    }
}
[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public AudioClip questionClip;
    public UnityEngine.Video.VideoClip questionVideo; 
    public List<string> options;
    public string correctAns;
}
[System.Serializable]
public enum QuestionType{
    Text,
    Image,
    Video,
    Audio,
}
[System.Serializable]
public enum GameStatus{
    Next,
    Play,
}
