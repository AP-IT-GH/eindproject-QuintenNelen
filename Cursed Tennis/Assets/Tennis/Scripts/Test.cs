using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Test : Agent
{
    public Transform ball;
    public Transform racket;

    private Rigidbody racketRb;
    private float previousDistance;

    public override void Initialize()
    {
        racketRb = racket.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the position and velocity of the ball and racket
        ball.position = new Vector3(0f, 0.5f, 0f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        racket.position = new Vector3(0f, 0.5f, -4f);
        racketRb.velocity = Vector3.zero;

        previousDistance = GetDistanceToBall();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe the position and velocity of the ball and racket
        sensor.AddObservation(ball.position);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(racket.position);
        sensor.AddObservation(racketRb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
{
    // Get the continuous actions from the action buffers
    float moveX = actionBuffers.ContinuousActions[0];
    float moveZ = actionBuffers.ContinuousActions[1];

    // Apply force to the racket based on the actions
    float force = 10f;
    Vector3 movement = new Vector3(moveX, 0f, moveZ);
    racketRb.AddForce(movement * force);

    // Calculate the current distance to the ball
    float currentDistance = GetDistanceToBall();

    // Set a reward based on the change in distance to the ball
    float distanceDelta = previousDistance - currentDistance;
    AddReward(distanceDelta);

    previousDistance = currentDistance;

    // Check if the ball falls out of bounds
    if (ball.position.y < -1f)
    {
        SetReward(-1f);
        EndEpisode();
    }

    // Check if the racket hits the ball
    if (currentDistance < 1.5f)
    {
        // If the ball is hit successfully, give a positive reward
        SetReward(1f);
        EndEpisode();
    }
}


    private float GetDistanceToBall()
    {
        return Vector3.Distance(ball.position, racket.position);
    }
}
