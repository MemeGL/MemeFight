using UnityEngine;
using MemeFight.Components;

[RequireComponent(typeof(ObjectMovement))]
public class MovementController : MonoBehaviour {

    [SerializeField]
    private float m_gravityMultiplier = 4;

    private ObjectMovement m_objectMovement;

    private void Awake() {
        m_objectMovement = GetComponent<ObjectMovement>();
    }

    private void Update() {
        m_objectMovement.m_velocity += Physics2D.gravity * m_gravityMultiplier * Time.deltaTime;
    }

    public void Launch(Vector2 target, float height, out float timeTaken) {
        float gravity = Physics2D.gravity.y * m_gravityMultiplier;
        float verticalSpeed = Mathf.Sqrt(-2 * gravity * height);
        timeTaken = -verticalSpeed / gravity + Mathf.Sqrt(2 * (target.y - (transform.position.y + height)) / gravity);
        float horizontalSpeed = (target.x - transform.position.x) / timeTaken;
        m_objectMovement.m_isGrounded = false;
        m_objectMovement.m_isPhasingThroughPlatforms = true;
        m_objectMovement.m_velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

}
