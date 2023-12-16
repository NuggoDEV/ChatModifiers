using CatCore.Models.Twitch.IRC;
using ChatModifiers.API;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace ChatModifiers.Utilities
{
    public class NotificationController : MonoBehaviour
    {
        [Inject] private CoreGameHUDController _coreGameHudController;
        private TextMeshProUGUI _notiText;
        private CanvasGroup _notiCanvasGroup;

        public static TextMeshProUGUI CreateNotiText(CoreGameHUDController gameHud)
        {
            var GO = new GameObject();
            GO.AddComponent<Canvas>();
            var text = GO.AddComponent<TextMeshProUGUI>();
            var canvasGroup = GO.AddComponent<CanvasGroup>();
            GO.transform.parent = gameHud.transform;

            text.text = "";
            text.fontSize = 9f;
            text.transform.position = new Vector3(0f, 2.35f, 7f);
            text.transform.localScale = new Vector3(0.035f, 0.035f, 0.025f);
            text.alignment = TextAlignmentOptions.Center;
            text.gameObject.SetActive(false);
            canvasGroup.alpha = 0;

            return text;
        }

        public void Awake()
        {
            Plugin.Log.Notice("HEY IM START");
            if (_notiText == null)
            {
                _notiText = CreateNotiText(_coreGameHudController);
                _notiCanvasGroup = _notiText.GetComponent<CanvasGroup>();
            }
            TwitchConnection.OnMessage += OnNotiTextChanged;
        }

        public void OnDisable()
        {
            TwitchConnection.OnMessage -= OnNotiTextChanged;
        }

        private IEnumerator FadeInText()
        {
            float duration = 0.4f;
            float elapsedTime = 0f;
            _notiText.gameObject.SetActive(true);
            while (elapsedTime < duration)
            {
                _notiCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _notiCanvasGroup.alpha = 1f;
        }

        private IEnumerator FadeOutText()
        {
            float duration = 0.5f;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                _notiCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _notiCanvasGroup.alpha = 0f;
            _notiText.gameObject.SetActive(false);
        }

        private void OnNotiTextChanged(TwitchMessage twitchMessage, CustomModifier customModifier)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInText());
            StartCoroutine(DelayedFadeOutText(5f));
            _notiText.text = $"<color={twitchMessage.Sender.Color}>{twitchMessage.Sender.UserName}</color> excecuted {customModifier.Name}";
            _notiText.richText = true;
        }

        private IEnumerator DelayedFadeOutText(float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FadeOutText());
        }
    }
}
