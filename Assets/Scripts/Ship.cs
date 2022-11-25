using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace MathewHartley
{
    public class Ship : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private List<Crew> shipRoster;
        [SerializeField] private Crew crewGenPrefab;
        [SerializeField] private Crew newCrew;
        [SerializeField] private bool offerCrewmate;
        [SerializeField] private int deathCount;

        [Header("UI Elements")]
        public GameObject canvas;
        public GameObject genButton;
        public GameObject addButton;
        public GameObject decButton;
        public GameObject replayButton;
        public GameObject quitButton;

        [Header("Text Fields")]
        public TextMeshProUGUI gameText;
        public TextMeshProUGUI shipCountText;
        public TextMeshProUGUI deathCountText;

        [Header("Sound Effects")]
        public AudioSource gunshot;
        public AudioSource bloodSplat;
        public AudioSource humanDeathSound;
        public AudioSource monsterDeathSound;
        public AudioSource successAdd;
        public AudioSource winSound;
        public AudioSource loseSound;

        /// <summary>
        /// sets the game initial gamestate
        /// </summary>
        private void Start()
        {
            genButton.SetActive(true);
            addButton.SetActive(false);
            decButton.SetActive(false);
            replayButton.SetActive(false);
            quitButton.SetActive(true);
            offerCrewmate = false;
        }

        /// <summary>
        /// the game controller logic and process. 
        /// calls required methods based on UI button presses. 
        /// processes victory logic.
        /// </summary>
        void Update()
        {
            //keeps count win/lose conditions, i.e. accepted crewmates and wrongful deaths
            shipCountText.SetText(shipRoster.Count.ToString());
            deathCountText.SetText(deathCount.ToString());

            if (shipRoster.Count == 10 || deathCount == 3)
            {
                GameEnd();
                enabled = false;
            }
        }

        /// <summary>
        /// generates a crewmate and offers it for the player to accept or decline
        /// kills existing crewmate if added crewmate is a parasite
        /// </summary>
        public void OfferCrew()
        {
            Debug.Log("Generate Crew clicked");
            offerCrewmate = true;
            SwapButtons();
            offerCrewmate = false;

            //generate a new crewmate
            newCrew = Instantiate(crewGenPrefab, transform);
            newCrew.Generate();
            newCrew.name = newCrew.firstName + " " + newCrew.lastName;

            gameText.SetText("Crewmate: " + newCrew.firstName + " " + newCrew.lastName + " Hobby: " + newCrew.hobby);
        }


        /// <summary>
        /// activates and deactivates UI buttons based on the stage of the game.
        /// </summary>
        void SwapButtons()
        {
            //swaps button status to allow player to accept or deny a generated crewmate
            if (offerCrewmate == true)
            {
                genButton.SetActive(false);
                addButton.SetActive(true);
                decButton.SetActive(true);
            }
            //swaps button status to allow a new crewmate to be generated
            else if (offerCrewmate == false)
            {
                genButton.SetActive(true);
                addButton.SetActive(false);
                decButton.SetActive(false);
            }
        }

        /// <summary>
        /// populates and depopulates the ship roster.
        /// checks if the generated crewmate is a parasite
        ///if true, the generated crewmate is not added to the shipRoster list, and an existing crewmate is killed
        /// </summary>
        public void AddCrew()
        {
            Debug.Log("Add Crew clicked");
            
            //checks if crew is a parasite...
            if (newCrew.isParasite == true)
            {
                gameText.SetText(newCrew.firstName + " " + newCrew.lastName + " was a parasite!");
                //...calling a coroutine to kill an existing crewmate if they are...
                StartCoroutine(Co_KillCrew());
            }
            else
            {
                //...or adding them to the ship roster list if they are not.
                successAdd.Play();
                shipRoster.Add(newCrew);
                gameText.SetText(newCrew.firstName + " " + newCrew.lastName + " has been added to the crew.");

                SwapButtons();
            }
        }
        /// <summary>
        /// Coroutine for killing a crewmate if the added crewmate is a parasite
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_KillCrew()
        {
            addButton.SetActive(false);
            decButton.SetActive(false);
            //pause for dramatic effect
            yield return new WaitForSeconds(2);

            bloodSplat.Play();

            //randomly selects an existing crewmate from the shipRoster list...
            int killGen = Random.Range(0, shipRoster.Count);
            gameText.SetText(shipRoster[killGen].firstName + " " + shipRoster[killGen].lastName
                        + " has been killed!");
            //...and then removes them from the list
            shipRoster.RemoveAt(killGen);

            //pause for dramatic effect
            yield return new WaitForSeconds(3);

            SwapButtons();
        }

        /// <summary>
        /// handles the decline crew option, adding to the wrongful death count failure condition
        /// if the declined crew is not a parasite
        /// </summary>
        public void DeclineCrew()
        {
            Debug.Log("Decline Crew clicked");

            gunshot.Play();
            gameText.SetText(newCrew.firstName + " " + newCrew.lastName + "'s application has been denied.");

            //checks if the crew is a parasite, adding to the death count if they are not
            //plays appropriate sound effect based on parasite status
            if (newCrew.isParasite == false)
            {
                deathCount++;
                humanDeathSound.Play();
            }
            else 
            {
                monsterDeathSound.Play();
            }
            SwapButtons();
        }

        /// <summary>
        /// processes victory (or defeat) and game over.
        /// </summary>
        void GameEnd()
        {
            //turns off all buttons
            genButton.SetActive(false);
            addButton.SetActive(false);
            decButton.SetActive(false);
            quitButton.SetActive(false);

            StartCoroutine(Co_WinOrLose());
        }

        /// <summary>
        /// coroutine for game end, pauses once win or lose criteria is met
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_WinOrLose()
        {
            yield return new WaitForSeconds(2);

            //differentiates win or loss, displays relevant text and plays relevant sound effect
            if (shipRoster.Count == 10)
            {
                gameText.SetText("Congratulations, your ship has a full crew!");
                winSound.Play();
            }
            else if (deathCount == 3)
            {
                gameText.SetText("You have killed too many innocents. You lose.");
                loseSound.Play();
            }
            //activating quit and replay buttons
            replayButton.SetActive(true);
            quitButton.SetActive(true);
        }

        /// <summary>
        /// Reloads the game from the beginning
        /// </summary>
        public void Replay()
        {
            Debug.Log("Replay clicked");
            SceneManager.LoadScene("Crewmates");
        }

        /// <summary>
        /// Quits the game
        /// </summary>
        public void Exit()
        {
            Debug.Log("Quit Game clicked");
            Application.Quit();
        }
    }
}
