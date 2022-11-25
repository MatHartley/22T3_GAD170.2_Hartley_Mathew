using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathewHartley
{
    public class MenuButton : MonoBehaviour
    {
    /// <summary>
    /// plays an audio file when buttons are clicked
    /// </summary>
        public AudioSource buttonClickSound;

        public void clickButton()
        {
            buttonClickSound.Play();
        }
    }
}