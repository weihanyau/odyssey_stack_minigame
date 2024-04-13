using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Push : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject numDialogText;
    [SerializeField] private Button button;
    private TextMeshProUGUI textMeshProUGUI;
    private RockController rockController;
    private AudioSource source;

    private Animator animator;
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
        GameObject nextRock = rockController.GetNextRock();
        if (rockController.GetWon() && button.IsInteractable())
        {
            button.interactable = false;
            rockController.GetCharAnimator().SetTrigger("idle");
        }
        else if (!rockController.GetWon() && !button.IsInteractable())
        {
            button.interactable = true;
        }
        if (nextRock == null)
        {
            return;
        }
        if (rockController.GetWon())
        {
            nextRock.SetActive(false);
        }
        else
        {
            nextRock.SetActive(true);
        }
    }

    public void Clicked()
    {
        source.Play();
        GameObject nextRock = rockController.GetNextRock();
        if (nextRock == null)
        {
            textMeshProUGUI.text = "Out of rock, try again!";
            animator.SetTrigger("entry");
        }
        Animator currRockAnimator = nextRock.GetComponent<Animator>();
        List<GameObject> rockResult = rockController.GetRockResult();
        int rockResultSize = 8 - rockResult.Count;

        if (rockResult.Count == 0 || int.Parse(rockResult[rockResult.Count - 1].name[5].ToString()) > int.Parse(nextRock.name[5].ToString()))
        {
            currRockAnimator.SetTrigger("base" + rockResultSize.ToString());
            rockController.Push();
        }
        else
        {
            textMeshProUGUI.text = "Can't push on to a rock with smaller number!";
            animator.SetTrigger("entry");
        }
    }
}
