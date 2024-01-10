using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JWOAGameSystem
{

    public class LoadManager : MonoBehaviour
    {
        public static LoadManager instance;
        public GameObject loadScreen;
        public Slider slider;//进度条
        public Text text;//显示百分比
        public Animator animator;

        private int levelIndex = 1;
        public int Level { get => levelIndex; set => levelIndex = value; }

        [SerializeField] private GameObject startPanel;

        private void Awake()
        {
            instance = this;
        }
        public void LoadNextLevel()
        {
            StartCoroutine(Loadlevel(levelIndex));
        }
        public IEnumerator Loadlevel(int index)
        {
            startPanel  .SetActive(false);

            animator.SetBool("Fadein", true);
            animator.SetBool("Fadeout", false);
            yield return new WaitForSeconds(1);

            loadScreen.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            operation.allowSceneActivation = false;//场景加载完成不激活

            while (!operation.isDone)
            {
                float tempTime = Time.deltaTime / 3;
                slider.value += tempTime;
                text.text = slider.value * 100 + "%";


                if (slider.value >= 0.9f)
                {

                    slider.value = 1;
                    text.text = "按下任意按键继续";

                    if (Input.anyKeyDown)
                    {
                        loadScreen.SetActive(false);
                        operation.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }

        public void QuitGame()
        {
            UnityEngine.Application.Quit();
        }
    }

}
