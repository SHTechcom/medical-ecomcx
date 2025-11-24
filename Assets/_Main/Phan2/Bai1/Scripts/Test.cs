using UnityEngine;

[System.Serializable]
public class QuestionData
{
    [Header("---- DÙNG LOCALIZATION ----")]
    public string questionKey;           // key trong StringTable cho câu hỏi
    public string[] answerKeys = new string[4]; // 4 key cho 4 đáp án

    [Header("---- TEXT THƯỜNG (cũ) – có thể bỏ qua ----")]
    [TextArea] public string question;
    public string[] answers = new string[4];

    [Range(0, 3)] public int correctIndex;
}
