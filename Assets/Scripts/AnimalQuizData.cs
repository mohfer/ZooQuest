using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    [TextArea(2, 5)]
    public string question;
    public string answerA;
    public string answerB;
    public string answerC;
    public string correctAnswer;
}

[CreateAssetMenu(fileName = "AnimalQuiz", menuName = "ZooQuest/Animal Quiz")]
public class AnimalQuizData : ScriptableObject
{
    public int levelID;
    public string levelName;
    public QuizQuestion[] questions;
}