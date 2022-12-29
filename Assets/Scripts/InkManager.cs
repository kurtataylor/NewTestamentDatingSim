using System.Collections.Generic;
using System.Linq;
using Ink.Parsed;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Choice = Ink.Runtime.Choice;
using Story = Ink.Runtime.Story;

public class InkManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJsonAsset;
    private Story _story;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private VerticalLayoutGroup choiceButtonContainer;
    [SerializeField]
    private Button choiceButtonPrefab;
    private List<Button> choiceButtonsInScene; 

    [SerializeField] 
    private Button continueButton;
    
    [SerializeField] 
    private List<TextTypeScriptableObject> textTypes;
   

    private void Start()
    {
        choiceButtonsInScene = new List<Button>();
        StartStory();
    }

    private void StartStory()
    {
        _story = new Story(inkJsonAsset.text);
        DisplayNextLine();
        RefreshChoiceView();
    }

    public void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            if (!continueButton.gameObject.activeSelf) continueButton.gameObject.SetActive(true);
            ApplyStyling();
            textMeshPro.text = _story.Continue()?.Trim(); // displays new text
            return;
        }
        
        if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    private void ApplyStyling()
    {
        if (_story.currentTags.Contains("thought"))
        {
            textMeshPro.color = textTypes.FirstOrDefault(t => t.type.ToLower() == "thought")?.color ?? Color.black;
            textMeshPro.fontStyle = FontStyles.Italic;
            return;
        }

        textMeshPro.color = textTypes.FirstOrDefault(t => t.type.ToLower() == "base")?.color ?? Color.black;
        textMeshPro.fontStyle = FontStyles.Normal;
    }

    private void DisplayChoices()
    {
        foreach (Button choice in choiceButtonsInScene)
        {
            choice.gameObject.SetActive(false);
        }    
        
        if (continueButton.gameObject.activeSelf) continueButton.gameObject.SetActive(false);
        
        // check if choices are already being displayed
        // if (choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (var i = 0; i < _story.currentChoices.Count; i++)
        {
            var choice = _story.currentChoices[i];
            var button = choiceButtonsInScene.Count >= i + 1
                ? choiceButtonsInScene[i]
                : CreateChoiceButton(choice.text); 
            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    public Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(choiceButtonPrefab, choiceButtonContainer.transform, false);
        
        
        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;
        
        return choiceButton;
    }

    private void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        _story.Continue();
        RefreshChoiceView();
        DisplayNextLine();
    }

    // Deactivate all the choice buttons.
    private void RefreshChoiceView()
    {
        foreach (var button in choiceButtonContainer.GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(false);
        }
    }

}
