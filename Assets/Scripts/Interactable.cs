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
            if (isQuiz)
            {
                if (currentQuestionIndex == 0)
                {
                    LoadCurrentQuizSet();
                    ShowNextQuestion();
                }
            }
            else
            {
                DialogueManager.Instance.Show(title, information, animalImage);
            }
        }
    }

    private void LoadCurrentQuizSet()
    {
        if (allQuizSets == null || allQuizSets.Length == 0)
        {
            Debug.LogError("All Quiz Sets belum di-assign di Inspector!");
            return;
        }

        int activeLevel = GameProgressManager.Instance.currentActiveLevel;
        
        foreach (AnimalQuizData quizSet in allQuizSets) // GANTI
        {
            if (quizSet == null)
            {
                Debug.LogWarning("Ada Quiz Set yang null di array!");
                continue;
            }

            if (quizSet.levelID == activeLevel)
            {
                currentQuizSet = quizSet;
                Debug.Log($"📝 Quiz loaded: {quizSet.levelName}");
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
            Debug.LogWarning("⚠️ Tidak ada pertanyaan yang dijawab!");
            ResetQuiz();
            return;
        }

        int percentage = (correctAnswers * 100 / total);
        string levelName = GameProgressManager.Instance.GetCurrentLevelName();

        Debug.Log($"=== HASIL QUIZ {levelName} ===");
        Debug.Log($"Total Pertanyaan: {total}");
        Debug.Log($"✅ Benar: {correctAnswers}");
        Debug.Log($"❌ Salah: {wrongAnswers}");
        Debug.Log($"Nilai: {percentage}%");

        if (percentage == 100)
        {
            GameProgressManager.Instance.CompleteCurrentLevel();
        }
        else
        {
            Debug.Log($"Nilai kurang dari 100%. Coba lagi!");
        }

        ResetQuiz();
    }

    private void ResetQuiz()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        wrongAnswers = 0;
        currentQuizSet = null;
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
        }
    }
}