using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject interactionPrompt;
    public Text promptText;
    public GameObject collectionMessage;
    public Text collectionText;
    public Image collectionIcon;
    public GameObject temporaryMessage;
    public Text temporaryText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInteractionPrompt(string message)
    {
        interactionPrompt.SetActive(true);
        promptText.text = message;
    }

    public void HideInteractionPrompt()
    {
        interactionPrompt.SetActive(false);
    }

    public void ShowItemCollectedMessage(string message, Sprite icon)
    {
        collectionMessage.SetActive(true);
        collectionText.text = message;
        collectionIcon.sprite = icon;
        StartCoroutine(HideCollectedMessage());
    }

    IEnumerator HideCollectedMessage()
    {
        yield return new WaitForSeconds(2.5f);
        collectionMessage.SetActive(false);
    }

    public void ShowTemporaryMessage(string message, float duration = 3f)
    {
        temporaryMessage.SetActive(true);
        temporaryText.text = message;
        StartCoroutine(HideTemporaryMessage(duration));
    }

    IEnumerator HideTemporaryMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        temporaryMessage.SetActive(false);
    }
}

