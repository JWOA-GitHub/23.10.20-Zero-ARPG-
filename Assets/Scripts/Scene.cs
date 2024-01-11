using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // static Scene Instance;
    public GameObject gameOverPanel;
    private bool isEscapePressed = false;
    private bool isEscapeCoroutineRunning = false;
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

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverPanel)
            {
                // Debug.Log("         game  " + gameOverPanel.activeSelf + "      inhi         " + gameOverPanel.activeInHierarchy);
                gameOverPanel.SetActive(true);
                if (gameOverPanel.activeSelf)
                {
                    Debug.Log("         000   ");
                    Time.timeScale = 0;
                    Cursor.visible = true; // 显示鼠标光标
                    Cursor.lockState = CursorLockMode.None; // 解锁鼠标
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Z))
        {
            if (gameOverPanel)
            {
                Debug.Log("         game  " + gameOverPanel.activeSelf + "      inhi         " + gameOverPanel.activeInHierarchy);
                gameOverPanel.SetActive(false);
                if (!gameOverPanel.activeSelf)
                {
                    Debug.Log("         11111111111   ");
                    Time.timeScale = 1;
                    Cursor.visible = false; // 隐藏鼠标光标
                    Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标在屏幕中心
                }
            }
        }



        // 检查是否按下 Escape 键，并且上一帧未按下
        // if (Input.GetKeyDown(KeyCode.Escape) && !isEscapePressed)
        // {
        //     isEscapePressed = true; // 标记 Escape 键已按下

        //     // 判断当前页面是否已经打开
        //     bool isPanelActive = gameOverPanel.activeSelf;

        //     if (isPanelActive)
        //     {
        //         // 如果页面已经打开，关闭页面并恢复游戏状态
        //         gameOverPanel.SetActive(false);
        //         Time.timeScale = 1;
        //         Debug.Log("         111   ");
        //         Cursor.visible = false; // 隐藏鼠标光标
        //         Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标在屏幕中心
        //     }
        //     else
        //     {
        //         // 如果页面未打开，打开页面并暂停游戏
        //         gameOverPanel.SetActive(true);
        //         Time.timeScale = 0;
        //         Debug.Log("         000   ");
        //         Cursor.visible = true; // 显示鼠标光标
        //         Cursor.lockState = CursorLockMode.None; // 解锁鼠标
        //     }
        // }

        // // 检查是否释放了 Escape 键，并且当前为按下状态
        // if (Input.GetKeyUp(KeyCode.Escape) && isEscapePressed)
        // {
        //     isEscapePressed = false; // 标记 Escape 键已释放
        // }


        // if (Input.GetKeyDown(KeyCode.Escape) && !isEscapeCoroutineRunning)
        // {
        //     StartCoroutine(TogglePanelWithDelay());
        // }
    }

    IEnumerator TogglePanelWithDelay()
    {
        isEscapeCoroutineRunning = true;

        bool isPanelActive = gameOverPanel.activeSelf;

        if (isPanelActive)
        {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("         111   ");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("         000  ");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // 等待下一帧
        yield return new WaitForSeconds(5f);

        isEscapeCoroutineRunning = false;
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
