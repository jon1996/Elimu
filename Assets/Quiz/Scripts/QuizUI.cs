using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Text questionText,scoreText,timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel, OptionPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio; 
    [SerializeField] private List<Button> options, uiButtons;
    [SerializeField] private Color correctCol, wrongCol, NormalCol;

    private Question question;
    private bool Answered;

    private float audioLength;

    public Text ScoreText{ get {return scoreText;}}
    public Text TimerText{ get {return timerText;}}

    public GameObject GameOverPanel{get {return gameOverPanel;} }


    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i<options.Count; i++ )
        {
            Button LocalBtn = options[i];
            LocalBtn.onClick.AddListener(()=> OnClick(LocalBtn));
        }
        for(int i = 0; i<uiButtons.Count; i++ )
        {
            Button LocalBtn = uiButtons[i];
            LocalBtn.onClick.AddListener(()=> OnClick(LocalBtn));
        }
        
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.Text:
            questionImage.transform.parent.gameObject.SetActive(false);
            break;
            case QuestionType.Image:
            ImageHolder();
            questionImage.transform.gameObject.SetActive(true);
            questionImage.sprite = question.questionImg;
            break;
            case QuestionType.Video:
            questionVideo.transform.gameObject.SetActive(true);
            questionVideo.clip = question.questionVideo;
            questionVideo.Play();
            ImageHolder();
            break;
            case QuestionType.Audio:
            ImageHolder();
            questionAudio.transform.gameObject.SetActive(true);
            audioLength = question.questionClip.length;
            StartCoroutine(PlayAudio());
            break;
        }
        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for(int i=0; i< options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = NormalCol;
        }
        Answered = false;

    }

    IEnumerator PlayAudio(){
        if(question.questionType == QuestionType.Audio)
        {
            questionAudio.PlayOneShot(question.questionClip);
            yield return new WaitForSeconds(audioLength + 0.5f);
            StartCoroutine(PlayAudio());
        }
        else
        {
            StopCoroutine(PlayAudio());
            yield return null;
        }
    }
    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }
    private void OnClick(Button btn)
    {
        if(quizManager.GameStatus == GameStatus.Play)
        {
            if(!Answered)
            {
                Answered = true;
                bool val = quizManager.Answer(btn.name);

                if(val)
                {
                    btn.image.color = correctCol;
                }
                else
                {
                    btn.image.color = wrongCol;
                }
            }

        }

        switch (btn.name)
        {
            
            case "Play":
            quizManager.StartGame(0);
            mainMenuPanel.SetActive(false);
            gameMenuPanel.SetActive(true);
            break;
            case "Options":
            mainMenuPanel.SetActive(false);
            OptionPanel.SetActive(true);
            break;
            case "Exit":
            break;
        }
        
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReduceLife(int index)
    {
        lifeImageList[index].color = wrongCol;

    }

}
