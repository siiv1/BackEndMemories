using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NpcDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;

    void Start()

    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()

    {
        //to Start Dialogue
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }
        //to Quit dialogue
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {

        dialogueText.text = "";

        index = 0;

        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {

        foreach (char letter in dialogue[index].ToCharArray())

        {

            dialogueText.text += letter;

            yield return new WaitForSeconds(wordSpeed);

        }

    }

    public void NextLine()

    {

        if (index < dialogue.Length - 1)

        {

            index++;

            dialogueText.text = "";

            StartCoroutine(Typing());

        }

        else

        {

            RemoveText();

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)

    {
        if (other.CompareTag("Player"))
        {

            playerIsClose = false;

            RemoveText();

        }
    }

}
