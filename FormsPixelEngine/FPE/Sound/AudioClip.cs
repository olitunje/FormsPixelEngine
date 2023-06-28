using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Sound
{

    /*! \class AudioClip
    \brief Represents a sound bit loaded form a audio file. 
    */
    public class AudioClip
    {
        private SoundPlayer simpleSound;

        //! Constructor 
        /*!
          \param _filename filepath of the sound file
        */
        public AudioClip(string _filename) {
            simpleSound = new SoundPlayer(_filename);
        }

        //! Play the audio clip once.
        /*!
        */
        public void Play() {
            simpleSound.Play();
        }

        //! Play the audio clip looping
        /*!
        */
        public void PlayLoop() { 
            simpleSound.PlayLooping();
        }

        //! Stop the audio clip form playing
        /*!
        */
        public void Stop() {
            simpleSound.Stop();
        }
    }
}
 