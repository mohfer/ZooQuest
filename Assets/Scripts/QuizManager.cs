using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    public GameObject quizPanel;
    public TMP_Text questionText;

    public Button answerA;
    public Button answerB;
    public Button answerC;

    private string correctAnswer;
    private Interactable currentInteractable; // referensi ke Interactable yang aktif

    private void Awake()
    {
        Instance = this;
        quizPanel.SetActive(false);
    }

    public void ShowQuiz(string question, string optA, string optB, string optC, string correct, Interactable interactable)
    {
        quizPanel.SetActive(true);
        
        questionText.text = question;
        answerA.GetComponentInChildren<TMP_Text>().text = optA;
        answerB.GetComponentInChildren<TMP_Text>().text = optB;
        answerC.GetComponentInChildren<TMP_Text>().text = optC;

        correctAnswer = correct;
        currentInteractable = interactable; // simpan referensi
    }

    public void CheckAnswer(string selectedAnswer)
    {
        bool isCorrect = (selectedAnswer == correctAnswer);

        // Tidak tampilkan log benar/salah di sini, tunggu sampai akhir
        
        quizPanel.SetActive(false);

        // Beritahu Interactable bahwa user sudah jawab
        if (currentInteractable != null)
        {
            currentInteractable.OnAnswered(isCorrect);
        }
    }
}