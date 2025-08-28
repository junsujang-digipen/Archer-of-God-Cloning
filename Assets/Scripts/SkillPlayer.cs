using System;
using UnityEngine;

public class SkillPlayer : CharacterAction
{
    Vector3 _skillTargetPosition;
    bool _isAiming = true;
    Skill _skill;
    public Skill Skill
    {
        get { return _skill; }
        set
        {
            _skill = value;
        }
    }
    float _cooldownTimer;
    public float CooldownTimer { get { return _cooldownTimer; } }
    public bool IsAble => _cooldownTimer <= 0;
    float _intervalTimer = 0f;
    int _genCount = 0;
    Aiming _axis;
    public void Start()
    {
        _axis = GetComponentInChildren<Aiming>();
        _skillTargetPosition = GlobalVariables.CenterPosition; 
    }
    public override void Enter()
    {
        IsExitable = false;
        _isAiming = true;
        _genCount = 0;
        ResetInterval();
    }
    public override void Do()
    {
        Vector3 direction = Aiming.ComputeDirection(transform.position, _skillTargetPosition, _skill.IsStraight ? 0 : Physics2D.gravity.y, _skill.ArrowSpeed);
        _axis.Velocity = direction;
        if (_isAiming == false) // 마우스 드랍
        {
            if (_intervalTimer < 0f)
            {
                // 화살 생성
                GameObject arrow = Instantiate(_skill.ArrowPrefab);
                // 화살 초기 속도 설정
                Rigidbody2D _rigidbody2D = arrow.GetComponent<Rigidbody2D>();
                _rigidbody2D.linearVelocity = direction;
                if (_skill.IsStraight) _rigidbody2D.gravityScale = 0;
                arrow.transform.position = transform.position;
                CountArrow();
            }
            else
            {
                _intervalTimer -= GameTime.DeltaTime;
            }
        }
    }
    public override void Exit()
    {
        _axis.Velocity = GlobalVariables.CenterPosition - transform.position;
    }
    public void SetTargetPosition(Vector3 pos)
    {
        _skillTargetPosition = pos;
    }
    public void Shot()
    {
        _cooldownTimer = _skill.Cooldown;
        _isAiming = false;
        if (_skill.IsJump) GetComponent<Rigidbody2D>().AddForce(Vector2.up * Math.Abs(Physics2D.gravity.y) * _skill.GenInterval * _skill.GenCount, ForceMode2D.Impulse);
    }
    public void CoolDown()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
    void CountArrow()
    {
        ++_genCount;
        if (_genCount >= _skill.GenCount)
        {
            IsExitable = true;
        }
        else
        {
            ResetInterval();
        }
    }
    void ResetInterval()
    { 
        _intervalTimer = _skill.GenInterval;
    }
}
