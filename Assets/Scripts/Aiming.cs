using UnityEngine;

public class Aiming : MonoBehaviour
{
    Vector2 _velocity;
    public Vector2 Velocity
    {
        get { return _velocity; }
        set
        {
            _velocity = value;
            if (_velocity.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    
    public static Vector3 ComputeDirection(Vector3 position, Vector3 targetPosition, float gravity, float defaultTime = 1f, float timeScale = 0.05f)
    {
        Vector3 direction = targetPosition - position;
        float distance = direction.magnitude;
        float t = defaultTime + distance * timeScale; // 1초 + 거리 기반 시간 추가
        direction.x /= t;
        direction.y -= 0.5f * gravity * t * t;
        direction.y /= t;
        return direction;
    }
}
