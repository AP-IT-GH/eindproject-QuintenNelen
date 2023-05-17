using UnityEngine;

public class RagdollFollower : MonoBehaviour
{
    public GameObject targetObject;
    public Transform ragdollRoot;
    public float followForce = 500f;
    public float maxVelocity = 10f;

    private FixedJoint fixedJoint;

    void Start()
    {
        fixedJoint = ragdollRoot.gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = targetObject.GetComponent<Rigidbody>();
        fixedJoint.anchor = Vector3.zero;
        fixedJoint.connectedAnchor = Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector3 direction = targetObject.transform.position - ragdollRoot.position;
        ragdollRoot.GetComponent<Rigidbody>().AddForce(direction * followForce * Time.fixedDeltaTime);
        if (ragdollRoot.GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
        {
            ragdollRoot.GetComponent<Rigidbody>().velocity = ragdollRoot.GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
        }
    }
}
