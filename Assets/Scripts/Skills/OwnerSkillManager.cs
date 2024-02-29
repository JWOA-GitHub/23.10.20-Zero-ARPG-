using System;
using System.Collections;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 角色技能管理器
    /// </summary>
    public class OwnerSkillManager : MonoBehaviour
    {
        // 技能列表
        public SkillData[] skills;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                InitSkill(skills[i]);
            }

        }

        /// <summary>   初始化技能预制件路径以及技能所属对象
        /// </summary>
        /// <param name="data"></param>
        private void InitSkill(SkillData data)
        {
            // 找到预制件 skillData.prefabName --> skillD   ata.skillPrefab
            // data.SkillPrefab = Resources.Load<GameObject>("Prefabs/Skills/" + data.PrefabName);
            // 获取预制件名称   通过资源管理器 加载到相对应的预制件路径
            data.SkillPrefab = ResourceManager.Load<GameObject>(data.PrefabName);
            Debug.Log($"<color=red> {data.SkillPrefab}   名字：  {data.PrefabName}  </color>");
            data.Owner = gameObject;
        }

        // 技能释放条件：冷却 能量值
        public SkillData PrepareSkill(int id)
        {
            // 根据id 查找技能数据
            SkillData data = Find(s => s.SkillID == id);
            // skills.Find(id);

            float mp = GetComponent<CharactersBase>().Mp;
            // 判断条件  (技能不为空 且 cd结束 且 当前MP大于所需MP) 返回技能数据
            if (data != null && data.CoolRemainTime <= 0 && data.CostMP <= mp)
            {
                return data;
            }
            else
                return null;
        }

        private SkillData Find(Func<SkillData, bool> handler)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (handler(skills[i]))
                    return skills[i];
            }
            return null;
        }
        

        // private SkillData Find(int id)
        // {
        //     for (int i = 0; i < skills.Length; i++)
        //     {
        //         if (skills[i].SkillID == id)
        //             return skills[i];
        //     }
        //     return null;
        // }

        /// <summary>   生成技能
        /// </summary>
        /// <param name="data"></param>
        public void GenerateSkill(SkillData data, Vector3 poisition = default, Quaternion rotation = default)
        {
            // 创建技能预制件
            // GameObject skillGo = Instantiate(data.SkillPrefab, transform.position, transform.rotation);

            Vector3 targetPos = default ? transform.position : poisition;
            Quaternion targetRotation = default ? transform.rotation : rotation;

            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.PrefabName, data.SkillPrefab, targetPos, targetRotation);

            // 传递技能数据
            SkillReleaser releaser = skillGo.GetComponent<SkillReleaser>();
            Debug.Log("       技能释放器  " + releaser.name);
            releaser.SkillData = data;  // 内部创建算法对象
            releaser.ReleaserSkill();    // 内部执行算法对象

            // 销毁技能
            // Destroy(skillGo, data.DurationTime);
            GameObjectPool.Instance.CollectObject(skillGo, data.DurationTime);

            // 开启技能CD
            StartCoroutine(CoolTimeDown(data));
        }

        // 技能冷却
        private IEnumerator CoolTimeDown(SkillData data)
        {
            // data.coolTime --> data.coolReamain
            data.CoolRemainTime = data.CoolTime;
            while (data.CoolRemainTime > 0)
            {
                yield return new WaitForSeconds(1);
                data.CoolRemainTime--;

            }
        }
    }
}
