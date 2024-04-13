using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] private GameObject congratsScreen;
    [SerializeField] private GameObject endScreen;
    private AudioSource source;
    private Animator animator;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        animator = menu.GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(CloseMenu);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseMenu()
    {
        source.Play();
        animator.SetTrigger("close");
    }
}
