using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;   // 新增：使用新版 Input System 一定要加這行  //此行為claude協助

public class GameManager : MonoBehaviour
{
    public GameObject ground;
    public GameObject player;
    public GameObject groundmaker;
    public GameObject gametitle;
    bool gameStarted = false;
    public AudioManager musicPlayer;
    float distance = 0f;
    public TextMeshProUGUI score;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI recentScores; //new
    float finalDistance = 0f;
    public PlayerMovement playerMovement;
    bool isGameOver = false;

    public GameObject pauseMenu;
    bool isPaused = false;
    
    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        musicPlayer.PlayTitle();
        GroundMove.speed = 5f;
    }

    void Update() 
    {
        if (gameStarted == false && Keyboard.current.enterKey.wasPressedThisFrame)  //此行為claude協助
        {
            StartGame();
        }
        if (gameStarted)
        {
            distance += GroundMove.speed * Time.deltaTime;
            score.text = $" {distance:F0} m"; //Debug.Log("已經跑了" + distance); F0代表零位小數
        }
        if (playerMovement.isDead && !isGameOver) //為什麼這邊不用標註isdead為"true"時才啟用 就可以成功執行?
        {
            GameOver();  
        }
        
        if(gameStarted && !isGameOver && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        ground.SetActive(true);
        player.SetActive(true);
        groundmaker.SetActive(true);
        gametitle.SetActive(false);
        musicPlayer.StopAudio();
        musicPlayer.PlayBgm();
        score.gameObject.SetActive(true);
    }

    void SaveRecentScore(int newScore)
    {
        PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2", -1));
        PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score1", -1));
        PlayerPrefs.SetInt("Score1", newScore );
        PlayerPrefs.Save();
    }

    void ShowRecentScores()
    {
        string text = "Recent 3\n";
        for (int i = 1; i <= 3; i++) //programmer的慣例：i 是 index（索引、第幾個） 或 iteration（第幾輪） 的縮寫，看到 for (int i ... 一眼就知道是「計數用的盒子」。
        {
            int s = PlayerPrefs.GetInt("Score" + i, -1);
            if (s >= 0)
                text += s + "m\n"; //把「分數 + 空格 + m + 換行」接到 text 後面。例如 s 是 120，就接上 120 m 再換行。
            else
                text += "--\n";
        }
        recentScores.text = text;
    }

    void GameOver()
    {
        isGameOver = true;
        finalDistance = distance;
        score.gameObject.SetActive(false);
        finalScore.gameObject.SetActive(true);
        finalScore.text = $"{finalDistance:F0} m";

        SaveRecentScore(Mathf.RoundToInt(finalDistance)); //一定要先SaveRecentScore，再ShowRecentScores，這樣顯示出來的才包含這次的分數。
        recentScores.gameObject.SetActive(true);
        ShowRecentScores();
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
