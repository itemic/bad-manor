﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class AnviDialogue : CharacterDialogue
{

    private string characterName;
    private Dictionary<GameManager.GameState, string[]> storyLines = new Dictionary<GameManager.GameState, string[]>();
    private List<string> randomLines = new List<string>();

    private static AnviDialogue inst;

    public static AnviDialogue getInstance()
    {
        if (inst == null)
            inst = new AnviDialogue();

        return inst;
    }

    private AnviDialogue()
    {
        characterName = "Anvi";

        storyLines[GameManager.GameState.TUTORIAL_1] = new string[] { "Hey, what's up?", "Player", "Did you hear that Maurice used to be in love with Bertha? Apparently she rejected him because he was 40 years older than her. It’s a shame they didn’t marry, together they would have been the richest couple in the country!", characterName };
        storyLines[GameManager.GameState.TUTORIAL_2] = new string[] { "Hey, what's up?", "Player", "The debt collectors will surely be paying a visit to Sam soon. He’s been bleeding money for months at the blackjack table.", characterName };
        storyLines[GameManager.GameState.TUTORIAL_3] = new string[] { "Hey, what's up?", "Player", "I heard a rumor that Charles has been left off of Bertha’s will. Maybe my complaints have finally gotten through to her.", characterName };

        randomLines.Add("Do you think Charles will ever get a job? He’s so lazy.");
        randomLines.Add("What’s Charles doing again? Nothing.");
        randomLines.Add("Where’s Charles?");
        randomLines.Add("Sorry, not now. Charles is slacking off again and I need him to get a job.");
    }

    public string[] getStoryLines(GameManager.GameState state)
    {
        string[] value = null;
        if (storyLines.TryGetValue(state, out value))
        {
            return value;
        }
        return null;
    }

    public string getRandomLine()
    {
        int index = (int) Random.Range(0f, randomLines.Count - 1);
        return randomLines[index];
    }

}
