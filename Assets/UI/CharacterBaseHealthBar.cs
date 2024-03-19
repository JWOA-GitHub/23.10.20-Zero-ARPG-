using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JWOAGameSystem
{
    public class CharacterBaseHealthBar : MonoBehaviour
    {
        public Image healthPointImage;
        [SerializeField, Header("协程每次递增的速度")] private float increaseBufferSpeed = 0.000003f;
        [SerializeField, Header("协程递增的时间间隔")] private float increaseSpacing = 0.000005f;

        public Text healthPointText;
        public Text cahracterNameText;

        [SerializeField] private CharactersBase characterBase;

        private void Start()
        {
            cahracterNameText.text = characterBase.name;
            OnHpUpdate();
        }
        private void Update()
        {
            // Debug.Log("             Hp  " + characterBase.Hp);
            if (characterBase.Hp <= 0)
            {
                characterBase.Hp = 0;
                OnHpUpdate();
                return;
            }

            if (characterBase.Hp < characterBase.maxHp && !characterBase.IsDead)
            {
                StartCoroutine(UpdateHpCo());
            }
        }

        public void OnHpUpdate()
        {
            // Debug.Log("     " + characterBase.Hp / characterBase.maxHp);
            healthPointImage.fillAmount = characterBase.Hp / characterBase.maxHp;
            healthPointText.text = $"{characterBase.Hp}/{characterBase.maxHp}";
        }


        IEnumerator UpdateHpCo()
        {
            while (characterBase.Hp <= characterBase.maxHp)
            {
                if (characterBase.Hp <= 0)
                {
                    characterBase.Hp = 0;
                    OnHpUpdate();
                    break;
                }

                characterBase.Hp += increaseBufferSpeed;
                OnHpUpdate();
                yield return new WaitForSeconds(increaseSpacing);
                // Debug.Log("C");
            }
            if (characterBase.Hp > characterBase.maxHp)
            {
                characterBase.Hp = characterBase.maxHp;
            }
        }

    }
}
