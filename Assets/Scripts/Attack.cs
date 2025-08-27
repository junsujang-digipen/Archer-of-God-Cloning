using UnityEngine;

public class Attack : CharacterAction
{
    [SerializeField] GameObject _targetSide;
    SearchTarget _searchTarget;
    [SerializeField] float _attackDelay;
    float _attackDelayTimer;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Aiming _aiming;
    [SerializeField] GameObject _bow;
    void Start()
    {
        _searchTarget = _targetSide.GetComponent<SearchTarget>();
        ResetTimer();
    }
    public override void Do()
    {
        if (_attackDelayTimer <= 0f)
        {
            ResetTimer();
            Debug.Log("Attack!");
            // 화살 생성
            GameObject arrow = Instantiate(_arrowPrefab);
            // 화살 초기 속도 설정
            Rigidbody2D _rigidbody2D = arrow.GetComponent<Rigidbody2D>();
            float gravity = Physics2D.gravity.y * _rigidbody2D.gravityScale;
            // 속도 구하기
            Vector3 direction = ComputeDirection(_searchTarget.GetTarget().transform.position, gravity);
            _rigidbody2D.linearVelocity = direction;
            // 활 회전 설정
            _aiming.Velocity = direction;
            arrow.transform.position = _bow.transform.position;
        }
        else
        {
            Vector3 direction = ComputeDirection(_searchTarget.GetTarget().transform.position, Physics2D.gravity.y);
            _aiming.Velocity = direction;
            _attackDelayTimer -= GameTime.DeltaTime;
        }
    }
    public void ResetTimer()
    {
        _attackDelayTimer = _attackDelay;
    }
    Vector3 ComputeDirection(Vector3 targetPosition, float gravity)
    {
        Vector3 direction = targetPosition - transform.position;
        float t = 1.0f; // 임시 시간초, 추후 거리 기반으로 변경?
        direction.x /= t;
        direction.y -= 0.5f * gravity * t * t;
        direction.y /= t;
        return direction;
    }
}
