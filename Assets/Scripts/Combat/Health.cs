using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;
        
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            if(damage - healthPoints > 0 )
              healthPoints = 0;
            else
              healthPoints -= damage;

            print (healthPoints);
            checkIsDead();
        }

        public void checkIsDead()
        {
            if(isDead) return;
            if(healthPoints == 0)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }


}
