using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null)
            return ;

            if(target.IsDead())
            return ;

            if(target != null && !GetIsInRange())
            {
                // print("Moving into combat");
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                // print("Pew Pew");
                GetComponent<Mover>().Cancel();
                if(timeSinceLastAttack > timeBetweenAttacks)
                  AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            // This will trigger the Hit() event
            TriggerAttack();
            timeSinceLastAttack = 0;
        }

        public void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if(target == null) return ;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            stopAttack();
            target = null;
        }

        public void stopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }
}
