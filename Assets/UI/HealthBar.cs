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
        [SerializeField] private float hurtBufferSpeed = 0.003f;

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
            Debug.Log("             " + playerCharacterBase.Hp);
        }

        private void Update()
        {
            healthPointImage.fillAmount = playerCharacterBase.Hp / playerCharacterBase.maxHp;
            if (healthPointEffect.fillAmount > healthPointImage.fillAmount)
            {
                healthPointEffect.fillAmount -= hurtBufferSpeed;
            }
            else
            {
                healthPointEffect.fillAmount = healthPointImage.fillAmount;
            }
        }

    }
}
