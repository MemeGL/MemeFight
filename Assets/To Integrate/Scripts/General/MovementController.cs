using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMovement))]
public class MovementController : MonoBehaviour {

    [SerializeField]
    private float gravityMultiplier = 1;

    private ObjectMovement m_objectMovement;

    private void Awake() {
        m_objectMovement = GetComponent<ObjectMovement>();
    }

    private void Update() {
        m_objectMovement.m_velocity += new Vector2(0, GlobalConstants.gravity * gravityMultiplier) * Time.deltaTime;
    }

    public void Launch(Vector2 target, float height, out float timeTaken) {
        float gravity = GlobalConstants.gravity * gravityMultiplier;
        float verticalSpeed = Mathf.Sqrt(-2 * gravity * height);
        timeTaken = -verticalSpeed / gravity + Mathf.Sqrt(2 * (target.y - (transform.position.y + height)) / gravity);
        float horizontalSpeed = (target.x - transform.position.x) / timeTaken;
        m_objectMovement.m_isGrounded = false;
        m_objectMovement.m_isPhasingThroughPlatforms = true;
        m_objectMovement.m_velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

}
