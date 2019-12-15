using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        private void Update()
        {

            if(InAttackRange() && fighter.CanAttack(player))
            {
                print(gameObject.name + " Chase!");
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance ;
        }
    }
}
