using UnityEngine;

namespace Options_UI
{
    public class Settings : MonoBehaviour 
    {
        [Header("Volume Levels")]
        [SerializeField, Range(0f, 100f)] public float musicVolume = 50f;
        [SerializeField, Range(0f, 100f)] public float soundFxVolume = 50f;

        [Header("Stereo")]
        [SerializeField] public bool stereoMute = false;
    }
}