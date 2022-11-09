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
        [SerializeField] private List<Crew> shipRoster;
        [SerializeField] private Crew crewGenPrefab;
        [SerializeField] private Crew newCrew;
        [SerializeField] private bool offerCrewmate;
        [SerializeField] private int deathCount;

        public GameObject canvas;
        public GameObject genButton;
        public GameObject addButton;
        public GameObject decButton;
        public GameObject replayButton;
        public GameObject quitButton;
        public TextMeshProUGUI gameText;
        public TextMeshProUGUI shipCountText;
        public TextMeshProUGUI deathCountText;


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
        /// sets the game initial gamestate for buttons
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
            shipCountText.SetText(shipRoster.Count.ToString());
            deathCountText.SetText(deathCount.ToString());
            if (shipRoster.Count == 10)
            {
                GameEnd();
            }
            else if (deathCount == 3)
            {
                GameEnd();
            }
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
            
            if (newCrew.isParasite == true)
            {
                gameText.SetText(newCrew.firstName + " " + newCrew.lastName + " was a parasite!");
                StartCoroutine(Co_KillCrew());
            }
            else
            {
                shipRoster.Add(newCrew);
                gameText.SetText(newCrew.firstName + " " + newCrew.lastName + " has been added to the crew.");

                SwapButtons();
            }
        }

        private IEnumerator Co_KillCrew()
        {
            yield return new WaitForSeconds(2);
            //play blood splatter sound effect

            //randomly selects an existing crewmate from the shipRoster list
            int killGen = Random.Range(0, shipRoster.Count);
            gameText.SetText(shipRoster[killGen].firstName + " " + shipRoster[killGen].lastName
                        + " has been killed!");
            shipRoster.RemoveAt(killGen);
            SwapButtons();
        }

        /// <summary>
        /// displays crew declined message
        /// </summary>
        public void DeclineCrew()
        {
            Debug.Log("Decline Crew clicked");

            if (newCrew.isParasite == false)
            {
                deathCount++;
            }
            gameText.SetText(newCrew.firstName + " " + newCrew.lastName + "'s application has been denied.");
            SwapButtons();
        }

        /// <summary>
        /// processes victory (or defeat) and game over.
        /// </summary>
        void GameEnd()
        {
            genButton.SetActive(false);
            addButton.SetActive(false);
            decButton.SetActive(false);
            replayButton.SetActive(true);
            quitButton.SetActive(true);

            if (shipRoster.Count == 10)
            {
                gameText.SetText("Congratulations, your ship has a full crew!");
            }
            else if (deathCount == 3)
            {
                gameText.SetText("You have killed too many innocents. You lose.");
            }
        }
        public void Replay()
        {
            Debug.Log("Replay clicked");
            SceneManager.LoadScene("Crewmates");
        }
        public void Exit()
        {
            Debug.Log("Quit Game clicked");
            Application.Quit();
        }
    }
}
