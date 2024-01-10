using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JWOAGameSystem
{
    public class EnergyBar : MonoBehaviour
    {
        public Image energyPointImage;
        [SerializeField, Header("协程每次递增的速度")] private float increaseBufferSpeed = 0.00003f;
        [SerializeField, Header("协程递增的时间间隔")] private float increaseSpacing = 0.00005f;

        public Text energyPointText;

        private CharactersBase playerCharacterBase;


        private void Awake()
        {
            playerCharacterBase = GameObject.FindGameObjectWithTag("Player").GetComponent<CharactersBase>();
        }

        private void Start()
        {
            OnMpUpdate();
        }
        private void Update()
        {
            // Debug.Log("             mp  " + playerCharacterBase.Mp);
            if (playerCharacterBase.Mp < playerCharacterBase.maxMp)
            {
                StartCoroutine(UpdateMpCo());
            }
        }

        public void OnMpUpdate()
        {
            Debug.Log("     " + playerCharacterBase.Mp / playerCharacterBase.maxMp);
            energyPointImage.fillAmount = playerCharacterBase.Mp / playerCharacterBase.maxMp;
            energyPointText.text = $"{playerCharacterBase.Mp}/{playerCharacterBase.maxMp}";
        }


        IEnumerator UpdateMpCo()
        {
            OnMpUpdate();
            while (playerCharacterBase.Mp <= playerCharacterBase.maxMp)
            {
                playerCharacterBase.Mp += increaseBufferSpeed;
                UpdateMpCo();
                yield return new WaitForSeconds(increaseSpacing);
                // Debug.Log("B");
            }
            if (playerCharacterBase.Mp > playerCharacterBase.maxMp)
            {
                playerCharacterBase.Mp = playerCharacterBase.maxMp;
            }
        }

    }
}
