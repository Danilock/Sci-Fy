using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DialogSystem{

    [RequireComponent(typeof(Collider2D))]
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private Dialog _dialogToInitiate;
        [SerializeField] private bool _stopPlayer;
        private DialogManager _dgManager;

        [Header("Time Changer")]
        [SerializeField] private bool _changeTimeScaleWhenTrigger;
        [SerializeField] private float _newScaleValue;
        private PlayerController _player;

        private void Start() {
            _dgManager = FindObjectOfType<DialogManager>();
            _player = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                _dgManager.SetNewDialog(_dialogToInitiate);

                if(_stopPlayer)
                    _player.SetPlayerState("NPC");

                if(_changeTimeScaleWhenTrigger)
                    Time.timeScale = _newScaleValue;
            }
        }
    }
}