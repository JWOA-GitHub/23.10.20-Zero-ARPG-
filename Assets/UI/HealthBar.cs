using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JWOAGameSystem
{
    public class HealthBar : MonoBehaviour
    {
        public Image healthPointImage;
        public Image healthPointEffect;
        [SerializeField, Header("协程每次递减的速度")] private float hurtBufferSpeed = 0.003f;
        [SerializeField, Header("协程递减的时间间隔")] private float reduceSpacing = 0.005f;

        public Text healthPointText;

        // public Image HealthPointImage
        // {
        //     get => healthPointImage;
        //     set
        //     {
        //         healthPointImage.fillAmount = playerCharacterBase.Hp / playerCharacterBase.maxHp;
        //         healthPointImage = value;
        //     }
        // }

        private CharactersBase playerCharacterBase;


        private void Awake()
        {
            playerCharacterBase = GameObject.FindGameObjectWithTag("Player").GetComponent<CharactersBase>();
            healthPointText.text = $"{playerCharacterBase.Hp}/{playerCharacterBase.maxHp}";
        }

        // private void Update()
        // {
        //     healthPointImage.fillAmount = playerCharacterBase.Hp / playerCharacterBase.maxHp;
        //     if (healthPointEffect.fillAmount > healthPointImage.fillAmount)
        //     {
        //         healthPointEffect.fillAmount -= hurtBufferSpeed;
        //     }
        //     else
        //     {
        //         healthPointEffect.fillAmount = healthPointImage.fillAmount;
        //     }
        // }

        public void OnHpUpdateStartCoroutine()
        {
            StartCoroutine(UpdateHpCo());
        }

        IEnumerator UpdateHpCo()
        {
            healthPointImage.fillAmount = playerCharacterBase.Hp / playerCharacterBase.maxHp;
            healthPointText.text = $"{playerCharacterBase.Hp}/{playerCharacterBase.maxHp}";
            while (healthPointEffect.fillAmount > healthPointImage.fillAmount)
            {
                healthPointEffect.fillAmount -= hurtBufferSpeed;
                yield return new WaitForSeconds(reduceSpacing);
                // Debug.Log("A");
            }
            if (healthPointEffect.fillAmount < healthPointImage.fillAmount)
            {
                healthPointEffect.fillAmount = healthPointImage.fillAmount;
            }
        }

    }
}
