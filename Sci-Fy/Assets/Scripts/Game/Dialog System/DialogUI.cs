using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DialogSystem
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogManager _dialogManager;
        [SerializeField] private Text _characterName;
        [SerializeField] private Text _sentenceBody;

        private void Awake()
        {
            if(_dialogManager == null)
                _dialogManager = GetComponentInParent<DialogManager>();
        }

        private void OnEnable()
        {
            if (_dialogManager == null)
                return;

            _dialogManager.OnDialogStart.AddListener(ShowDialog);
            _dialogManager.OnDialogEnd.AddListener(HideDialog);
            _dialogManager.OnSaySentence.AddListener(UpdateDialogUI);
        }

        private void OnDisable()
        {
            if (_dialogManager == null)
                return;

            _dialogManager.OnDialogStart.RemoveListener(ShowDialog);
            _dialogManager.OnDialogEnd.RemoveListener(HideDialog);
            _dialogManager.OnSaySentence.RemoveListener(UpdateDialogUI);
        }

        public void UpdateDialogUI()
        {
            _characterName.text = _dialogManager.CurrentSentence.Character;

            StartCoroutine(ShowBodySentenceLetterByLetter(_dialogManager.CurrentSentence.Body));
        }

        IEnumerator ShowBodySentenceLetterByLetter(string bodyText)
        {
            string body = "";

            foreach(char letter in bodyText)
            {
                body += letter;
                _sentenceBody.text = body;
                yield return null;
            }
        }

        //TODO: Make an animation!
        private void ShowDialog() => gameObject.SetActive(true);
        private void HideDialog() => gameObject.SetActive(false);
    }
}