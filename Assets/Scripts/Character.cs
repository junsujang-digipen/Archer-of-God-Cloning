using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _hp;
    public float HP { get { return _hp; } set { _hp = value; } }
    float _baseYPosition;

    Attack _attackAction;
    Move _moveAction;
    SkillSet _skillSet;
    public SkillPlayer CurrentSkillPlayer
    {
        get { return _skillSet.CurrentSkillPlayer; }
    }

    public enum State { Attack, Moving, Skill, None }
    State _currentState = State.None;
    CharacterAction _currentAction;
    public State CurrentState
    {
        get { return _currentState; }
        set
        {
            if(_currentState == value) return;
            if (_currentAction?.IsExitable == false) return;
            _currentAction?.Exit();
            _currentState = value;
            switch (_currentState)
            {
                case State.Attack:
                    _currentAction = _attackAction;
                    break;
                case State.Moving:
                    _currentAction = _moveAction;
                    break;
                case State.Skill:
                    _currentAction = CurrentSkillPlayer;
                    break;
                case State.None:
                    _currentAction = _attackAction;
                    break;
            }
            _currentAction.Enter();
        }
    }
    void Awake()
    {
        _baseYPosition = transform.position.y;
    }
    void Start()
    {
        _attackAction = GetComponent<Attack>();
        _moveAction = GetComponent<Move>();
        _skillSet = GetComponent<SkillSet>();
        CurrentState = State.Attack;
    }
    void Update()
    {
        _currentAction.Do();
        if(_currentAction.IsExitable == true)
        {
            CurrentState = State.Attack;
        }
    }
    public void Move(float x)
    {
        if (x == 0) _moveAction.IsExitable = true;
        else
        {
            _moveAction.Direction = x;
            CurrentState = State.Moving;
        }
    }
    public void Skill(int skillIdx)
    {
        _currentAction.IsExitable = true;
        _skillSet.CurrSkillIdx = skillIdx;
        CurrentState = State.Skill;
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
