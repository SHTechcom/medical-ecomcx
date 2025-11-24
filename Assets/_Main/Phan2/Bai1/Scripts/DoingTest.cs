using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Localization;
public class DoingTest : MonoBehaviour
{
    [Header("Tên StringTable cho câu hỏi")]
    [SerializeField] private string tableName = "Table"; // đúng với tên bạn tạo
    [Header("Giới tính hiện tại (Male/Female)")]
    public bool isMale = true;   // gán bằng button Nam/Nữ

    [Header("Data câu hỏi (mỗi bên 5 câu)")]
    public QuestionData[] maleQuestions;     // size = 5
    public QuestionData[] femaleQuestions;   // size = 5

    [Header("UI câu hỏi")]
    public TMP_Text questionText;

    [Tooltip("5 TMP_Text: 4 đáp án + 1 ô dòng thứ 5 (cũng dùng khi tổng kết)")]
    public TMP_Text[] answerTexts;   // size = 5

    [Tooltip("5 Toggle: 4 đáp án + 1 toggle dòng thứ 5 (cũng dùng khi tổng kết)")]
    public Toggle[] answerToggles;   // size = 5

    [Header("ToggleGroup cho 4 đáp án (0–3)")]
    public ToggleGroup answerGroup;  // tạo 1 ToggleGroup, kéo vào đây

    [Header("Icon kết quả đúng/sai")]
    public Sprite iconTrue;   // sprite tích xanh
    public Sprite iconWrong;  // sprite tích đỏ

    [Header("Nút & Text khác")]
    public Button checkButton;    // dùng như nút NEXT
    public Button testButton;
    public TMP_Text finalScoreText;  // hiển thị tổng 5 câu khi tổng kết

    // ====== STATE ======
    private QuestionData[] currentQuestions; // đang dùng bộ male/female
    private int currentIndex = 0;            // câu hiện tại (0–4)
    private int correctCount = 0;

    // Lưu đáp án đã chọn & đúng/sai cho từng câu để tổng kết
    private int[] chosenIndexes;     // -1 nếu chưa chọn
    private bool[] isCorrectArray;

    private const int ANSWERS_PER_QUESTION = 4; // 4 đáp án

    // Lưu sprite gốc của Toggle để reset nếu cần
    private Sprite[] defaultToggleSprites;

    private void Awake()
    {
        // Lưu sprite gốc mỗi toggle (Image trên chính Toggle)
        if (answerToggles != null && answerToggles.Length > 0)
        {
            defaultToggleSprites = new Sprite[answerToggles.Length];
            for (int i = 0; i < answerToggles.Length; i++)
            {
                Image img = answerToggles[i].GetComponent<Image>();
                defaultToggleSprites[i] = img != null ? img.sprite : null;
            }
        }
    }

    private void Start()
    {
        // reset tổng kết
        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(false);
            finalScoreText.text = "";
        }

        // Gán sự kiện nút
        if (checkButton != null)
            checkButton.onClick.AddListener(OnClickNextQuestion); // DÙNG LÀM NÚT NEXT

        if (testButton != null)
            testButton.onClick.AddListener(OnClickStartTest);

        // Cấu hình ToggleGroup
        if (answerGroup != null && answerToggles != null)
        {
            answerGroup.allowSwitchOff = true; // lúc đầu chưa chọn gì cũng được
            // Gán 4 toggle đáp án vào group
            int maxAns = Mathf.Min(ANSWERS_PER_QUESTION, answerToggles.Length);
            for (int i = 0; i < maxAns; i++)
            {
                if (answerToggles[i] != null)
                    answerToggles[i].group = answerGroup;
            }
        }

        // Ẩn hết toggle lúc đầu
        if (answerToggles != null && answerTexts != null)
        {
            int max = Mathf.Min(answerToggles.Length, answerTexts.Length);
            for (int i = 0; i < max; i++)
            {
                answerToggles[i].gameObject.SetActive(false);
                answerTexts[i].gameObject.SetActive(false);
            }
        }
    }

    // ====== Button chọn giới tính ======
    public void SetGender(bool male)
    {
        isMale = male;
    }

    // ====== Bắt đầu làm test (5 câu) – gọi bằng nút Test ======
    public void OnClickStartTest()
    {
        currentQuestions = isMale ? maleQuestions : femaleQuestions;

        if (currentQuestions == null || currentQuestions.Length == 0)
        {
            Debug.LogWarning("Chưa gán câu hỏi cho " + (isMale ? "maleQuestions" : "femaleQuestions"));
            return;
        }

        int n = currentQuestions.Length; // giả định 5 câu

        currentIndex = 0;
        correctCount = 0;

        chosenIndexes = new int[n];
        isCorrectArray = new bool[n];
        for (int i = 0; i < n; i++)
        {
            chosenIndexes[i] = -1;
            isCorrectArray[i] = false;
        }

        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(false);
            finalScoreText.text = "";
        }

        ShowCurrentQuestion();
    }

    // ====== Hiển thị câu hiện tại ======
    private void ShowCurrentQuestion()
    {
        if (currentQuestions == null)
            return;

        if (currentIndex >= currentQuestions.Length)
        {
            ShowFinalSummary();
            return;
        }

        var q = currentQuestions[currentIndex];
        LocalizedString locQuestion = new LocalizedString(tableName, q.questionKey);
        locQuestion.StringChanged += (value) =>
        {
            questionText.text = value;   // chỉ đổi text, không đổi logic
        };

        if (answerToggles == null || answerTexts == null)
            return;

        int max = Mathf.Min(answerToggles.Length, answerTexts.Length);

        for (int i = 0; i < max; i++)
        {
            bool isAnswer = (i < ANSWERS_PER_QUESTION); // 0..3 là đáp án, 4 là ẩn khi làm bài

            if (isAnswer)
            {
                // Bật toggle + text
                answerToggles[i].gameObject.SetActive(true);
                answerTexts[i].gameObject.SetActive(true);

                // Gán vào group để chỉ chọn được 1
                if (answerGroup != null)
                    answerToggles[i].group = answerGroup;

                // Gán text đáp án
                if (q.answers != null && i < q.answers.Length)
                {
                    LocalizedString locAnswer = new LocalizedString(tableName, q.answerKeys[i]);
                    int index = i;
                    locAnswer.StringChanged += (value) =>
                    {
                        answerTexts[index].text = value;   // chỉ đổi text
                    };
                }    
                else
                    answerTexts[i].text = "";
            }
            else
            {
                // Toggle thứ 5 (index 4) ẩn lúc làm bài
                answerToggles[i].gameObject.SetActive(false);
                answerTexts[i].gameObject.SetActive(false);

                // Không thuộc group
                answerToggles[i].group = null;
            }

            // Reset trạng thái toggle
            answerToggles[i].isOn = false;
            answerToggles[i].interactable = isAnswer;
        }

        // Reset màu chữ của tất cả dòng (chỉ để hiển thị bình thường, không báo đúng/sai)
        for (int i = 0; i < max; i++)
        {
            answerTexts[i].color = Color.white;
        }

        // Reset sprite toggle về default (phòng trường hợp chơi lại)
        if (defaultToggleSprites != null)
        {
            for (int i = 0; i < max && i < defaultToggleSprites.Length; i++)
            {
                Image img = answerToggles[i].GetComponent<Image>();
                if (img != null && defaultToggleSprites[i] != null)
                    img.sprite = defaultToggleSprites[i];
            }
        }

        if (checkButton != null)
        {
            checkButton.interactable = true;
            checkButton.gameObject.SetActive(true);
        }
    }

    // ====== Bấm NEXT để lưu câu hiện tại và sang câu tiếp ======
    private void OnClickNextQuestion()
    {
        if (currentQuestions == null || answerToggles == null)
            return;

        int selectedIndex = GetSelectedAnswerIndex();

        // Nếu muốn bắt buộc phải chọn mới cho sang câu, giữ đoạn này
        if (selectedIndex == -1)
        {
            Debug.Log("Chưa chọn đáp án, không thể sang câu tiếp theo.");
            return;
        }

        var q = currentQuestions[currentIndex];

        bool isCorrect = (selectedIndex == q.correctIndex);

        // Lưu kết quả
        if (chosenIndexes != null && currentIndex < chosenIndexes.Length)
            chosenIndexes[currentIndex] = selectedIndex;

        if (isCorrectArray != null && currentIndex < isCorrectArray.Length)
            isCorrectArray[currentIndex] = isCorrect;

        if (isCorrect)
        {
            // Chỉ cộng điểm, KHÔNG đổi màu, KHÔNG hiện đúng/sai lúc đang làm
            correctCount++;
        }

        // Sang câu tiếp theo
        currentIndex++;

        if (currentIndex >= currentQuestions.Length)
        {
            // Hết câu -> tổng kết
            ShowFinalSummary();
        }
        else
        {
            ShowCurrentQuestion();
        }
    }

    // ====== Lấy index đáp án được chọn (0..3) ======
    private int GetSelectedAnswerIndex()
    {
        if (answerToggles == null)
            return -1;

        int maxAns = Mathf.Min(ANSWERS_PER_QUESTION, answerToggles.Length);
        for (int i = 0; i < maxAns; i++)
        {
            if (answerToggles[i].isOn)
                return i;
        }
        return -1;
    }

    // ====== Tổng kết sau khi làm hết ======
    // ====== Tổng kết sau khi làm hết ======
    private void ShowFinalSummary()
    {
        // Đảm bảo mảng không null
        if (currentQuestions == null || chosenIndexes == null || answerToggles == null || answerTexts == null)
            return;

        int total = currentQuestions.Length;
        int wrongCount = total - correctCount;

        // Tiêu đề
        LocalizedString locSummary = new LocalizedString(tableName, "SummaryTitle");
        locSummary.StringChanged += (value) =>
        {
            questionText.text = value;   // VI = Tổng kết, EN = Final
        };


        // Ẩn nút Next/Check
        if (checkButton != null)
            checkButton.gameObject.SetActive(false);

        // Số toggle/text thực sự dùng để hiển thị câu hỏi
        int maxUI = Mathf.Min(total, answerToggles.Length, answerTexts.Length);

        // Hiển thị từng câu với toggle tương ứng
        for (int i = 0; i < maxUI; i++)
        {
            Toggle toggle = answerToggles[i];
            TMP_Text text = answerTexts[i];

            toggle.gameObject.SetActive(true);
            text.gameObject.SetActive(true);

            // Không cho bấm nữa, bỏ group
            toggle.interactable = false;
            toggle.group = null;
            toggle.isOn = false;

            bool corr = (isCorrectArray != null && i < isCorrectArray.Length && isCorrectArray[i]);
            int chosen = (chosenIndexes != null && i < chosenIndexes.Length) ? chosenIndexes[i] : -1;

            // Nếu không chọn
            if (chosen == -1)
            {
                // Localize "Not Chosen"
                LocalizedString locNotChosen = new LocalizedString(tableName, "NotChosen");
                locNotChosen.StringChanged += (value) =>
                {
                    text.text = $"{i + 1}. {value}"; // ví dụ EN: "Not selected", VI: "Không chọn"
                };
            }
            else
            {
                // Localize đáp án đã chọn
                LocalizedString locChosen = new LocalizedString(tableName, currentQuestions[i].answerKeys[chosen]);
                locChosen.StringChanged += (value) =>
                {
                    text.text = $"{i + 1}. {value}";   // Ví dụ EN answer / VI answer
                };
            }
            //text.color = corr ? Color.green : Color.red;

            // Đổi sprite của toggle theo đúng/sai
            Image img = toggle.transform.Find("Background").GetComponent<Image>();
            if (img != null)
            {
                if (corr)
                {
                    if (iconTrue != null)
                        img.sprite = iconTrue;
                    img.color = new Color(0.2f, 1f, 0.2f);   // xanh
                }
                else
                {
                    if (iconWrong != null)
                        img.sprite = iconWrong;
                    img.color = new Color(1f, 0.3f, 0.3f);   // đỏ
                }
            }

        }

        // Ẩn các toggle/text dư (nếu có)
        int maxAll = Mathf.Min(answerToggles.Length, answerTexts.Length);
        for (int i = maxUI; i < maxAll; i++)
        {
            answerToggles[i].gameObject.SetActive(false);
            answerTexts[i].gameObject.SetActive(false);
        }

        // Text tổng kết chung
        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text =
                $"{correctCount}/" +$"{total}";
        }
    }


    // Reset khi tắt panel
    private void OnDisable()
    {
        currentQuestions = null;
        currentIndex = 0;
        correctCount = 0;
        chosenIndexes = null;
        isCorrectArray = null;

        if (questionText != null)
            questionText.text = "";

        if (finalScoreText != null)
        {
            finalScoreText.text = "";
            finalScoreText.gameObject.SetActive(false);
        }

        if (answerToggles != null && answerTexts != null)
        {
            int max = Mathf.Min(answerToggles.Length, answerTexts.Length);
            for (int i = 0; i < max; i++)
            {
                answerToggles[i].isOn = false;
                answerToggles[i].gameObject.SetActive(false);
                answerTexts[i].gameObject.SetActive(false);
            }
        }
    }
}
