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

            _dialogManager.OnSaySentence.AddListener(UpdateDialogUI);
            _dialogManager.OnDialogStart.AddListener(ShowDialog);
            _dialogManager.OnDialogEnd.AddListener(HideDialog);
        }

        private void OnDisable()
        {
            if (_dialogManager == null)
                return;

            _dialogManager.OnSaySentence.RemoveListener(UpdateDialogUI);
            _dialogManager.OnDialogStart.RemoveListener(ShowDialog);
            _dialogManager.OnDialogEnd.RemoveListener(HideDialog);
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

                if(_dialogManager.CurrentDialog.IgnoreScaleTime)
                    yield return new WaitForSecondsRealtime(.03f);
                else
                    yield return new WaitForSeconds(.03f);
            }

            _continueButton.interactable = true;
            _buttonGroup.LeanAlpha(1f, .3f);
        }

        //TODO: Make an animation!
        private void ShowDialog() => DialogVisible(1f, .3f, _dialogManager.CurrentDialog.IgnoreScaleTime);
        private void HideDialog() => DialogVisible(0f, .3f, _dialogManager.CurrentDialog.IgnoreScaleTime);

        private void DialogVisible(float to, float time, bool ignoreScaleTime)
        {
            if(ignoreScaleTime)
                LeanTween.dtManual = Time.unscaledDeltaTime;

            _dialogUI.LeanAlpha(to, time).useManualTime = ignoreScaleTime;
        }
    }
}