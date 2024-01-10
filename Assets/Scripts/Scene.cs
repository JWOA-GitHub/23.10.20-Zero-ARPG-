using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // static Scene Instance;
    public GameObject gameOverPanel;
    // private void Awake() 
    // {
    //     Debug.Log("场景"+SceneManager.GetActiveScene().name);
    //     if( Instance == null)
    //         Instance = this;
    //     else
    //         if(Instance != this)
    //             Destroy(gameObject);
    //     DontDestroyOnLoad(gameObject);
    // }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverPanel)
            {
                gameOverPanel.SetActive(!gameOverPanel.activeInHierarchy);
                if (gameOverPanel.activeInHierarchy)
                {
                    Time.timeScale = 0;
                    Cursor.visible = true; // 显示鼠标光标
                    Cursor.lockState = CursorLockMode.None; // 解锁鼠标
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.visible = false; // 隐藏鼠标光标
                    Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标在屏幕中心
                }
            }
        }
    }

    public void Restart()
    {
        transform.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);

        Time.timeScale = 0;
        Cursor.visible = true; // 显示鼠标光标
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
        if (gameOverPanel)
            gameOverPanel.SetActive(false);
    }

    public void StartScene()
    {
        SceneManager.LoadScene(0);
        if (gameOverPanel)
            gameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

}
