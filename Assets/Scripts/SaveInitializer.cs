using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInitializer : MonoBehaviour
{
    public void InitializeSave() {
        PlayerPrefs.SetInt("SaveExists", 1);

        //data management
        PlayerPrefs.SetInt("SchoolDataManagement", 1);
        PlayerPrefs.SetInt("SNICODataManagement", 1);
        PlayerPrefs.SetInt("LCPDataManagement", 1);
        PlayerPrefs.SetInt("RicksDataManagement", 1);
        PlayerPrefs.SetInt("EADataManagement", 1);
        PlayerPrefs.SetInt("SewersDataManagement", 1);

        //broad game flags
        PlayerPrefs.SetString("TimeOfDay", "Day");
        PlayerPrefs.SetInt("OverworldInstructions", 0);
        PlayerPrefs.SetString("IntroCutsceneStatus", "Unwatched");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 0);
        PlayerPrefs.SetInt("Dollars", 0);

        //house dialogue
        PlayerPrefs.SetString("SisterDialogueState", "Init");
        PlayerPrefs.SetString("MomDialogueState", "Init");
        PlayerPrefs.SetString("BrotherDialogueState", "Init");
        PlayerPrefs.SetString("DadDialogueState", "Init");

        //overworld map entries
        PlayerPrefs.SetString("EtherealAscentEntry", "Closed");
        PlayerPrefs.SetString("SewersEntry", "Closed");
        PlayerPrefs.SetString("RicksEntry", "Closed");
        PlayerPrefs.SetString("LCPEntry", "Open");
        PlayerPrefs.SetString("SchoolEntry", "Open");
        PlayerPrefs.SetString("SNICOEntry", "Open");

        //school state
        PlayerPrefs.SetString("TeacherDialogueState", "Init");

        //LCP state
        PlayerPrefs.SetString("DateProgress", "Init");
        PlayerPrefs.SetString("LCPSpriteState", "Normal");

        //SNICO state
        PlayerPrefs.SetString("SNICOTutorialState", "Incomplete");
        PlayerPrefs.SetString("SNICOProgress", "Unstarted");

        //rick's state
        PlayerPrefs.SetString("LairryDialogueState", "Init");
        PlayerPrefs.SetString("RicksKey", "Uncollected");
        PlayerPrefs.SetInt("DrinkingGameTries", 0);

        //EA state
        PlayerPrefs.SetString("SantaDialogueState", "Init");

        //sewers state
        PlayerPrefs.SetInt("SewersLocationDisplay", 1);

        PlayerPrefs.SetInt("MaxHealth", 6);
        PlayerPrefs.SetInt("CollectedHC1", 0);
        PlayerPrefs.SetInt("CollectedHC2", 0);

        PlayerPrefs.SetInt("HasDoubleJump", 0);
        PlayerPrefs.SetInt("HasCloner", 0);
        PlayerPrefs.SetInt("HasBomb", 0);
        PlayerPrefs.SetInt("HasGun", 0);

        PlayerPrefs.SetInt("Room3Gate", 0);
        PlayerPrefs.SetInt("Room5Button", 0);
        PlayerPrefs.SetInt("Room5WestWall", 0);
        PlayerPrefs.SetInt("Room5EastWall", 0);
        PlayerPrefs.SetInt("Room8Gate", 0);
        PlayerPrefs.SetInt("Room9EnemyGate", 0);
        PlayerPrefs.SetInt("Room9Target", 0);
        PlayerPrefs.SetInt("Room11Wall", 0);
        PlayerPrefs.SetInt("Room14Target1", 0);
        PlayerPrefs.SetInt("Room14Target2", 0);
        PlayerPrefs.SetInt("LCPWall", 0);

        PlayerPrefs.SetInt("BrokeYellowEgg", 0);
        PlayerPrefs.SetInt("BrokeGreenEgg", 0);
        PlayerPrefs.SetInt("BrokeRedEgg", 0);
        PlayerPrefs.SetInt("BrokeBlueEgg", 0);
        PlayerPrefs.SetInt("BrokeOrangeEgg", 0);
        PlayerPrefs.SetInt("BrokePurpleEgg", 0);

        PlayerPrefs.SetInt("SMRoom9", 0);
        PlayerPrefs.SetInt("SMRoom10", 0);
        PlayerPrefs.SetInt("SMRoom11", 0);
        PlayerPrefs.SetInt("SMRoom12", 0);
        PlayerPrefs.SetInt("SMRoom1314", 0);
        PlayerPrefs.SetInt("SMRoom15", 0);
        PlayerPrefs.SetInt("SMRoom16", 0);

        PlayerPrefs.SetInt("BossCheckpoint", 0);

        //pizza guy state
        PlayerPrefs.SetString("PizzaGuyState", "Init");
    }
}
