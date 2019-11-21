using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TypingScript : MonoBehaviour {
    public Player player;


    //sounds

    [SerializeField]
    AudioSource typeSound;
    [SerializeField]
    AudioSource incorrectTypeSound;
    bool wasIncorrectKeyDown;
    [SerializeField]
    AudioSource completeSound;
    [SerializeField]
    AudioSource increaseDifficultySound;
    [SerializeField]
    float pitchVariation = 0.35f;
    [SerializeField]
    AudioSource cancelTyping;

    bool isFirstLetter;

    //settings
    [SerializeField]
    [Tooltip("Remember to get rid of .txt extension or it wont work!")]
    string fileName = "1-1000";
    [Header("Remember to get rid of .txt extension or it wont work!")]
    [Header("")]
    [SerializeField]
    int baseAmount = 6;
    [SerializeField]
    float modifier = 1.5f;

    //score
    [SerializeField]
    Text score;

    //what to type
    [SerializeField]
    Text whatToType;
    [SerializeField]
    Color whenCompleteColor = new Color(0.9f, 0.9f, 0.9f);

    //how much will be reloaded
    [SerializeField]
    Text howMuchReload;

    //levels
    [SerializeField]
    Level[] levels;

    //internal
    List<string> wordBank;

    string word;
    int currentChar;
    int level;
    int amountOverMaxLevel;

    string currentKeysDown;
    string previousKeysDown;

    bool wasntReleased = false;

    // Use this for initialization
    void Start () {
        wordBank = new List<string>();
        LoadWords();
        ResetTypingVars();
	}

    void ResetTypingVars()
    {
        word = "";
        currentChar = 0;
        level = 0;
        amountOverMaxLevel = 0;
        isFirstLetter = true;
        wasntReleased = true;
    }

    private void LoadWords()
    {
        TextAsset file = Resources.Load("Words/" + fileName) as TextAsset; //load file from resources
        if (file == null)
            Debug.LogError("Failed to load words. At TypingScript.LoadWords(void)");

        System.IO.StringReader reader = new System.IO.StringReader(file.text); //process string
        if (reader == null)
            Debug.LogError("Assets/Resources/Words/" + fileName + " Not found! At TypingScript.LoadWords(void)");

        while (reader.Peek() >= 0)
        {
            wordBank.Add(reader.ReadLine());
        }
    }

	// Update is called once per frame
	void Update () {
		if (player.playerStat.IsTyping && !isFirstLetter)
        {
            whatToType.text = word;

            if (Input.GetButtonDown("Cancel") || Input.GetButtonUp("pause"))
            {
                CancelTyping();
            }
            else
            {


                currentKeysDown = Input.inputString;
                if (currentChar >= word.Length) //should increase difficulty?
                {
                    //The word going on for ever is technically a bug. it shouldn't go on for ever. but its a fun bug :D
                    word += " ";
                    level++;
                    //if the player has reached max level chuck random words, it doesnt matter
                    if (amountOverMaxLevel > 0) // GC "google-10000-english-no-swears.txt" | Measure - Property length - Maximum | Select Maximum to find longest thing in line
                        word += GetWord(Random.Range(1, 19));
                    else //otherwise increase difficulty
                        word += GetWord(System.Convert.ToInt32(baseAmount * levels[getLevel()].multiplier));

                    increaseDifficultySound.pitch = 1.0f;
                    increaseDifficultySound.pitch += Random.Range(-pitchVariation, pitchVariation);
                    increaseDifficultySound.Play();
                }
                else
                {
                    Debug.Log(word[currentChar]);
                    if (isAlphaCharacterPressed(System.Convert.ToChar(System.Convert.ToString(word[currentChar]).ToLower()))) //if the correct thing is pressed then progress the current word typed
                    {
                        typeSound.Play();
                        currentChar++;
                    
                        if (currentChar > 0)
                        {
                            System.Text.StringBuilder strBuild = new System.Text.StringBuilder(word);
                            strBuild[currentChar - 1] = char.ToLower(strBuild[currentChar - 1]);
                            word = strBuild.ToString();
                        }

                        wasntReleased = true;
                    }
                    else if (Input.GetButtonDown("Submit")) //if player has submitted
                    {
                        if (level > 0)
                        {
                            completeSound.Play();
                            addAmmo();
                        }
                        else
                        {
                            CancelTyping();
                        }
                    }
                    else
                    {
                        /*this probably uses some weird bug in windows because when a key is held down, according to the program its not held down for a breif second making code start making the 'key is wrong' noise, which is the desired behaviour.*/
                        if (currentChar > 0 && !currentKeysDown.Contains(System.Convert.ToString(word[currentChar - 1]))) //if it doesnt contain the previous character
                        {
                            wasntReleased = false; //tell the program that it got released
                        }

                        /*if (currentChar > 0 && isAlphaCharacterPressed(word[currentChar - 1]) && wasntReleased) //if the previous character was right and its still pressed
                        {
                            //dont play the sound
                        }*/
                        else if (!wasntReleased)
                        {
                            //then start playing the wrong sound
                            if (!wasIncorrectKeyDown)
                            {
                                incorrectTypeSound.Play();
                                wasIncorrectKeyDown = true;
                            }
                            else if (currentKeysDown != previousKeysDown)
                            {
                                wasIncorrectKeyDown = false;
                            }
                        }
                        
                    }
                    previousKeysDown = currentKeysDown;
                    /*else if (Input.anyKey) //otherwise play error soud
                    {
                        if (!wasIncorrectKeyDown)
                        {
                            incorrectTypeSound.Play();
                            wasIncorrectKeyDown = true;
                        }
                    }
                    else if (!Input.anyKey)
                    {
                        wasIncorrectKeyDown = false;
                    }*/


                }
            }

            //update how much reload
            if (level == 0) //if at level zero
            {
                howMuchReload.text =
                    "No.of d'nuts you will get:" + "\n" +
                    "Currently:" + "\n" +
                    "Nothing" + "\n" + "\n" +
                    "Next:" + "\n" +
                    levels[level + 1].amountToReload;
            }
            else if (level >= levels.Length - 1) //if at last level
            {
                howMuchReload.color = whenCompleteColor;
                howMuchReload.text =
                    "No.of d'nuts you will get:" + "\n" +
                    "Currently:" + "\n" +
                    levels[levels.Length - 1].amountToReload + "\n" + "\n" +
                    "Next:" + "\n" +
                    "Same + Extra Points";
            }
            else //if increasing levels
            {
                howMuchReload.text =
                     "No.of d'nuts you will get:" + "\n" +
                     "Currently:" + "\n" +
                     levels[level].amountToReload + "\n" + "\n" +
                     "Next:" + "\n" +
                     levels[level + 1].amountToReload;
            }

        }
        else
        {
            whatToType.text = "";
            howMuchReload.text = "";
            howMuchReload.color = Color.white;
        }

        if (!Input.anyKey)
            isFirstLetter = false;
	}

    private void CancelTyping()
    {
        if (level > 0)
        {
            addAmmo();
            completeSound.Play();
        }
        else
        {
            cancelTyping.Play();
        }

        player.playerStat.IsTyping = false;

        ResetTypingVars();
    }

    public void Reload()
    {
        player.playerStat.IsTyping = true;
        word = GetWord(baseAmount);
    }

    int getLevel()
    {
        if (level >= levels.Length)
        {
            level = levels.Length - 1;
            amountOverMaxLevel++;
        }
        return level;
    }

    void addAmmo()
    {
        player.playerStat.Ammunition += levels[getLevel()].amountToReload; //reload
        player.playerStat.Ammunition = (int)Mathf.Clamp(player.playerStat.Ammunition, 0, player.playerStat.MaxAmmuntion); //ensure that there isnt too much ammo

        player.playerStat.IsTyping = false; //go back to playing
        score.text = System.Convert.ToString((System.Convert.ToInt32(score.text) + currentChar)); //update score
        ResetTypingVars();
    }

    string GetWord(int length)      
    {
        if (length <= 0 || length >= 19)
            throw new System.Exception("The length is out of bounds in TypingScript.GetWord(int).");

        List<string> possibleWords = wordBank.FindAll(x => x.Length == length);

        return possibleWords[Random.Range(0, possibleWords.Count - 1)].ToUpper();
    }

    bool isAlphaCharacterPressed(char character)
    {
        if      (character == ' ' && Input.GetKeyDown(KeyCode.Space)) { return true; }
        else if (character == 'a' && Input.GetKeyDown(KeyCode.A)) { return true; }
        else if (character == 'b' && Input.GetKeyDown(KeyCode.B)) { return true; }
        else if (character == 'c' && Input.GetKeyDown(KeyCode.C)) { return true; }
        else if (character == 'd' && Input.GetKeyDown(KeyCode.D)) { return true; }
        else if (character == 'e' && Input.GetKeyDown(KeyCode.E)) { return true; }
        else if (character == 'f' && Input.GetKeyDown(KeyCode.F)) { return true; }
        else if (character == 'g' && Input.GetKeyDown(KeyCode.G)) { return true; }
        else if (character == 'h' && Input.GetKeyDown(KeyCode.H)) { return true; }
        else if (character == 'i' && Input.GetKeyDown(KeyCode.I)) { return true; }
        else if (character == 'j' && Input.GetKeyDown(KeyCode.J)) { return true; }
        else if (character == 'k' && Input.GetKeyDown(KeyCode.K)) { return true; }
        else if (character == 'l' && Input.GetKeyDown(KeyCode.L)) { return true; }
        else if (character == 'm' && Input.GetKeyDown(KeyCode.M)) { return true; }
        else if (character == 'n' && Input.GetKeyDown(KeyCode.N)) { return true; }
        else if (character == 'o' && Input.GetKeyDown(KeyCode.O)) { return true; }
        else if (character == 'p' && Input.GetKeyDown(KeyCode.P)) { return true; }
        else if (character == 'q' && Input.GetKeyDown(KeyCode.Q)) { return true; }
        else if (character == 'r' && Input.GetKeyDown(KeyCode.R)) { return true; }
        else if (character == 's' && Input.GetKeyDown(KeyCode.S)) { return true; }
        else if (character == 't' && Input.GetKeyDown(KeyCode.T)) { return true; }
        else if (character == 'u' && Input.GetKeyDown(KeyCode.U)) { return true; }
        else if (character == 'v' && Input.GetKeyDown(KeyCode.V)) { return true; }
        else if (character == 'w' && Input.GetKeyDown(KeyCode.W)) { return true; }
        else if (character == 'x' && Input.GetKeyDown(KeyCode.X)) { return true; }
        else if (character == 'y' && Input.GetKeyDown(KeyCode.Y)) { return true; }
        else if (character == 'z' && Input.GetKeyDown(KeyCode.Z)) { return true; }
        else { return false; }
    }
}
