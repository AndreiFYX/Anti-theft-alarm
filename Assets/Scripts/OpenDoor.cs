using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class OpenDoor : MonoBehaviour
    {
        private readonly int OpenTrigger = Animator.StringToHash("Open");

        [SerializeField] private Animator _animator;
        
        public void Open()
        {
            _animator.SetTrigger(OpenTrigger);
        }        
    }
}