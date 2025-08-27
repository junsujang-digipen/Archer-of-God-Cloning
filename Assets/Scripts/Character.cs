using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _hp;
    public float HP { get { return _hp; } set { _hp = value; } }
    float _baseYPosition;

    Attack _attackAction;
    Move _moveAction;

    public enum State { Attack, Moving, Skill, None }
    State _currentState;
    public State CurrentState { get { return _currentState; } set { _currentState = value; } }
    void Awake()
    {
        _baseYPosition = transform.position.y;
        CurrentState = State.Attack;
    }
    void Start()
    {
        _attackAction = GetComponent<Attack>();
        _moveAction = GetComponent<Move>();
    }
    void Update()
    {
        switch (_currentState)
        {
            case State.Attack:
                _attackAction.Do();
                break;
            case State.Moving:
                _attackAction.ResetTimer();
                _moveAction.Do();
                break;
            case State.Skill:
                _attackAction.ResetTimer();
                Skill(0);
                break;
        }
    }
    public void Move(float x)
    {
        _moveAction.Direction = x;
        CurrentState = State.Moving;
    }
    void Skill(int skillNumber)
    {
        Debug.Log("Skill!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Hit by Arrow");
            _hp -= other.GetComponent<Arrow>().Damage;
            if (_hp <= 0f)
            {
                Debug.Log("Dead");
                // Destroy(gameObject);
            }
        }
    }
}
