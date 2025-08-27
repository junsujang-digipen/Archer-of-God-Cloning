using UnityEngine;

public class Aiming : MonoBehaviour
{
    Vector2 _velocity;
    public Vector2 Velocity {set
        {
            _velocity = value;
            if (_velocity.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
