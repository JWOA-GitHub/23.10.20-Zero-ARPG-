using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class GUItest : MonoBehaviour
    {
        private Player player;
        private GameObject prefab;
        SkillData data;
        private void Start()
        {
            player = FindObjectOfType<Player>().GetComponent<Player>();
            Debug.Log("            " + player);
            // prefab = player.SkillManager.crea

        }

        //  *****************测试****************
        private void OnGUI()
        {
            Rect buttonRect = GUILayoutUtility.GetRect(new GUIContent("生成1"), "Button");

            buttonRect.width *= 1.5f;
            buttonRect.height *= 1.5f;

            // if (GUILayout.Button("生成1"))
            if (GUI.Button(buttonRect, "生成1"))
            {
                // GameObjectPool.Instance.CreateObject("skill");
                //技能准备（判断条件 能量 cd等）
                data = player.SkillManager.PrepareSkill(1001);

                if (data != null)
                {
                    // 创建物体 --> 设置位置/旋转 --> 重置（计算目标点）
                    player.SkillManager.GenerateSkill(data);
                    Debug.Log("生成技能 1001");
                }
            }

            if (GUILayout.Button("生成2"))
            {
                // GameObjectPool.Instance.CreateObject("skill");
                //技能准备（判断条件 能量 cd等）
                data = player.SkillManager.PrepareSkill(1002);

                if (data != null)
                {
                    player.SkillManager.GenerateSkill(data);
                    Debug.Log("生成技能 1002");
                }
            }

            if (GUILayout.Button("清空类别"))
            {
                GameObjectPool.Instance.Clear(data.Name);
            }

            if (GUILayout.Button("清空全部"))
            {
                GameObjectPool.Instance.ClearAll();
            }
        }
    }
}
