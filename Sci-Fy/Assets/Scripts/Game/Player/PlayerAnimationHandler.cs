using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _animatorController;

    private readonly int _idleAnimation = Animator.StringToHash("Idle");
    private readonly int _walkAnimation = Animator.StringToHash("Walk");
    private readonly int _jumpAnimation = Animator.StringToHash("Jump");


    // Start is called before the first frame update
    void Awake()
    {
        _animatorController = GetComponent<Animator>();    
    }
    public void TriggerIdleAnimation()
    {
        if(_animatorController == null)
        {
            Debug.LogWarning("No animator on player!");
            return;
        }
        _animatorController.Play(_idleAnimation);
    }
    public void TriggerWalkAnimation()
    {
        if (_animatorController == null)
        {
            Debug.LogWarning("No animator on player!");
            return;
        }
        _animatorController.Play(_walkAnimation);
    }

    public void TriggerJumpAnimation()
    {
        if (_animatorController == null)
        {
            Debug.LogWarning("No animator on player!");
            return;
        }
        _animatorController.Play(_jumpAnimation);
    }

    public void TriggerAttackAnimation(int attackIndex)
    {
        _animatorController.Play("Attack" + attackIndex);
    }
}
