using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NpcDialogue : MonoBehaviour
{
    //Managing the speaker and lines
    [System.Serializable]
    public struct DialogueLine
    {
        public string speaker;
        public string text;
        public Sprite portrait; // Optional
    }
    /*public string speakerName;
    public Sprite speakerPortrait;
    */
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameTxt; //assign in Inspector
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public DialogueLine[] dialogueLines;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;

    void Start()

    {
        if (dialogueText != null)
            dialogueText.text = string.Empty;
        if (nameTxt != null)
            nameTxt.text = string.Empty;
    }
    

    // Update is called once per frame
    void Update()
    {

        //to Start lines
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            // Start new dialogue
            {
                dialoguePanel.SetActive(true);
                PlayerMovement.isUIOpen = true;
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[index].text)
            {
                // Continue to next line
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index].text;
                PlayerMovement.isUIOpen = false;
            }
        }
        //to Quit lines
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    
    void StartDialogue()
    {
        //to Start Dialogue
        index = 0;
        UpdateSpeakerInfo();
        StartCoroutine(Typing());
    }
    public void RemoveText()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        nameTxt.text = "";
        index = 0;
        PlayerMovement.isUIOpen = false;
        dialoguePanel.SetActive(false);
    }

    public void NextLine()

    {

        if (index < dialogueLines.Length - 1)

        {

            index++;

            dialogueText.text = string.Empty;
            UpdateSpeakerInfo();
            StartCoroutine(Typing());

        }

        else

        {
            RemoveText();
        }

    }
    void UpdateSpeakerInfo()
    {
        nameTxt.text = dialogueLines[index].speaker;
        if (portraitImage != null)
            portraitImage.sprite = dialogueLines[index].portrait;
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueLines[index].text.ToCharArray())

        {

            dialogueText.text += letter;

            yield return new WaitForSeconds(wordSpeed);

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
        if ((other.CompareTag("Player"))&&other!=null)
        {

            playerIsClose = false;

            RemoveText();

        }
    }

}
