using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//测试用例
public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("背景音乐（开始界面）");
        //AudioManager.Instance.Play("22");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
