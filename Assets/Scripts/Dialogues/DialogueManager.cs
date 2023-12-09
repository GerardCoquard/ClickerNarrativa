using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public List<GameObject> answers = new List<GameObject>();
    int currentAnswer;
    bool answered;
    bool answering;
    public TextMeshProUGUI speakerName;
    public Image speakerIcon;
    public NPC currentNPC;
    public GameObject dialogueRender;
    public TextMeshProUGUI dialogueText;
    private DialogueNode currentNode;
    private QuestionNode currentQuestion;
    [SerializeField] float defaultTypeSpeed;
    [SerializeField] float fastTypeSpeed;
    private float currentTypeSpeed;
    private DIALOGUE_STATE currentState = DIALOGUE_STATE.DEFAULT;
    int x;
    int y;
    private void Awake()
    {
        instance = this;
        dialogueRender.SetActive(false);
    }
    public void SetAnswers(List<string> _answers)
    {
        //Set the current answers buttons to the amount of answers there are in a question or node
        for (int i = 0; i < answers.Count; i++)
        {
            if(i<_answers.Count)
            {
                answers[i].SetActive(true);
                answers[i].GetComponent<TextMeshProUGUI>().text = _answers[i];
            }
            else
            {
                answers[i].SetActive(false);
            }
        }
    }
    public void Answered(int answer)
    {
        currentAnswer = answer;
        answered = true;
    }
    public void Interact()
    {
        if(answering) return;

        switch (currentState)
        {
            case DIALOGUE_STATE.DEFAULT:
                currentTypeSpeed = fastTypeSpeed;
                currentState = DIALOGUE_STATE.FAST;
                break;
            case DIALOGUE_STATE.FAST:
                ShowFullText();
                currentState = DIALOGUE_STATE.SKIP;
                break;
            case DIALOGUE_STATE.SKIP:
                currentTypeSpeed = defaultTypeSpeed;
                currentState = DIALOGUE_STATE.DEFAULT;
                NextSentence();
            break;
        }
    }
    public void StartDialogue(NPC newNPC, DialogueNode startNode)
    {
        currentNPC = newNPC;
        dialogueRender.SetActive(true);
        Clear();
        currentNode = startNode;
        speakerName.text = currentNPC.data._name;
        speakerIcon.sprite = currentNPC.data._icon;
        x = 0;
        y = 0;
        StartCoroutine(Type());
    }
    IEnumerator Type()
    {
        foreach (char letter in currentNode.text)
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(currentTypeSpeed);
        }
        currentState = DIALOGUE_STATE.SKIP;
    }

    IEnumerator Question()
    {
        List<string> _ans = new List<string>();
        foreach (DialogueAnswer item in currentQuestion.answers)
        {
            _ans.Add(item.text);
        }
        SetAnswers(_ans);
        answering = true;
        answered = false;

        while (!answered)
        {
            yield return null;
        }
        DialogueAnswer finalAnswer = currentQuestion.answers[currentAnswer];
        if(finalAnswer.x) x++;
        if(finalAnswer.y) y++;
        answering = false;
        SetAnswers(new List<string>());
        if(finalAnswer.nextNode != null)
        {
            currentNode = finalAnswer.nextNode;
            StartCoroutine(Type());
        }
        else EndDialogue();
    }

    private void NextSentence()
    {
        Clear();
        if (currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
            StartCoroutine(Type());
            return;
        }
        if (currentNode.nextQuestion != null)
        {
            currentQuestion = currentNode.nextQuestion;
            StartCoroutine(Question());
            return;
        }
        
        EndDialogue();
    }
    private void ShowFullText()
    {
        StopAllCoroutines();
        dialogueText.text = currentNode.text;
    }
    private void EndDialogue()
    {
        int targetIndex = x>y ? 0 : 1;
        if(currentNPC.HasAllUpgrades())
        {
            currentNPC.Offer(currentNPC.GetOffers()[targetIndex]);
        }
        else
        {
            currentNPC.Unblock(currentNPC.GetUpgrades()[targetIndex]);
        }

        dialogueRender.SetActive(false);
    }
    void Clear()
    {
        dialogueText.text = "";
        currentState = DIALOGUE_STATE.DEFAULT;
        currentTypeSpeed = defaultTypeSpeed;
        SetAnswers(new List<string>());
    }
}
public enum DIALOGUE_STATE
{
    DEFAULT,
    FAST,
    SKIP
}