using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class ParticleSystemAutoDisabled : MonoBehaviour
    {
        ParticleSystem _particleSystem;
        ParticleSystem.MainModule _mainModule;
        private void Start()
        {

            Debug.Log("     粒子start  " + gameObject.name);
            _particleSystem = GetComponent<ParticleSystem>();
            _mainModule = _particleSystem.main;
            _mainModule.loop = false;
            _mainModule.stopAction = ParticleSystemStopAction.Callback;
        }
        private void OnParticleSystemStopped()
        {
            Debug.Log("     粒子" + gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
