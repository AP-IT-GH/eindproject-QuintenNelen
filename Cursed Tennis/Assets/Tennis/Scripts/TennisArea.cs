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

    /*public void MatchReset()
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
    }*/

    public void MatchReset()
    {
        // Reset ball position
        var agentAX = agentA.transform.position.x;
        var agentBX = agentB.transform.position.x;
        var flip = Random.Range(0, 2);
        var ballX = flip == 0 ? agentAX - 0.5f : agentBX - 0.5f; // Adjust the value to determine the distance between the ball and the agent
        var ballY = 3.5f;
        var ballZ = 0f;
        ball.transform.position = new Vector3(ballX, ballY, ballZ) + transform.position;

        // Reset ball velocity
        m_BallRb.velocity = Vector3.zero;

        // Reset ball scale
        var ballScale = new Vector3(0.5f, 0.5f, 0.5f);
        ball.transform.localScale = ballScale;

        // Reset last agent hit
        ball.GetComponent<HitWall>().lastAgentHit = -1;

        // Reset floor hit
        ball.GetComponent<HitWall>().lastFloorHit = HitWall.FloorHit.Service;
    }


}



