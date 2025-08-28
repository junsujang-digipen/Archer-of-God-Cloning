using UnityEngine;

public class Attack : CharacterAction
{
    [SerializeField] GameObject _targetSide;
    SearchTarget _searchTarget;
    [SerializeField] float _attackDelay;
    float _attackDelayTimer;
    [SerializeField] GameObject _arrowPrefab;
    Aiming _axis;
    Vector3 direction;
    void Start()
    {
        IsExitable = true;
        _searchTarget = _targetSide.GetComponent<SearchTarget>();
        _axis = GetComponentInChildren<Aiming>();
    }
    public override void Enter()
    {
        ResetTimer();
    }
    public override void Do()
    {
        // 활 회전 설정
        GameObject target = _searchTarget.GetTarget();
        direction = Aiming.ComputeDirection(transform.position, target.transform.position, Physics2D.gravity.y);
        _axis.Velocity = direction;
        if (_attackDelayTimer <= 0f)
        {
            ResetTimer();
            Debug.Log("Attack!");
            // 화살 생성
            GameObject arrow = Instantiate(_arrowPrefab);
            // 화살 초기 속도 설정
            Rigidbody2D _rigidbody2D = arrow.GetComponent<Rigidbody2D>();
            _rigidbody2D.linearVelocity = direction;

            arrow.transform.position = transform.position;
        }
        else
        {
            _attackDelayTimer -= GameTime.DeltaTime;
        }
    }
    public override void Exit()
    {
        ResetTimer();
        _axis.Velocity = GlobalVariables.CenterPosition - transform.position;
    }
    private void ResetTimer()
    {
        _attackDelayTimer = _attackDelay;
    }
}
