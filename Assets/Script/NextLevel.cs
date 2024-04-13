using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject congratsScreen;
    private RockController rockController;
    private Animator congratsAnimator;
    private AudioSource source;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        rockController = character.GetComponent<RockController>();
        congratsAnimator = congratsScreen.GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(GoNextLevel);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoNextLevel()
    {
        source.Play();
        congratsAnimator.SetTrigger("close");
        rockController.HardReset();
    }
}
