using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private PlayerController _player;
        private HealthBase _healthBase;

        private void Start()
        {
            _healthBase = GetComponent<HealthBase>();
            _player = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (_healthBase.isDead && Input.GetKeyDown(KeyCode.R))
            {
                Revive();
            }
        }
        
        public void Revive()
        {
            _healthBase.ResetLife();
            _player.animator.SetTrigger("Revive");
            _player.stateMachine.SwitchState(PlayerController.PlayerStates.Idle);
            Respawn();
        }

        public void Respawn()
        {
            if (CheckPointManager.Instance.HasCheckPoint())
            {
                transform.position = CheckPointManager.Instance.GetLastCheckPoint();
            }
        }
    }
}