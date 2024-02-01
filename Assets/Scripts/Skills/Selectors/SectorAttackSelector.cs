using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary> 扇形/圆形选区
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">技能数据</param>
        /// <param name="skillTF">技能数据的Transform位置</param>
        /// <returns></returns>
        public Transform[] SelectTarget(SkillData data, Transform skillTF)
        {
            // 根据技能数据中的标签 获取所有目标
            // data.attackTargetTags
            // string[] --> Transform[]

            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < data.AttackTargetTags.Length; i++)
            {
                GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(data.AttackTargetTags[i]);
                // GameObject[] ---> Transform[]
                targets.AddRange(tempGOArray.Select(g => g.transform));
            }
            // 判断攻击范围(扇形/圆形)  t为列表中增加的Transform
            targets = targets.FindAll(t =>
                 Vector3.Distance(t.position, skillTF.position) <= data.AttackDistance &&
                 Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.AttackAngle / 2);

            // 筛选出活的角色
            targets = targets.FindAll(t => t.GetComponent<CharactersBase>().Hp > 0);

            //返回目标（单攻/群攻
            // data.AttackType
            // 群攻则返回所有
            Transform[] result = targets.ToArray();
            // MARKER: 当范围内无目标时 则无需查找第一个元素！直接返回！
            if (data.AttackType == SkillAttackType.Group || result.Length == 0)
                return result;

            // 距离最近（小）的敌人
            // Transform min = result.GetMin(t => Vector3.Distance(t.position, skillTF.position));
            Transform min = FindCloseTransform(result, skillTF);
            // if (data.AttackType == SkillAttackType.Single)
            return new Transform[] { min };
        }

        Transform FindCloseTransform(Transform[] result, Transform skillTF)
        {
            Transform min = result[0];
            // float minDistance = Vector3.Distance(result[0].position, skillTF.position);
            // foreach (var item in result)
            // {
            //     float distance = Vector3.Distance(item.position, skillTF.position);
            //     if (distance < minDistance)
            //     {
            //         min = item;
            //         minDistance = distance;
            //     }
            // }

            // 使用LinQ筛选出距离最近的Transform  
            // MARKER： OrderBy 默认按照升序（从小到大）排序的  降序（从大到小）排序，则使用 OrderByDescending 方法。
            min = result.OrderBy(t => Vector3.Distance(skillTF.position, t.position)).FirstOrDefault();
            // FirstOrDefault 获取第一个元素，需要确保目标数组不为空！

            return min;
        }
    }
}
