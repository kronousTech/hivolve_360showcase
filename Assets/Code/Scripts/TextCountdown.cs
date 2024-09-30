using System.Collections;
using TMPro;
using UnityEngine;

public class TextCountdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;

    private Animator _canvasAnimator;

    private string _baseText;

    private void OnEnable()
    {
        //VideosManager.OnSectionEndCooldown.AddListener(ShowText);
        //VideosManager.OnSectionEndCooldown.AddListener(() => StartCoroutine(SetCountdownText()));
        //VideosManager.OnVideoEnd.AddListener(HideText);
    }
    private void OnDisable()
    {
        //VideosManager.OnSectionEndCooldown.RemoveListener(ShowText);
        //VideosManager.OnSectionEndCooldown.RemoveListener(() => StartCoroutine(SetCountdownText()));
        //VideosManager.OnVideoEnd.RemoveListener(HideText);
    }
    private void Awake()
    {
        _canvasAnimator = GetComponent<Animator>();
        _baseText = _text.text;
    }
    private void ShowText()
    {
        _canvasAnimator.SetBool("State", true);
    }
    private void HideText()
    {
        _canvasAnimator.SetBool("State", false);
    }

    private IEnumerator SetCountdownText()
    {
        for (int i = 5; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);

            _text.text = _baseText + " " + i.ToString();
        }
    }
}