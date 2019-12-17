using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath ;
        [SerializeField] float waypointTolerance = 1f;

        Fighter fighter;
        GameObject player;
        Mover mover ;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();

            if(patrolPath != null)
              guardPosition = patrolPath.GetChild(0).position;
            else
              guardPosition = transform.position;

        }

        // Update is called once per frame
        private void Update()
        {
            if(!GetComponent<Health>().IsDead() )
            {
                if(InAttackRange() && fighter.CanAttack(player))
                {
//                    print(gameObject.name + " Chase!");
                    timeSinceLastSawPlayer = 0;
                    AttackBehaviour();
                }
                else if (timeSinceLastSawPlayer < suspicionTime)
                {
                    SuspicionBehaviour();
                }
                else
                {
                    PatrolBehaviour();
                }
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPostion = guardPosition;

            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            if(Vector3.Distance(transform.position, nextPosition) <= waypointTolerance)
              return true;
        }

        private bool CycleWaypoint()
        {
            GetComponent<PatrolPath>().GetNextIndex()
        }

        private int GetCurrentWaypoint()
        {
            for(int i = 0; i < transform.childCount; i++ )
              if(transform.GetChild(i).position == transform.position)
                return i;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance ;
        }

        // called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
