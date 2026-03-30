using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void StartGame()
    {
        // Bắt đầu từ con Mèo
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_Cat");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartGame");
    }

    public void NextLevel()
    {
        Time.timeScale = 1;

        // Lấy tên màn chơi vừa thắng từ PlayerPrefs (được lưu bởi GameManager)
        string lastLevel = PlayerPrefs.GetString("LastCompletedLevel", "Level_Cat");

        if (lastLevel == "Level_Cat") // Vừa thắng Mèo
        {
            SceneManager.LoadScene("Level_Giraffe");
        }
        else
        {
            SceneManager.LoadScene("StartGame");
        }
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        // Chơi lại đúng cái màn vừa bị thua
        string currentLevel = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentPlayingLevel", "Level_Cat"));
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Clicked");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}