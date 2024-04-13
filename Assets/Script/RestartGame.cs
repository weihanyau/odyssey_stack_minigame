using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RestartGame : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject storyText;
    [SerializeField] private GameObject howToPlayText;
    [SerializeField] private GameObject next;
    private TextMeshProUGUI storyTextMesh;
    private TextMeshProUGUI howToPlayTextMesh;
    private RockController rockController;
    private Animator startAnimator;
    private Animator endAnimator;
    private Button button;
    private Button nextButton;
    private TextMeshProUGUI nextButtonText;
    private Next nextController;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        rockController = character.GetComponent<RockController>();
        startAnimator = startScreen.GetComponent<Animator>();
        endAnimator = endScreen.GetComponent<Animator>();
        storyTextMesh = storyText.GetComponent<TextMeshProUGUI>();
        howToPlayTextMesh = howToPlayText.GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Reset);
        nextButtonText = next.GetComponentInChildren<TextMeshProUGUI>();
        nextButton = next.GetComponentInChildren<Button>();
        nextController = next.GetComponent<Next>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset()
    {
        source.Play();
        if (!storyText.activeSelf)
            storyText.SetActive(true);
        if (howToPlayText.activeSelf)
            howToPlayText.SetActive(false);
        endAnimator.SetTrigger("close");
        startAnimator.SetTrigger("open");
        rockController.HardReset();
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(nextController.NextPage);
        nextButtonText.text = "Next";
    }
}
