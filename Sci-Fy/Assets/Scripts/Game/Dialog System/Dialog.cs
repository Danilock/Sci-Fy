using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DialogSystem
{
    public class Dialog : MonoBehaviour
    {
        public UnityEvent OnDialogStart, OnDialogEnd;
        public Sentence[] Sentences;
    }
}