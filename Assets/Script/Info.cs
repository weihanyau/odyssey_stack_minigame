using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] GameObject menu;
    private Animator animator;
    private Button button;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        animator = menu.GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenMenu);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu()
    {
        source.Play();
        animator.SetTrigger("open");
    }
}
