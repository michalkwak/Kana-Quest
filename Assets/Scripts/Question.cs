using System.Collections.Generic;

[System.Serializable]
public class Question
{

    public string question;
    public int correctAnswerIndex;
    public string[] answers;
    
}

[System.Serializable]
public class KatakanaQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
}

