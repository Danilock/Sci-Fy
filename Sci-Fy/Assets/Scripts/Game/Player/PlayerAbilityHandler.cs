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

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _dashAbility = GetComponent<Dash>();
    }

    private void Update()
    {
        if (!CanUseAbilities)
            return;

        if (Input.GetButtonDown("Dash"))
        {
            HandleDashInput();
        }
    }

    void HandleDashInput()
    {
        if (_player.StateMachine.CurrentState == _player.IdleState ||
               _player.StateMachine.CurrentState == _player.MovingState)
        {
            _dashAbility.TriggerAbility();
        }
    }
}
