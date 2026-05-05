using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation; // AR kütüphaneleri
using UnityEngine.XR.ARSubsystems;

public class QuizManager : MonoBehaviour
{
    [Header("Veri Seti (Model)")]
    public List<Question> questions;
    private List<Question> unansweredQuestions;
    private Question currentQuestion;

    [Header("Arayüz (View) Baðlantýlarý")]
    public GameObject quizCanvas; // Gizleyip/Göstermek için tüm Canvas
    public TextMeshProUGUI questionTextUI;
    public TextMeshProUGUI scoreTextUI;
    public GameObject[] answerButtons;

    [Header("AR Entegrasyonu")]
    public ARTrackedImageManager imageManager; // Kamerayý dinleyecek yönetici
    private bool isQuizActive = false; // Quiz bir kere baþladýysa tekrar tetiklenmesin

    [Header("Oyun Deðerleri")]
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Uygulama ilk açýldýðýnda sorularý kopyala ama Canvas'ý gizle
        unansweredQuestions = new List<Question>(questions);
        quizCanvas.SetActive(false);
    }

    // --- AR TETÝKLEYÝCÝ SÝSTEMÝ ---

    void OnEnable()
    {
        // Script aktifleþtiðinde AR olaylarýný dinlemeye baþla
        if (imageManager != null)
            imageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        // Memory leak (bellek sýzýntýsý) olmamasý için aboneliði iptal et
        if (imageManager != null)
            imageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (isQuizActive) return; // Quiz zaten baþladýysa kamerayý yorma

        // Yeni bir görüntü algýlandýðýnda veya güncellendiðinde
        foreach (var trackedImage in eventArgs.added)
        {
            // "QuizTrigger" ismini, oluþturduðun Reference Image Library'deki isimle ayný yap!
            if (trackedImage.referenceImage.name == "QuizTrigger")
            {
                ActivateQuiz();
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.referenceImage.name == "QuizTrigger" && trackedImage.trackingState == TrackingState.Tracking)
            {
                ActivateQuiz();
            }
        }
    }

    // --- BÝLGÝ YARIÞMASI MANTIÐI ---

    void ActivateQuiz()
    {
        isQuizActive = true;
        quizCanvas.SetActive(true); // AR görüntüyü buldu, Canvas'ý göster!
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

        // Rastgele soru seç
        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];

        // Soruyu ve þýklarý UI'a yazdýr
        questionTextUI.text = currentQuestion.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
        }

        // Soruyu bir daha sormamak için listeden çýkar
        unansweredQuestions.RemoveAt(randomIndex);
    }

    // Butonlarýn "On Click" kýsmýna baðlanacak fonksiyon
    public void UserSelectAnswer(int buttonIndex)
    {
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            score += 10;
            UpdateScoreUI();
        }

        SetCurrentQuestion(); // Diðer soruya geç
    }

    void UpdateScoreUI()
    {
        scoreTextUI.text = "Skor: " + score;
    }
}

    //// Update is called once per frame
    //void Update()
    //    {
        
    //    }
    //}
