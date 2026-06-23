using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject prompt;

    [Header("Konten Info Hewan (untuk papan info)")]
    public string title;
    [TextArea(3, 10)]
    public string information;
    public Sprite animalImage;

    [Header("Quiz (untuk Game Master di Lobby)")]
    public bool isQuiz;
    public AnimalQuizData[] allQuizSets; // GANTI KE AnimalQuizData

    private bool playerInRange;
    private bool isOpen;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    private AnimalQuizData currentQuizSet; // GANTI

    private void Start()
    {
        prompt.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                Close();
                return;
            }

            if (isQuiz)
            {
                if (currentQuestionIndex == 0)
                {
                    LoadCurrentQuizSet();
                    isOpen = true;
                    ShowNextQuestion();
                }
            }
            else
            {
                DialogueManager.Instance.Show(title, information, animalImage);
                isOpen = true;
            }
        }
    }

    private void Close()
    {
        if (isQuiz)
        {
            if (QuizManager.Instance != null && QuizManager.Instance.quizPanel != null)
                QuizManager.Instance.quizPanel.SetActive(false);
            ResetQuiz();
        }
        else
        {
            DialogueManager.Instance.Close();
        }
        isOpen = false;
    }

    private void LoadCurrentQuizSet()
    {
        if (allQuizSets == null || allQuizSets.Length == 0)
        {
            Debug.LogError("All Quiz Sets belum di-assign di Inspector!");
            return;
        }

        int activeLevel = GameProgressManager.Instance.currentActiveLevel;
        
        foreach (AnimalQuizData quizSet in allQuizSets)
        {
            if (quizSet == null)
            {
                Debug.LogWarning("Ada Quiz Set yang null di array!");
                continue;
            }

            if (quizSet.levelID == activeLevel)
            {
                currentQuizSet = quizSet;
                Debug.Log($"Quiz loaded: {quizSet.levelName}");
                return;
            }
        }

        Debug.LogError($"Quiz set untuk level {activeLevel} tidak ditemukan!");
    }

    public void ShowNextQuestion()
    {
        if (currentQuizSet == null || currentQuestionIndex >= currentQuizSet.questions.Length)
        {
            ShowQuizResults();
            return;
        }

        if (QuizManager.Instance == null) return;

        QuizQuestion q = currentQuizSet.questions[currentQuestionIndex]; // GANTI
        QuizManager.Instance.ShowQuiz(
            q.question,
            q.answerA,
            q.answerB,
            q.answerC,
            q.correctAnswer,
            this
        );
    }

    public void OnAnswered(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswers++;
        }
        else
        {
            wrongAnswers++;
        }

        currentQuestionIndex++;
        ShowNextQuestion();
    }

    private void ShowQuizResults()
    {
        int total = correctAnswers + wrongAnswers;

        if (total == 0)
        {
            Debug.LogWarning("Tidak ada pertanyaan yang dijawab!");
            ResetQuiz();
            return;
        }

        int percentage = (correctAnswers * 100 / total);
        string levelName = GameProgressManager.Instance.GetCurrentLevelName();

        Debug.Log($"=== HASIL QUIZ {levelName} ===");
        Debug.Log($"Total Pertanyaan: {total}");
        Debug.Log($"Benar: {correctAnswers}");
        Debug.Log($"Salah: {wrongAnswers}");
        Debug.Log($"Nilai: {percentage}%");

        if (percentage == 100)
        {
            string nextLevelName = GetNextLevelName();
            
            if (NotificationManager.Instance != null)
            {
                NotificationManager.Instance.ShowCorrectNotification(
                    $"Quiz selesai! {nextLevelName} telah terbuka!"
                );
            }
            
            GameProgressManager.Instance.CompleteCurrentLevel();
            
            // Update guide text
            if (GuideManager.Instance != null)
            {
                GuideManager.Instance.UpdateGuideText();
            }
        }
        else
        {
            if (NotificationManager.Instance != null)
            {
                NotificationManager.Instance.ShowWrongNotification(
                    $"Nilai {percentage}%. Kumpulkan semua jawaban dengan benar!"
                );
            }
            
            Debug.Log($"Nilai kurang dari 100%. Coba lagi!");
        }

        ResetQuiz();
    }

    private string GetNextLevelName()
    {
        int currentLevel = GameProgressManager.Instance.currentActiveLevel;
        
        switch (currentLevel)
        {
            case 1:
                return "Iced";
            case 2:
                return "Forest";
            case 3:
                return "Semua Level";
            default:
                return "Level Berikutnya";
        }
    }

    private void ResetQuiz()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        wrongAnswers = 0;
        currentQuizSet = null;
        isOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            prompt.SetActive(false);
            if (isOpen) Close();
        }
    }
}