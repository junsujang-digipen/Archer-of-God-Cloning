using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] Character target;
    InputAction moveAction;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        if (moveAction.IsPressed() == true)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            if (input != null)
            {
                // Debug.Log("Move target");
                target.Move(input.x);
            }
        }
        else
        {
            target.StopMove();
            // target.CurrentState = Character.State.Attack;
        }
    }

    // UI interaction functions
    public Character Target { get { return target; } }
    public bool IsSkillable { get { return target.IsSkillable; } }
    public void PlaySkill(int skillNumber)
    {
        target.Skill(skillNumber);
    }
    public void Aiming(Vector3 targetPosition)
    {
        target.CurrentSkillPlayer.SetTargetPosition(targetPosition);
    }
    public void Shot()
    {
        target.CurrentSkillPlayer.Shot();
    }
}
