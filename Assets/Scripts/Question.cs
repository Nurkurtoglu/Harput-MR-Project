using UnityEngine;

// Bu etiket Unity'nin bu sýnýfý Inspector'da açýlýp kapanan bir form gibi göstermesini saðlar.
[System.Serializable]
public class Question
{
    [TextArea(3, 5)]
    public string questionText;

    public string[] answers = new string[4];

    public int correctAnswerIndex;
}