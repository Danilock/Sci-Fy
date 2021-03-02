using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.DialogSystem
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogManager _dialogManager;
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _sentenceBody;
        [SerializeField] private Button _continueButton;

        private CanvasGroup _dialogUI;
        private CanvasGroup _buttonGroup;

        private void Awake()
        {
            if(_dialogManager == null)
                _dialogManager = GetComponentInParent<DialogManager>();

            _buttonGroup = _continueButton.GetComponent<CanvasGroup>();
            _dialogUI = GetComponent<CanvasGroup>();
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

            _continueButton.interactable = false;
            _buttonGroup.LeanAlpha(0f, .3f);

            foreach(char letter in bodyText)
            {
                body += letter;
                _sentenceBody.text = body;
                yield return new WaitForSeconds(.03f);
            }

            _continueButton.interactable = true;
            _buttonGroup.LeanAlpha(1f, .3f);
        }

        //TODO: Make an animation!
        private void ShowDialog() => _dialogUI.LeanAlpha(1f, .3f);
        private void HideDialog() => _dialogUI.LeanAlpha(0f, .3f);
    }
}