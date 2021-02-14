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
        {
            _player.AnimationHandler.TriggerAttackAnimation(_meleeAttackIndex);
            _meleeAttackIndex = (_meleeAttackIndex + 1) % 3;
        }
            
    }

    private bool CheckIfPlayerIsInIdleOrWalkState()
    {
        return _player.StateMachine.CurrentState == _player.IdleState ||
               _player.StateMachine.CurrentState == _player.WalkState;
    }

    public void StartMeleeAttack() => _meleeWeapon.StartAttack();
    public void EndMeleeAttack() => _meleeWeapon.EndAttack();
}
