using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pop : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Button button;
    private RockController rockController;
    private TextMeshProUGUI textMesh;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        rockController = character.GetComponent<RockController>();
        textMesh = GetComponent<TextMeshProUGUI>();
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
        topRock.GetComponent<Animator>().SetTrigger("pop");
        topRock.GetComponent<Transform>().position = new Vector3(rockController.GetRockInitialPosition()[int.Parse(topRock.name[5].ToString()) - 1].x, rockController.GetRockInitialPosition()[int.Parse(topRock.name[5].ToString()) - 1].y, 0);
        rockController.Pop();
    }
}
