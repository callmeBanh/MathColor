using UnityEngine;
using UnityEngine.EventSystems; // Bắt buộc phải có để kiểm tra UI

public class ColoringRegion : MonoBehaviour
{
    public string mathQuestion = "1 + 1 = ?";
    public int correctAnswer = 2;
    public Color colorWhenDone = Color.white;

    private SpriteRenderer sRenderer;
    private bool isSolved = false;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.isQuizOpen) return;

        // Nếu mảnh này chưa giải, thì mới mở câu đố
        if (!isSolved)
        {
            GameManager.Instance.OpenQuiz(this);
        }
    }

    public void ApplyColor()
    {
        isSolved = true;
        sRenderer.color = colorWhenDone;
    }
}