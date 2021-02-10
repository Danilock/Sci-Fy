using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DialogSystem
{
    public class DialogManager : MonoBehaviour
    {
        #region Dialog behaviour
        public Sentence CurrentSentence { get; private set; }
        private Dialog _currentDialog;
        private Queue<Sentence> _sentenceQueue = new Queue<Sentence>();
        #endregion

        #region DialogManager Events
        public UnityEvent OnSaySentence;


        public UnityEvent OnDialogStart;

        public UnityEvent OnDialogEnd;
        #endregion

        public void SetNewDialog(Dialog newDialog)
        {
            _currentDialog = newDialog;

            _sentenceQueue = EnqueSentence(_currentDialog);

            _currentDialog.OnDialogStart.Invoke();
            OnDialogStart.Invoke();

            SayNextSentence();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SayNextSentence();
            }
        }

        public void SayNextSentence()
        {
            if (_currentDialog == null)
                return;

            if (_sentenceQueue.Count == 0)
            {
                EndConversation();
                return;
            }

            CurrentSentence = _sentenceQueue.Dequeue();
            
            OnSaySentence?.Invoke();
        }

        private Queue<Sentence> EnqueSentence(Dialog dialogSentences)
        {
            Queue<Sentence> sentenceQueueInstance = new Queue<Sentence>();

            foreach (Sentence sentence in _currentDialog.Sentences)
            {
                sentenceQueueInstance.Enqueue(sentence);
            }

            return sentenceQueueInstance;
        }

        public void EndConversation()
        {
            _currentDialog.OnDialogEnd.Invoke();
            _currentDialog = null;

            OnDialogEnd.Invoke();
        }
    }
}