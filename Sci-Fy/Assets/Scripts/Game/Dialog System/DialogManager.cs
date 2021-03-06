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
        public Dialog CurrentDialog;
        private Queue<Sentence> _sentenceQueue = new Queue<Sentence>();
        #endregion

        #region DialogManager Events
        public UnityEvent OnSaySentence;


        public UnityEvent OnDialogStart;

        public UnityEvent OnDialogEnd;
        #endregion

        public void SetNewDialog(Dialog newDialog)
        {
            CurrentDialog = newDialog;

            _sentenceQueue = EnqueSentence(CurrentDialog);

            CurrentDialog.OnDialogStart.Invoke();
            OnDialogStart.Invoke();

            SayNextSentence();
        }

        public void SayNextSentence()
        {
            if (CurrentDialog == null)
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

            foreach (Sentence sentence in CurrentDialog.Sentences)
            {
                sentenceQueueInstance.Enqueue(sentence);
            }

            return sentenceQueueInstance;
        }

        public void EndConversation()
        {
            CurrentDialog.OnDialogEnd.Invoke();
            CurrentDialog = null;

            OnDialogEnd.Invoke();
        }
    }
}