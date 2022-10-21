using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathewHartley
{

    public class CrewGenerator : MonoBehaviour
    {
        //These string arrays are the name and hobby selection lists
        private string[] firstNames = new string[25] {"Maria", "Mohammed", "Jose", "Wei", "Ali", 
            "John", "David", "Li", "Abdul", "Anna", "Michael", "Juan", "Mary", "Jean", "Robert", 
            "Daniel", "Luis", "Carlos", "Elena", "Lei", "Min", "Ibrahim", "Peter", "Fatima", 
            "Xin"};
        private string[] lastNames = new string[15] {"Wang", "Smith", "Devi", "Ivanov", "Kim", 
            "Ali", "Garcia", "Muller", "Silva","Mohamed", "Nguyen", "Rodriguez", "Moyo", 
            "Hansen", "Tesfaye"};
        private string[] crewHobbies = new string[10] { "Woodcarving", "Bushwalking", 
            "Train Spotting", "Dancing", "Dining with Friends", "Pet Grooming", "Physics", 
            "Watching Documentaries", "Gaming", "Watching Youtube"};
        private string[] paraHobbies = new string[10] { "Bonecarving", "Survivalism", 
            "People Watching", "Puppetry", "Dining on Friends", "Taxidermy", "Microbiology", 
            "Watching True Crime", "Wargaming", "Watching TicTok"};

        [SerializeField] private string crewFirstName;
        [SerializeField] private string crewLastName;
        [SerializeField] private string crewHobby;
        [SerializeField] private bool isParasite;

        /// <summary>
        /// Generates a crewmate using randomly selected names and hobbies from the arrays
        /// Sets the parasite condition and selects hobby accordingly
        /// </summary>
        void Generate()
        {
            //generates random numbers for name and hobby selection
            //generates random number for parasite condition
            int firstGen = Random.Range(1, 25);
            int lastGen = Random.Range(1, 15);
            int hobbyGen = Random.Range(1, 10);
            int paraGen = Random.Range(-2, 2);

            crewFirstName = firstNames[firstGen];
            crewLastName = lastNames[lastGen];
            
            //if paraGen is > 0, the crewmate is designated a parasite and gets a parasite hobby
            //otherwise the crewmate is not a parasite and gets a crewmate hobby
            //this SHOULD result in a 2/3 chance of crew and 1/3 chance of parasite
            if (paraGen > 0)
            {
                isParasite = true;
                crewHobby = paraHobbies[hobbyGen];
            }
            else
            {
                isParasite = false;
                crewHobby = crewHobbies[hobbyGen];
            }

            Debug.Log("Crewmate: " + crewFirstName + " " + crewLastName + ". Likes: " + crewHobby + 
                ". Parasite: " + isParasite);
        }
        /// <summary>
        /// used for testing the Generate method before the Ship clas was implemented
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Generate();
            }
        }
    }
}