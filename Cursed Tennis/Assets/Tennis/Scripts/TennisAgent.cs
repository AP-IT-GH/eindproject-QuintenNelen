using UnityEngine;
using UnityEngine.UI;
//using MLAgents;
//using MLAgents.Sensors;
//using MLAgents.SideChannels;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.SideChannels;
using Unity.MLAgents.Actuators;

public class TennisAgent : Agent
{
    [Header("Specific to Tennis")]
    public GameObject ball;
    public Transform Target;
    public GameObject targetPrefab;
    public GameObject net;

    public bool invertX;
    public int score;
    public GameObject myArea;
    public float angle;
    public float scale;

    Text m_TextComponent;
    Rigidbody m_AgentRb;
    Rigidbody m_BallRb;
    float m_InvertMult;
    EnvironmentParameters m_ResetParams;

    // Looks for the scoreboard based on the name of the gameObjects.
    // Do not modify the names of the Score GameObjects
    const string k_CanvasName = "Canvas";
    const string k_ScoreBoardAName = "ScoreA";
    const string k_ScoreBoardBName = "ScoreB";

    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_BallRb = ball.GetComponent<Rigidbody>();
        var canvas = GameObject.Find(k_CanvasName);
        GameObject scoreBoard;
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        if (invertX)
        {
            scoreBoard = canvas.transform.Find(k_ScoreBoardBName).gameObject;
        }
        else
        {
            scoreBoard = canvas.transform.Find(k_ScoreBoardAName).gameObject;
        }
        m_TextComponent = scoreBoard.GetComponent<Text>();
        SetResetParameters();
    }

    public override void OnEpisodeBegin()
    {
        m_InvertMult = invertX ? -1f : 1f;

        //transform.position = new Vector3(21.64f, -6.52f, -1.8f); // Set the desired position
        transform.position = new Vector3(-m_InvertMult * 16.5f, -1.5f, -1.8f) + transform.parent.position;
        m_AgentRb.velocity = Vector3.zero; // Reset velocity

        SetResetParameters();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       /* sensor.AddObservation(m_InvertMult * (transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(transform.position.y - myArea.transform.position.y);
        sensor.AddObservation(m_InvertMult * m_AgentRb.velocity.x);
        sensor.AddObservation(m_AgentRb.velocity.y);
        sensor.AddObservation(transform.position.z - myArea.transform.position.z);

        sensor.AddObservation(m_InvertMult * (ball.transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(ball.transform.position.y - myArea.transform.position.y);

        sensor.AddObservation(m_InvertMult * m_BallRb.velocity.x);
        sensor.AddObservation(m_InvertMult * m_BallRb.velocity.y);
        sensor.AddObservation(m_BallRb.velocity.y);
        sensor.AddObservation(m_BallRb.transform.localPosition.z);
        sensor.AddObservation(m_BallRb.transform.localPosition.z);
        sensor.AddObservation(transform.position.z - myArea.transform.position.z);

        sensor.AddObservation(m_InvertMult * gameObject.transform.rotation.z);

        // sensor.AddObservation(this.transform.localPosition);
        //sensor.AddObservation(this.transform.localRotation);
        // sensor.AddObservation(Target.transform.localPosition);*/


        /*sensor.AddObservation(m_InvertMult * (transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(transform.position.y - myArea.transform.position.y);
        sensor.AddObservation(m_InvertMult * m_AgentRb.velocity.x);
        sensor.AddObservation(m_AgentRb.velocity.y);
        sensor.AddObservation(m_AgentRb.transform.localPosition);
        sensor.AddObservation(m_AgentRb.transform.localRotation);
        sensor.AddObservation(transform.position.z - myArea.transform.position.z);

        sensor.AddObservation(m_InvertMult * (ball.transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(ball.transform.position.y - myArea.transform.position.y);
        sensor.AddObservation(m_InvertMult * m_BallRb.velocity.x);
        sensor.AddObservation(m_BallRb.velocity.y);
        sensor.AddObservation(m_BallRb.transform.localPosition);
        sensor.AddObservation(transform.position.z - myArea.transform.position.z);

        sensor.AddObservation(m_InvertMult * gameObject.transform.rotation.z);
        */

        sensor.AddObservation(m_InvertMult * (ball.transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(m_InvertMult * (gameObject.transform.position.x - myArea.transform.position.x));
        sensor.AddObservation(m_InvertMult * m_AgentRb.velocity.x);
        sensor.AddObservation(m_InvertMult * m_BallRb.velocity.x);

        sensor.AddObservation(ball.transform.position.y - myArea.transform.position.y);
        sensor.AddObservation(gameObject.transform.position.y - myArea.transform.position.y);
        sensor.AddObservation(m_AgentRb.velocity.y);
        sensor.AddObservation(m_BallRb.velocity.y);

        sensor.AddObservation(m_InvertMult * (ball.transform.position.z - myArea.transform.position.z));
        sensor.AddObservation(m_InvertMult * (gameObject.transform.position.z - myArea.transform.position.z));
        sensor.AddObservation(m_InvertMult * m_AgentRb.velocity.z);
        sensor.AddObservation(m_InvertMult * m_BallRb.velocity.z);

        sensor.AddObservation(m_InvertMult * (gameObject.transform.position - myArea.transform.position)); // Agent's position relative to myArea
        sensor.AddObservation(m_InvertMult * (ball.transform.position - myArea.transform.position)); // Ball's position relative to myArea

        Vector3 targetPosition = targetPrefab.transform.position;
        sensor.AddObservation(targetPosition.x - myArea.transform.position.x);
        sensor.AddObservation(targetPosition.y - myArea.transform.position.y);
        sensor.AddObservation(targetPosition.z - myArea.transform.position.z);

        Vector3 netpos = net.transform.position;
        sensor.AddObservation(netpos.x - myArea.transform.position.x);
        sensor.AddObservation(netpos.y - myArea.transform.position.y);
        sensor.AddObservation(netpos.z - myArea.transform.position.z);




    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

       /* var vectorAction = actionBuffers.ContinuousActions;
        var moveX = Mathf.Clamp(vectorAction[0], -1f, 1f) * m_InvertMult;
        var jumpY = Mathf.Clamp(vectorAction[1], -1f, 1f);
        var moveZ = Mathf.Clamp(vectorAction[2], -1f, 1f) * m_InvertMult;
        var rotateY = Mathf.Clamp(vectorAction[3], -1f, 1f) * m_InvertMult;
        var rotateX = Mathf.Clamp(vectorAction[4], -1f, 1f) * m_InvertMult;
        var rotateZ = Mathf.Clamp(vectorAction[5], -1f, 1f) * m_InvertMult;

        // Move in the X, Y, and Z dimensions
        m_AgentRb.velocity = new Vector3(moveX * 8f, m_AgentRb.velocity.y + jumpY * 8f, moveZ * 8f);

        // Rotate in the Y dimension
        m_AgentRb.transform.rotation = Quaternion.Euler(0f, -180f, 55f * rotateY + m_InvertMult * 90f);

        // Rotate in the X dimension
        var currentRotation = m_AgentRb.transform.rotation.eulerAngles;
        m_AgentRb.transform.rotation = Quaternion.Euler(rotateX * 90f, currentRotation.y, currentRotation.z);

        // Rotate in the Z dimension
        m_AgentRb.transform.Rotate(0f, 0f, rotateZ * 90f, Space.Self);

        if (jumpY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            m_AgentRb.velocity = new Vector3(m_AgentRb.velocity.x, 4f, m_AgentRb.velocity.z);
        }

        if (invertX && transform.position.x - transform.parent.transform.position.x < -m_InvertMult ||
            !invertX && transform.position.x - transform.parent.transform.position.x > -m_InvertMult)
        {
            transform.position = new Vector3(-m_InvertMult + transform.parent.transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        m_TextComponent.text = score.ToString();*/


  
        var vectorAction = actionBuffers.ContinuousActions;
        var moveX = Mathf.Clamp(vectorAction[0], -1f, 1f) * m_InvertMult;
        var moveY = Mathf.Clamp(vectorAction[1], -1f, 1f);
        var rotate = Mathf.Clamp(vectorAction[2], -1f, 1f) * m_InvertMult;
        var moveZ = Mathf.Clamp(vectorAction[3], -1f, 1f) * m_InvertMult;

        var rotateX = Mathf.Clamp(vectorAction[4], -1f, 1f) * m_InvertMult;
        var rotateY = Mathf.Clamp(vectorAction[5], -1f, 1f) * m_InvertMult;



        if (moveY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            m_AgentRb.velocity = new Vector3(m_AgentRb.velocity.x, 4f, m_AgentRb.velocity.z);
        }


        m_AgentRb.velocity = new Vector3(moveX * 8f, m_AgentRb.velocity.y, moveZ * 8f);


        m_AgentRb.transform.rotation = Quaternion.Euler(0f, -180f, 55f * rotate + m_InvertMult * 90f);

        Quaternion rotationX = Quaternion.Euler(55f * rotateX + m_InvertMult * 90f, -120f, 0f);
        m_AgentRb.transform.rotation *= rotationX;

        Quaternion rotationY = Quaternion.Euler(0f, 55f * rotateY + m_InvertMult * 90f, 90f);
        m_AgentRb.transform.rotation *= rotationY;


        if (invertX && transform.position.x - transform.parent.transform.position.x < -m_InvertMult ||
            !invertX && transform.position.x - transform.parent.transform.position.x > -m_InvertMult)
        {
            transform.position = new Vector3(-m_InvertMult + transform.parent.transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        m_TextComponent.text = score.ToString();
    

}



public override void Heuristic(in ActionBuffers actionsOut)
    {
       var continuousActionsOut = actionsOut.ContinuousActions;
        /*continuousActionsOut[0] = Input.GetAxis("Horizontal");    // Racket Movement
       continuousActionsOut[1] = Input.GetKey(KeyCode.Space) ? 1f : 0f;   // Racket Jumping
       continuousActionsOut[2] = Input.GetAxis("Vertical");   // Racket Rotation
       float v = Input.GetKey(KeyCode.G) ? -1f : Input.GetKey(KeyCode.B) ? 1f : 0f; // z
       continuousActionsOut[3] = v; //z actions*/



        continuousActionsOut[0] = Input.GetAxis("Horizontal");    // Racket Movement in the X dimension
        continuousActionsOut[1] = Input.GetKey(KeyCode.Space) ? 1f : 0f;   // Racket Jumping (Y dimension)
        continuousActionsOut[2] = Input.GetAxis("Vertical");   // Racket Rotation in the z dimension


        float z = Input.GetKey(KeyCode.O) ? -1f : Input.GetKey(KeyCode.L) ? 1f : 0f; // Additional input mapped to action index 5 (Z dimension rotation)
        continuousActionsOut[3] = z;

        float x = Input.GetKey(KeyCode.I) ? -1f : Input.GetKey(KeyCode.K) ? 1f : 0f; // Additional input mapped to action index 4 (X dimension rotation)
        continuousActionsOut[4] = x;

        float y = Input.GetKey(KeyCode.U) ? -1f : Input.GetKey(KeyCode.J) ? 1f : 0f; // Additional input mapped to action index 3 (Y dimension rotation)
        continuousActionsOut[5] = y;




    }


    public void SetRacket()
    {
        angle = m_ResetParams.GetWithDefault("angle", 55);
        gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            m_InvertMult * angle
        );
    }

    public void SetBall()
    {
        scale = m_ResetParams.GetWithDefault("scale", .5f);
        ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetRacket();
        SetBall();
    }
}
