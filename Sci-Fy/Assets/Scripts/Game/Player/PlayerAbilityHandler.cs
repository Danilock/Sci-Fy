using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Ability;

public class PlayerAbilityHandler : MonoBehaviour
{
    private PlayerController _player;

    #region Ability Usage
    [SerializeField] private bool _canUseAbilities = true;
    public bool CanUseAbilities
    {
        get
        {
            return _canUseAbilities;
        }
        set
        {
            _canUseAbilities = value;
        }
    }
    #endregion

    private Dash _dashAbility;
    private Laser _laserAbility;
    [SerializeField] private MeleeWeapon _meleeWeapon;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _dashAbility = GetComponent<Dash>();
        _laserAbility = GetComponent<Laser>();
    }

    private void Update()
    {
        if (!CanUseAbilities)
            return;

        if (Input.GetButtonDown("Dash"))
        {
            HandleDashInput();
        }
        else if (Input.GetButtonDown("Laser"))
        {
            HandleLaserInput();
        }

        else if(Input.GetButtonDown("Melee Attack"))
        {
            //TODO: Actually, what this needs to do is call the animator and then
                  //set the Start attack method in the animation as an event.
            HandleMeleeAttack();
        }
    }

    void HandleDashInput()
    {
        if (CheckIfPlayerIsInIdleOrWalkState() || _player.StateMachine.CurrentState == _player.JumpState)
            _dashAbility.TriggerAbility();
    }

    void HandleLaserInput()
    {
        if (CheckIfPlayerIsInIdleOrWalkState())
            _laserAbility.TriggerAbility();
    }

    void HandleMeleeAttack()
    {
        if (CheckIfPlayerIsInIdleOrWalkState())
            _meleeWeapon.StartAttack();
    }

    private bool CheckIfPlayerIsInIdleOrWalkState()
    {
        return _player.StateMachine.CurrentState == _player.IdleState ||
               _player.StateMachine.CurrentState == _player.MovingState;
    }
}
