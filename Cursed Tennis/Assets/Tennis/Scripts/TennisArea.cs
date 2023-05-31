using UnityEngine;

public class TennisArea : MonoBehaviour
{
    public GameObject ball;
    public GameObject agentA;
    public GameObject agentB;
    Rigidbody m_BallRb;

    // Use this for initialization
    void Start()
    {
        m_BallRb = ball.GetComponent<Rigidbody>();
        MatchReset();
    }

    public void MatchReset()
    {
        var racketAX = agentA.transform.position.x;
        var racketBX = agentB.transform.position.x;
        var flip = Random.Range(0, 2);

        if (flip == 0)
        {
            ball.transform.position = new Vector3(racketAX, 0f, 0f) + transform.position;
        }
        else
        {
            ball.transform.position = new Vector3(racketBX, 0f, 0f) + transform.position;
        }

        m_BallRb.velocity = Vector3.zero;
        ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        ball.GetComponent<HitWall>().lastAgentHit = -1;
    }


    void FixedUpdate()
    {
        var rgV = m_BallRb.velocity;
        m_BallRb.velocity = new Vector3(Mathf.Clamp(rgV.x, -9f, 9f), Mathf.Clamp(rgV.y, -9f, 9f), rgV.z);
    }
}
