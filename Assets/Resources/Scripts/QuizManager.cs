using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class QuizManager : MonoBehaviour
{
    [Header("Veri Seti (Model)")]
    public List<Question> questions;
    private List<Question> unansweredQuestions;
    private Question currentQuestion;

    [Header("Arayüz (View) Baðlantýlarý")]
    public GameObject quizCanvas; // Sahnede hazýr duran Canvas'ý buraya baðlayacaðýz
    public TextMeshProUGUI questionTextUI;
    public TextMeshProUGUI scoreTextUI;
    public GameObject[] answerButtons;

    [Header("AR Entegrasyonu")]
    public ARTrackedImageManager imageManager;
    private bool isQuizActive = false;

    [Header("Oyun Deðerleri")]
    private int score = 0;

    void Start()
    {
        unansweredQuestions = new List<Question>(questions);
        if (quizCanvas != null) quizCanvas.SetActive(false); // Oyun baþlarken Canvas gizli baþlasýn
    }

    void OnEnable()
    {
        if (imageManager != null) imageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        if (imageManager != null) imageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (isQuizActive) return;

        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "QuizTrigger") ActivateQuiz();
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.referenceImage.name == "QuizTrigger" && trackedImage.trackingState == TrackingState.Tracking) ActivateQuiz();
        }
    }

    void ActivateQuiz()
    {
        isQuizActive = true;
        if (quizCanvas != null) quizCanvas.SetActive(true); // Resim bulundu, Canvas'ý aç!
        UpdateScoreUI();
        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        if (unansweredQuestions.Count == 0)
        {
            questionTextUI.text = "Tebrikler!\nYarýþmayý Tamamladýn.\nSkorun: " + score;
            foreach (GameObject btn in answerButtons) btn.SetActive(false);
            return;
        }

        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];

        questionTextUI.text = currentQuestion.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
        }

        unansweredQuestions.RemoveAt(randomIndex);
    }

    public void UserSelectAnswer(int buttonIndex)
    {
        StartCoroutine(ProcessAnswer(buttonIndex));
    }

    IEnumerator ProcessAnswer(int buttonIndex)
    {
        // Butonlarý geçici kilitle
        foreach (GameObject btn in answerButtons) btn.GetComponent<Button>().interactable = false;

        Image clickedButtonImage = answerButtons[buttonIndex].GetComponent<Image>();

        // Puan ekleme kontrolü tam olarak burada çalýþýyor!
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            score += 10;
            UpdateScoreUI();
            if (clickedButtonImage != null) clickedButtonImage.color = Color.green;
        }
        else
        {
            if (clickedButtonImage != null) clickedButtonImage.color = Color.red;
        }

        yield return new WaitForSeconds(1.2f);

        // Renkleri ve kilitleri sýfýrla
        foreach (GameObject btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.GetComponent<Button>().interactable = true;
        }

        SetCurrentQuestion();
    }

    void UpdateScoreUI()
    {
        scoreTextUI.text = "Skor: " + score;
    }
}