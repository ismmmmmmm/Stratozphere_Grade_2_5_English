using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class typewriterUI : MonoBehaviour
{
    TMP_Text _tmpProText;
    string _writer;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    void Start()
    {
        _tmpProText = GetComponent<TMP_Text>()!;
    }

    public IEnumerator TypeWriterTMP()
    {
        if (_tmpProText != null)
        {
            _writer = _tmpProText.text;
            _tmpProText.text = "";


            _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

            yield return new WaitForSeconds(delayBeforeStart);

            foreach (char c in _writer)
            {
                if (_tmpProText.text.Length > 0)
                {
                    _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
                }
                _tmpProText.text += c;
                _tmpProText.text += leadingChar;
                yield return new WaitForSeconds(timeBtwChars);
            }

            if (leadingChar != "")
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
        }
       
    }
}   