using UnityEngine;

public class ColoringRegion : MonoBehaviour
{
    public string mathQuestion = "2 + 3 = ?";
    public int correctAnswer = 5;
    public Color colorWhenDone = Color.green;

    private SpriteRenderer sRenderer;
    private bool isSolved = false;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        Debug.Log("Bạn vừa click vào: " + gameObject.name);
        // Chỉ mở câu hỏi nếu chưa giải xong
        if (!isSolved)
        {
            GameManager.Instance.OpenQuiz(this);
        }
    }

    public void ApplyColor()
    {
        isSolved = true;
        sRenderer.color = colorWhenDone;
        // Có thể thêm hiệu ứng Particle (pháo hoa nhỏ) tại vị trí mảnh ghép ở đây
    }

}