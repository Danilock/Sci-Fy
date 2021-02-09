using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DialogSystem
{
    [System.Serializable]
    public class Sentence
    {
        public string Character;
        [TextArea] public string Body;
        public UnityEvent OnSaySentence;
    }
}