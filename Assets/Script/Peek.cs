using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Peek : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject numDialogText;
    [SerializeField] private Button button;
    private RockController rockController;
    private TextMeshProUGUI textMeshProUGUI;
    private Animator animator;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        rockController = character.GetComponent<RockController>();
        textMeshProUGUI = numDialogText.GetComponentInChildren<TextMeshProUGUI>();
        animator = numDialogText.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rockController.GetWon() && button.IsInteractable())
        {
            button.interactable = false;
        }
        else if (!rockController.GetWon() && !button.IsInteractable())
        {
            button.interactable = true;
        }
    }

    public void Clicked()
    {
        source.Play();
        List<GameObject> rockResult = rockController.GetRockResult();
        if (rockResult.Count <= 0)
        {
            return;
        }
        GameObject topRock = rockResult[rockResult.Count - 1];
        string topRockNumber = topRock.name[5].ToString();
        textMeshProUGUI.text = "The top rock is numbered '" + topRockNumber + "'!";
        animator.SetTrigger("entry");
    }
}
