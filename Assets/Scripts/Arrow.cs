using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float Damage;
    public GameObject ArrowHead;
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
    [SerializeField]UnityEvent _hitAudio;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.CompareTag("Castle") || other.CompareTag("Character"))) return;
        Debug.Log("Hit " + other.name);
        _hitAudio?.Invoke();
        _rigidbody2D.linearVelocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.simulated = false;
        if (_afterEffect != null)
        {
            GameObject afterEffect = Instantiate(_afterEffect, ArrowHead.transform.position, transform.rotation);
            if (afterEffect.GetComponent<AfterEffect>() != null)
            { 
                Destroy(gameObject);
            }
            // afterEffect.transform.SetParent(other.transform);
        }
        if (other.CompareTag("Character")) Destroy(gameObject);
        else Destroy(gameObject, 2f);
    }
}
