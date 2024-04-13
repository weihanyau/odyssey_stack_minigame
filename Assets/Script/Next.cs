using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject storyText;
    [SerializeField] private GameObject howToPlayText;
    private TextMeshProUGUI storyTextMesh;
    private TextMeshProUGUI howToPlayTextMesh;
    private TextMeshProUGUI buttonText;
    private Button button;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        storyTextMesh = storyText.GetComponent<TextMeshProUGUI>();
        howToPlayTextMesh = howToPlayText.GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(NextPage);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextPage()
    {
        source.Play();
        storyText.SetActive(false);
        howToPlayText.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(BackPage);
        buttonText.text = "Back";
    }

    public void BackPage()
    {
        source.Play();
        storyText.SetActive(true);
        howToPlayText.SetActive(false);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(NextPage);
        buttonText.text = "Next";
    }
}
