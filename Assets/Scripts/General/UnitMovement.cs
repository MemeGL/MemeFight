using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UnitMovement : MonoBehaviour {

    private const float RAYCAST_BUFFER_WIDTH = 0.05f;
    private float MAXIMUM_CLIMBABLE_ANGLE = 60;

    private Collider2D m_collider;

    [SerializeField]
    private int m_verticalRaycastCount = 5;
    private LayerMask m_collisionLayerMask;
    private LayerMask m_collisionLayerMaskNoPlatform;
    [HideInInspector]
    public bool m_isGrounded = true;
    [HideInInspector]
    public bool m_isPhasingThroughPlatforms = false;
    [HideInInspector]
    public Vector2 m_velocity;

    private void Awake() {
        m_collider = GetComponent<Collider2D>();
        m_collisionLayerMask = LayerMask.GetMask(Layers.PLATFORM, Layers.BOUNDARY);
        m_collisionLayerMaskNoPlatform = LayerMask.GetMask(Layers.BOUNDARY);
    }

    private void FixedUpdate() {
        if (m_velocity.x != 0) {
            ProcessHorizontalMovement();
        }
        if (m_isGrounded || m_velocity.y < 0) {
            ProcessVerticalDownwardsMovement();
        } else {
            transform.Translate(new Vector3(0, m_velocity.y, 0) * Time.fixedDeltaTime);
            //ProcessVerticalUpwardsMovement();
        }
    }

    private void ProcessHorizontalMovement() {
        Bounds objectColliderBounds = m_collider.bounds;
        Vector2 startRaycastPoint = new Vector2(objectColliderBounds.center.x, objectColliderBounds.min.y + RAYCAST_BUFFER_WIDTH);
        Vector2 endRaycastPoint = new Vector2(objectColliderBounds.center.x, objectColliderBounds.max.y - RAYCAST_BUFFER_WIDTH);
        float halfBoundsWidthX = objectColliderBounds.size.x * 0.5f;
        float raycastDistance = halfBoundsWidthX + Mathf.Abs(m_velocity.x * Time.fixedDeltaTime);
        Vector2 raycastDirection = Mathf.Sign(m_velocity.x) > 0 ? Vector2.right : Vector2.left;
        float horizontalMovementDirection = Mathf.Sign(m_velocity.x);

        RaycastHit2D horizontalHit = Physics2D.Raycast(startRaycastPoint, raycastDirection, raycastDistance, m_collisionLayerMask);
        int hitLayer = 0;
        if (horizontalHit) {
            hitLayer = horizontalHit.collider.gameObject.layer;
        }
        if (horizontalHit && (hitLayer == LayerMask.NameToLayer(Layers.BOUNDARY) || (m_isGrounded && hitLayer == LayerMask.NameToLayer(Layers.PLATFORM)))) {
            if (Vector2.Angle(horizontalHit.normal, Vector2.up) > MAXIMUM_CLIMBABLE_ANGLE) {
                float distanceToObstacle = horizontalHit.distance - halfBoundsWidthX;
                transform.Translate(raycastDirection * distanceToObstacle);
                m_velocity = new Vector2(0, m_velocity.y);
            } else {
                if (m_isGrounded) {
                    Vector2 directionAlongGround = new Vector2(horizontalHit.normal.y * horizontalMovementDirection, -horizontalHit.normal.x * horizontalMovementDirection);
                    transform.Translate(directionAlongGround * m_velocity.magnitude * Time.fixedDeltaTime);
                } else {
                    float distanceToObstacle = horizontalHit.distance - halfBoundsWidthX;
                    transform.Translate(raycastDirection * distanceToObstacle);
                    m_isGrounded = true;
                }
            }
        } else {
            transform.Translate(new Vector2(m_velocity.x, 0) * Time.fixedDeltaTime);
        }
    }

    private void ProcessVerticalDownwardsMovement() {
        Bounds objectColliderBounds = m_collider.bounds;
        Vector2 startRaycastPoint = new Vector2(objectColliderBounds.min.x + RAYCAST_BUFFER_WIDTH, objectColliderBounds.min.y + RAYCAST_BUFFER_WIDTH);
        Vector2 endRaycastPoint = new Vector2(objectColliderBounds.max.x - RAYCAST_BUFFER_WIDTH, objectColliderBounds.min.y + RAYCAST_BUFFER_WIDTH);
        //float halfBoundsWidthY = objectColliderBounds.size.y * 0.5f;
        float raycastDistance = RAYCAST_BUFFER_WIDTH + (m_isGrounded ? RAYCAST_BUFFER_WIDTH : Mathf.Abs(m_velocity.y * Time.fixedDeltaTime));

        RaycastHit2D[] verticalHits = new RaycastHit2D[m_verticalRaycastCount];
        float minHitDistance = Mathf.Infinity;
        int raycastIndexUsed = -1;
        LayerMask layerMask = m_isPhasingThroughPlatforms ? m_collisionLayerMaskNoPlatform : m_collisionLayerMask;

        for (int i = 0; i < m_verticalRaycastCount; i++) {
            float lerpAmount = i / (m_verticalRaycastCount - 1);
            Vector2 raycastOrigin = Vector2.Lerp(startRaycastPoint, endRaycastPoint, lerpAmount);
            verticalHits[i] = Physics2D.Raycast(raycastOrigin, Vector2.down, raycastDistance, layerMask);
            float hitDistance = verticalHits[i].distance;

            if (hitDistance > 0 && hitDistance < minHitDistance) {
                raycastIndexUsed = i;
                minHitDistance = hitDistance;
            }
        }

        if (raycastIndexUsed != -1) {
            RaycastHit2D verticalHit = verticalHits[raycastIndexUsed];
            if (Vector2.Angle(verticalHit.normal, Vector2.up) > MAXIMUM_CLIMBABLE_ANGLE) {
                float slideDirectionX = -Mathf.Sign(verticalHit.normal.x);
                Vector2 directionAlongGround = new Vector2(verticalHit.normal.y * slideDirectionX, -verticalHit.normal.x * slideDirectionX);
                float slideVelocityScale = Vector2.Angle(verticalHit.normal, Vector2.up) / 90f;
                Vector2 targetVelocity = directionAlongGround * m_velocity.y * slideVelocityScale;
                transform.Translate(targetVelocity * Time.fixedDeltaTime);
                m_velocity.x = Mathf.Max(m_velocity.x, targetVelocity.x);
            } else {
                m_isGrounded = true;
                float distanceToObstacle = minHitDistance - RAYCAST_BUFFER_WIDTH;
                transform.Translate(Vector3.down * distanceToObstacle);
                m_velocity = new Vector2(m_velocity.x, 0);
            }
        } else {
            transform.Translate(new Vector3(0, m_velocity.y, 0) * Time.fixedDeltaTime);
            m_isGrounded = false;
        }

    }
}