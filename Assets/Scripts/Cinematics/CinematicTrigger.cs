using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool hasBeenTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if(!hasBeenTriggered && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                hasBeenTriggered = true;
            }

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
