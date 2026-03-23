using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [HideInInspector] // Ẩn trong Inspector vì chúng ta quản lý bằng code
    public bool isQuizOpen = false;

    [Header("UI Khung Chơi")]
    public GameObject quizPanel;
    public TextMeshProUGUI txtQuestion;
    public Button[] btnAnswers;

    [Header("Cấu hình Scene")]
    public string winSceneName = "Win";   
    public string loseSceneName = "Lose";

    private ColoringRegion currentRegion;
    private int totalParts;
    private int completedParts = 0;

    void Awake() { Instance = this; }

    void Start()
    {
        isQuizOpen = false;
        quizPanel.SetActive(false);
        // Tự động đếm tổng số mảnh tranh cần tô
        totalParts = FindObjectsOfType<ColoringRegion>().Length;
    }

    public void OpenQuiz(ColoringRegion region)
    {
        currentRegion = region;
        txtQuestion.text = region.mathQuestion;
        
        isQuizOpen = true; // Khóa tương tác với các mảnh tranh khác
        quizPanel.SetActive(true);
        SetupButtons(region.correctAnswer);
    }

    void SetupButtons(int correctAns)
    {
        int luckyBtn = Random.Range(0, btnAnswers.Length);
        
        for (int i = 0; i < btnAnswers.Length; i++)
        {
            int displayValue = (i == luckyBtn) ? correctAns : correctAns + Random.Range(-3, 4);
            if (displayValue == correctAns && i != luckyBtn) displayValue += 1;

            // Tìm component Text (cả TMP hoặc Legacy)
            var tmpText = btnAnswers[i].GetComponentInChildren<TextMeshProUGUI>();
            var oldText = btnAnswers[i].GetComponentInChildren<Text>();

            if (tmpText != null) tmpText.text = displayValue.ToString();
            else if (oldText != null) oldText.text = displayValue.ToString();
            
            int finalValue = displayValue;
            btnAnswers[i].onClick.RemoveAllListeners();
            btnAnswers[i].onClick.AddListener(() => OnClickAnswer(finalValue));
        }
    }

    void OnClickAnswer(int value)
    {
        Debug.Log($"Clicked value: {value}, correct: {currentRegion.correctAnswer}");

        // Luôn đóng panel và unlock (tránh kẹt ở true)
        quizPanel.SetActive(false);
        isQuizOpen = false;

        if (currentRegion == null)
        {
            Debug.LogWarning("currentRegion null khi click");
            return;
        }

        if (value == currentRegion.correctAnswer)
        {
            currentRegion.ApplyColor();
            completedParts++;
            CheckWin();
        }
        else
        {
            // Nếu chọn sai, reset biến và chuyển sang Scene Thua
            isQuizOpen = false;
            SceneManager.LoadScene(loseSceneName);
        }
    }

    void CheckWin()
    {
        if (completedParts >= totalParts)
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}