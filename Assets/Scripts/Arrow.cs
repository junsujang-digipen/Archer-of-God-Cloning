using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Damage;
    public Vector3 targetPosition;
    Rigidbody2D _rigidbody2D;
    Aiming _aiming;
    [SerializeField] GameObject _afterEffect;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _aiming = GetComponent<Aiming>();
        float _lifeTime = 10f;
        Destroy(gameObject, _lifeTime);
    }
    void Update()
    { 
        _aiming.Velocity = _rigidbody2D.linearVelocity;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow")) return;
        Debug.Log("Hit " + other.name);
        _rigidbody2D.linearVelocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.simulated = false;
        if (_afterEffect != null)
        {
            GameObject afterEffect = Instantiate(_afterEffect, (other.transform.position + transform.position) / 2f, transform.rotation);
            afterEffect.transform.SetParent(other.transform);
        }
        if (other.CompareTag("Character")) Destroy(gameObject);
        else Destroy(gameObject, 2f);
    }
}
