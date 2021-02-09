using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DialogSystem
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogManager m_dialogManager;
        [SerializeField] private Text m_characterName;
        [SerializeField] private Text m_sentenceBody;

        private void Awake()
        {
            if(m_dialogManager == null)
                m_dialogManager = GetComponentInParent<DialogManager>();
        }

        public void UpdateDialogUI()
        {
            m_characterName.text = m_dialogManager.CurrentSentence.Character;

            StartCoroutine(ShowBodySentenceLetterByLetter(m_dialogManager.CurrentSentence.Body));
        }

        IEnumerator ShowBodySentenceLetterByLetter(string bodyText)
        {
            string body = "";

            foreach(char letter in bodyText)
            {
                body += letter;
                m_sentenceBody.text = body;
                yield return null;
            }
        }
    }
}