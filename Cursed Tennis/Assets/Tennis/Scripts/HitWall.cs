using UnityEngine;

public class HitWall : MonoBehaviour
{
    public TennisMatchManager matchManager;

    public GameObject areaObject;
    public TennisArea m_Area;
    public TennisAgent m_AgentA;
    public TennisAgent m_AgentB;

    public int lastAgentHit;
    public bool net;

    public enum FloorHit
    {
        Service,
        FloorHitUnset,
        FloorAHit,
        FloorBHit
    }

    public FloorHit lastFloorHit;

    void Start()
    {
        m_Area = areaObject.GetComponent<TennisArea>();
        m_AgentA = m_Area.agentA.GetComponent<TennisAgent>();
        m_AgentB = m_Area.agentB.GetComponent<TennisAgent>();
    }

    void Reset()
    {
        m_AgentA.EndEpisode();
        m_AgentB.EndEpisode();
        m_Area.MatchReset();
        lastFloorHit = FloorHit.Service;
        net = false;
    }

    void AgentAWins()
    {
        m_AgentA.AddReward(1f);
        m_AgentB.AddReward(-1f);
        m_AgentA.score += 1;
        Reset();
    }

    void AgentBWins()
    {
        m_AgentA.AddReward(-1f);
        m_AgentB.AddReward(1f);
        m_AgentB.score += 1;
        Reset();
    }

    void ScorePoint(TennisAgent agent)
    {
        agent.ScorePoint();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("iWall"))
        {
            if (col.gameObject.name == "wallA")
            {
                if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit)
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
                else
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
            }
            else if (col.gameObject.name == "wallB")
            {
                if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit)
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
                else
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
            }
            else if (col.gameObject.name == "LeftSideWall" || col.gameObject.name == "RigthSideWall")
            {
                if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit)
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
                else
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
            }
            else if (col.gameObject.name == "RigthSideWall" || col.gameObject.name == "LeftSideWall")
            {
                if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit)
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
                else
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
            }
            else if (col.gameObject.name == "floorA")
            {
                if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit || lastFloorHit == FloorHit.Service)
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
                else
                {
                    lastFloorHit = FloorHit.FloorAHit;
                    if (!net)
                    {
                        net = true;
                    }
                }
            }
            else if (col.gameObject.name == "floorB")
            {
                if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit || lastFloorHit == FloorHit.Service)
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
                else
                {
                    lastFloorHit = FloorHit.FloorBHit;
                    if (!net)
                    {
                        net = true;
                    }
                }
            }
            else if (col.gameObject.name == "net" && !net)
            {
                if (lastAgentHit == 0)
                {
                    AgentBWins();
                    matchManager.ScorePoint(false);
                }
                else if (lastAgentHit == 1)
                {
                    AgentAWins();
                    matchManager.ScorePoint(true);
                }
            }

            if (col.gameObject.name == "over" && lastAgentHit == 0)
            {
                if (m_AgentA != null)
                {
                    m_AgentA.AddReward(0.2f);
                    matchManager.ScorePoint(false);
                }
                else
                {
                    Debug.LogError("Agent A is niet correct toegewezen.");
                }
            }
            else if (col.gameObject.name == "over" && lastAgentHit == 1)
            {
                if (m_AgentB != null)
                {
                    m_AgentB.AddReward(0.2f);
                    matchManager.ScorePoint(true);
                }
                else
                {
                    Debug.LogError("Agent B is niet correct toegewezen.");
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "AgentA")
        {
            if (lastAgentHit == 0)
            {
                AgentBWins();
                matchManager.ScorePoint(false);
            }
            else
            {
                m_AgentA.AddReward(0.1f);

                // Agent A hits the ball successfully
                // m_AgentA.AddReward(0.3f);

                //agent can return serve in the air
                if (lastFloorHit != FloorHit.Service && !net)
                {
                    net = true;
                }

                lastAgentHit = 0;
                lastFloorHit = FloorHit.FloorHitUnset;
            }
        }
        else if (col.gameObject.name == "AgentB")
        {
            if (lastAgentHit == 1)
            {
                AgentAWins();
                matchManager.ScorePoint(true);
            }
            else
            {
                m_AgentB.AddReward(0.1f);

                // Agent B hits the ball successfully
                // m_AgentB.AddReward(0.3f);

                // Rest of the code...
                if (lastFloorHit != FloorHit.Service && !net)
                {
                    net = true;
                }

                lastAgentHit = 1;
                lastFloorHit = FloorHit.FloorHitUnset;
            }
        }
    }
}
