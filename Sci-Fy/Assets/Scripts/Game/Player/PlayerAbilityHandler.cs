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
        get => _canUseAbilities;
        set => _canUseAbilities = value;
    }
    #endregion
    
    private Dash _dashAbility;
    private Laser _laserAbility;
    [SerializeField] private MeleeWeapon _meleeWeapon;
    [SerializeField] private bool _canUseMeleeAttacks;
    public bool CanUseMeleeAttacks
    {
        get => _canUseMeleeAttacks;
        set => _canUseMeleeAttacks = value;
    }
    int _meleeAttackIndex = 1;

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
            HandleDashInput();

        else if (Input.GetButtonDown("Laser"))
            HandleLaserInput();

        else if(Input.GetButtonDown("Melee Attack"))
            HandleMeleeAttack();
    }

    private void HandleDashInput()
    {
        if (CheckIfPlayerIsInIdleOrWalkState() || _player.StateMachine.CurrentState == _player.JumpState)
            _dashAbility.TriggerAbility();
    }

    private void HandleLaserInput()
    {
        if (CheckIfPlayerIsInIdleOrWalkState())
        {
            _laserAbility.TriggerAbility();
            StopPlayerMovement();
        }
    }

    private void HandleMeleeAttack()
    {
        if(!CanUseMeleeAttacks)
            return;

        if (CheckIfPlayerIsInIdleOrWalkState() || _player.StateMachine.CurrentState == _player.JumpState)
        {
            _player.AnimationHandler.TriggerAttackAnimation(_meleeAttackIndex);

            if (_meleeAttackIndex == 2)
                _meleeAttackIndex = 1;
            else
                _meleeAttackIndex++;
        }
            
    }

    private void StopPlayerMovement()
    {
        _player.Rigidbody.velocity = Vector2.zero;
    }

    private bool CheckIfPlayerIsInIdleOrWalkState()
    {
        return _player.StateMachine.CurrentState == _player.IdleState ||
               _player.StateMachine.CurrentState == _player.WalkState;
    }

    public void StartMeleeAttack() => _meleeWeapon.StartAttack();
    public void EndMeleeAttack() => _meleeWeapon.EndAttack();
}
