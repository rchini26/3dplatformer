using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.StateMachine;
using DG.Tweening;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;
        
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            boss = o as BossBase;
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            boss.StartCoroutine(WaitForActivationAndPlay());
        }

        private IEnumerator WaitForActivationAndPlay()
        {
            while (!boss.bossActive) yield return null;
            boss.BeginWithAnimation().OnComplete(() => boss.SwitchState(BossActions.Walk));
        }
    }
    
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            boss.GoToRandomPosition(OnArrive);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossActions.Attack);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            boss.StartAttack(EndAttack);
        }

        void EndAttack()
        {
            boss.SwitchState(BossActions.Walk);
        }
        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    
    public class BossStateDead : BossStateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            boss.transform.localScale = Vector3.one * .2f;
        }
    }
}