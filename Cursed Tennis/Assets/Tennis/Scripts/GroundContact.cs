using UnityEngine;
//using MLAgents;
using Unity.MLAgents;

namespace MLAgentsExamples
{
    [DisallowMultipleComponent]
    public class GroundContact : MonoBehaviour
    {
        [HideInInspector] public Agent agent;

        [Header("Ground Check")] public bool agentDoneOnGroundContact;
        public bool penalizeGroundContact;
        public float groundContactPenalty;
        public bool touchingGround;
        const string k_Ground = "ground";

        void OnCollisionEnter(Collision col)
        {
            if (col.transform.CompareTag(k_Ground))
            {
                touchingGround = true;
                if (penalizeGroundContact)
                {
                    agent.SetReward(groundContactPenalty);
                }

                if (agentDoneOnGroundContact)
                {
                    agent.EndEpisode();
                }
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(k_Ground))
            {
                touchingGround = false;
            }
        }
    }
}
