using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        quizPanel.SetActive(false);
        totalParts = FindObjectsOfType<ColoringRegion>().Length;
    }

    public void OpenQuiz(ColoringRegion region)
    {
        currentRegion = region;
        txtQuestion.text = region.mathQuestion;
        quizPanel.SetActive(true);
        SetupButtons(region.correctAnswer);
    }

    void SetupButtons(int correctAns)
    {
        int luckyBtn = Random.Range(0, btnAnswers.Length);
        for (int i = 0; i < btnAnswers.Length; i++)
        {
            // Tạo đáp án gây nhiễu đơn giản
            int displayValue = (i == luckyBtn) ? correctAns : correctAns + Random.Range(-3, 4);
            if (displayValue == correctAns && i != luckyBtn) displayValue += 1;

            // Hỗ trợ cả UI.Text và TextMeshProUGUI
            TextMeshProUGUI tmpText = btnAnswers[i].GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = displayValue.ToString();
            }
            else
            {
                Text oldText = btnAnswers[i].GetComponentInChildren<Text>();
                if (oldText != null)
                    oldText.text = displayValue.ToString();
                else
                    Debug.LogError("Button " + i + " không có Text hoặc TextMeshProUGUI component!");
            }
            
            int finalValue = displayValue;
            btnAnswers[i].onClick.RemoveAllListeners();
            btnAnswers[i].onClick.AddListener(() => OnClickAnswer(finalValue));
        }
    }

    void OnClickAnswer(int value)
    {
        if (value == currentRegion.correctAnswer)
        {
            currentRegion.ApplyColor();
            quizPanel.SetActive(false);
            completedParts++;
            CheckWin();
        }
        else
        {
            // Khi chọn sai, chuyển ngay sang Scene Thua
            SceneManager.LoadScene(loseSceneName);
        }
    }

    void CheckWin()
    {
        if (completedParts >= totalParts)
        {
            // Khi hoàn thành tất cả, chuyển sang Scene Thắng
            SceneManager.LoadScene(winSceneName);
        }
    }
}