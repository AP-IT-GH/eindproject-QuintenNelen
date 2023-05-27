using UnityEngine;

public class HitWall : MonoBehaviour
{
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
        Debug.Log(m_AgentA.score + " agent A");
        Reset();
    }

    void AgentBWins()
    {
        m_AgentA.AddReward(-1f);
        m_AgentB.AddReward(1f);
        m_AgentB.score += 1;
        Debug.Log(m_AgentB.score + " agent B");
        Reset();
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
                }
                else
                {
                    AgentAWins();
                }
            }
            else if (col.gameObject.name == "wallB")
            {
                if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit)
                {
                    AgentAWins();
                }
                else
                {
                    AgentBWins();
                }
            }
            else if (col.gameObject.name == "LeftSideWall")
            {
                if (lastAgentHit == 1)
                {
                    AgentAWins();
                }
                else
                {
                    AgentBWins();
                }
            }
            else if (col.gameObject.name == "RightSideWall")
            {
                if (lastAgentHit == 1)
                {
                    AgentAWins();
                }
                else
                {
                    AgentBWins();
                }
            }
            else if (col.gameObject.name == "floorA")
            {
                if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit || lastFloorHit == FloorHit.Service)
                {
                    m_AgentA.AddReward(-0.5f);
                    AgentBWins();
                }
                else
                {
                    lastFloorHit = FloorHit.FloorAHit;
                    m_AgentA.AddReward(1f);
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
                    m_AgentB.AddReward(-0.5f);
                    AgentAWins();
                }
                else
                {
                    lastFloorHit = FloorHit.FloorBHit;
                    m_AgentB.AddReward(1f);
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
                }
                else if (lastAgentHit == 1)
                {
                    AgentAWins();
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
            }
            else
            {
                m_AgentA.AddReward(0.1f);

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
            }
            else
            {
                m_AgentB.AddReward(0.1f);

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
