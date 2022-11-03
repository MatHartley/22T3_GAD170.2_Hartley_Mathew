using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathewHartley
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private List<Crew> shipRoster;
        [SerializeField] private Crew crewGen;

        private bool offerCrewmate;

        /// <summary>
        /// initializes the UI elements of the game
        /// </summary>
        void Start()
        { 
            //initialize UI buttons
        }

        /// <summary>
        /// the game controller logic and process. 
        /// calls required methods based on UI button presses. 
        /// processes victory logic.
        /// </summary>
        void Update()
        {
            //if generate crew button is pressed
            //{
            //offerCrewmate = true;
            //SwapButtons();
            //offerCrewmate = false;

            //Crew newCrew = Instantiate(crewGen, transform);
            //newCrew.crewFirstName = 
            //newCrew.crewLastName = 
            //newCrew.crewHobby = 
            //newCrew.isParasite =

            //newCrew.name = crewFirstName + " " + crewLastName;

            //display "Crewmate: " + newCrew.name + "Hobby: " + newCrew.crewHobby"
            //}

            //if accept crew button is pressed
            //{
            //wait 2 seconds
            //AddCrew();
            //SwapButtons();
            //}

            //if decline crew button is pressed
            //{
            //wait 2 seconds
            //display "newCrew.name + "'s application has been declined.""
            //SwapButtons();
            //}

            if (shipRoster.Count == 10)
            {
                GameEnd();
            }
        }

        /// <summary>
        /// activates and deactivates UI buttons based on the stage of the game.
        /// </summary>
        void SwapButtons()
        {
            if (offerCrewmate == true)
            {
                //deactivate generate crewmate button
                //activate accept crewmate button
                //activate decline crewmate button
            }
            else
            { 
                //activate generate crewmate button
                //deactivate accept crewmate button
                //deactive decline crewmate button
            }
        }

        /// <summary>
        /// populates and depopulates the ship roster.
        /// </summary>
        void AddCrew()
        {
            //if (newCrew.isParasite == true)
                //{
                //play blood splatter sound effect
                //int killGen= Random.Range(1, shipRoster.Count)
                //display "newCrew.name + " was a parasite.""
                //display "shipRoster[killGen].crewFirstName + " " +
                //shipRoster[killGen].crewFirstName + " has been found dead.""
                //shipRoster[killGen].Remove;
                //}
            //else
                //{
                //shipRoster.Add(newCrew);
                //display "newCrew.name + " has joined the crew.""
                //}
        }

        /// <summary>
        /// processes victory (or defeat) and game over.
        /// </summary>
        void GameEnd()
        {
            if (shipRoster.Count == 10)
            {
                //display “Congratulations, your ship has a full crew.”
            }
            //activate replay button
        }
    }
}