using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public List<GameObject> answers = new List<GameObject>();
    public TextMeshProUGUI speakerName;
    public Image speakerIcon;
    public NPC currentNPC;
    public GameObject dialogueRender;
    public TextMeshProUGUI dialogueText;
    public float defaultTypeSpeed;
    public float fastTypeSpeed;
    public Sprite playerIcon;
    public Sprite narradorIcon;
    public UnityEvent onEndDialogue;
    DialogueNode currentNode;
    QuestionNode currentQuestion;
    float currentTypeSpeed;
    DIALOGUE_STATE currentState = DIALOGUE_STATE.DEFAULT;
    int currentAnswer;
    bool answered;
    bool answering;
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
        SetSpeaker();
        x = 0;
        y = 0;
        currentNode.text = SetText(currentNode.text);
        StartCoroutine(Type());
    }
    IEnumerator Type()
    {
        for (int i = 0; i < currentNode.text.Length; i++)
        {
            char letter = currentNode.text[i];
            if(letter == '<')
            {
                dialogueText.text += Utilities.ToCurrencyType("");
                i+=15;
            }
            else dialogueText.text += letter;
            yield return new WaitForSeconds(currentTypeSpeed);
        }
        /*foreach (char letter in currentNode.text)
        {
            if(letter == '<')
            {
                dialogueText.text += 
            }
            else dialogueText.text += letter;
            yield return new WaitForSeconds(currentTypeSpeed);
        }*/
        currentState = DIALOGUE_STATE.SKIP;
    }

    IEnumerator Question()
    {
        speakerName.text = "Jugador";
        speakerIcon.sprite = playerIcon;

        List<string> _ans = new List<string>();
        foreach (DialogueAnswer item in currentQuestion.answers)
        {
            _ans.Add(SetText(item.text));
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
        if(currentQuestion.finalQuestion)
        {
            if(x>y)
            {
                currentNode = currentQuestion.xNode;
                StartCoroutine(Type());
            }
            else
            {
                currentNode = currentQuestion.yNode;
                SetSpeaker();
                currentNode.text = SetText(currentNode.text);
                StartCoroutine(Type());
            }
        }
        else if(finalAnswer.nextNode != null)
        {
            currentNode = finalAnswer.nextNode;
            SetSpeaker();
            currentNode.text = SetText(currentNode.text);
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
            SetSpeaker();
            currentNode.text = SetText(currentNode.text);
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
        dialogueText.text = SetText(currentNode.text);
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
        onEndDialogue?.Invoke();
        dialogueRender.SetActive(false);
    }
    void Clear()
    {
        dialogueText.text = "";
        currentState = DIALOGUE_STATE.DEFAULT;
        currentTypeSpeed = defaultTypeSpeed;
        SetAnswers(new List<string>());
    }
    string SetText(string oldText)
    {
        string newText = oldText.Replace("Diuras","<sprite index=0>");
        newText = newText.Replace("Almas","<sprite index=1>");
        return  newText.Replace("Eter","<sprite index=2>");
    }
    void SetSpeaker()
    {
        if(currentNode.player)
        {
            speakerName.text = "Jugador";
            speakerIcon.sprite = playerIcon;
            dialogueText.fontStyle = FontStyles.Normal;
            return;
        }
        if(currentNode.narrador)
        {
            speakerName.text = "";
            speakerIcon.sprite = narradorIcon;
            dialogueText.fontStyle = FontStyles.Italic;
            return;
        }

        speakerName.text = currentNPC.data._name;
        speakerIcon.sprite = currentNPC.data._icon;
        dialogueText.fontStyle = FontStyles.Normal;
    }
}
public enum DIALOGUE_STATE
{
    DEFAULT,
    FAST,
    SKIP
}