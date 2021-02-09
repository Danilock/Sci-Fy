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
        private Dialog m_currentDialog;
        private Queue<Sentence> m_sentenceQueue = new Queue<Sentence>();
        #endregion

        #region DialogManager Events
        [SerializeField] UnityEvent OnSaySentence;


        [SerializeField] UnityEvent OnDialogStart;

        [SerializeField] UnityEvent OnDialogEnd;
        #endregion

        public void SetNewDialog(Dialog newDialog)
        {
            m_currentDialog = newDialog;

            m_sentenceQueue = EnqueSentence(m_currentDialog);

            m_currentDialog.OnDialogStart.Invoke();
            OnDialogStart?.Invoke();

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
            if (m_currentDialog == null)
                return;

            if (m_sentenceQueue.Count == 0)
            {
                EndConversation();
                return;
            }

            CurrentSentence = m_sentenceQueue.Dequeue();
            
            OnSaySentence?.Invoke();
        }

        private Queue<Sentence> EnqueSentence(Dialog dialogSentences)
        {
            Queue<Sentence> sentenceQueueInstance = new Queue<Sentence>();

            foreach (Sentence sentence in m_currentDialog.Sentences)
            {
                sentenceQueueInstance.Enqueue(sentence);
            }

            return sentenceQueueInstance;
        }

        public void EndConversation()
        {
            m_currentDialog.OnDialogEnd.Invoke();
            m_currentDialog = null;

            OnDialogEnd?.Invoke();
        }
    }
}