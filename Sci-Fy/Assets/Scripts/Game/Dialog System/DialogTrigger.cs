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

        private void Start() {
            _dgManager = FindObjectOfType<DialogManager>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                _dgManager.SetNewDialog(_dialogToInitiate);

                if(_stopPlayer)
                    FindObjectOfType<PlayerController>().SetPlayerState("NPC");

                if(_changeTimeScaleWhenTrigger)
                    Time.timeScale = _newScaleValue;
            }
        }
    }
}