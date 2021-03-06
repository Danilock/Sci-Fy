using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DialogSystem
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private bool _ignoreScaleTime = false;
        public bool IgnoreScaleTime{
            get => _ignoreScaleTime;
            set => _ignoreScaleTime = value;
        }
        public UnityEvent OnDialogStart, OnDialogEnd;
        public Sentence[] Sentences;
    }
}