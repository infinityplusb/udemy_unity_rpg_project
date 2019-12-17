using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
      const float waypointGizmoRadius = 0.3f;
      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }

      private void OnDrawGizmos()
      {
          for(int i = 0; i < transform.childCount; i++ )
          {
              int j = GetNextIndex(i);

              Gizmos.color = Color.blue;
              Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
              Gizmos.color = Color.white;
              Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
          }
      }

      private Vector3 GetWaypoint(int i)
      {
          return transform.GetChild(i).position;
      }

      private int GetNextIndex(int i)
      {
          if(i < transform.childCount - 1)
            return i+1;
          else
            return 0;
      }


    }
}
