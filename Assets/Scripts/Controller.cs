using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] Character target;
    InputAction moveAction;
    void Start() { moveAction = InputSystem.actions.FindAction("Move"); }
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
            target.CurrentState = Character.State.Attack;
        }
    }

    public void PlaySkill(int skillNumber)
    {
        // target.CurrentState = Character.State.Skill;
        Debug.Log("Skill " + skillNumber + " played");
    }
}
