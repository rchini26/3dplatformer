using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Clothes
{
    public class ClothesUiMessage : MonoBehaviour
    {
        public TextMeshProUGUI messageText;
        public float displayDuration = 2f;

        private Coroutine _currentCoroutine;

        public void ShowMessage(string msg)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(ShowMessageRoutine(msg));

        }

        private IEnumerator ShowMessageRoutine(string msg)
        {
            messageText.text = msg;
            messageText.enabled = true;

            yield return new WaitForSeconds(displayDuration);

            messageText.enabled = false;
            _currentCoroutine = null;
        }
    }
}