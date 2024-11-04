using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerOpening : MonoBehaviour
{
    public void LoadToOpening()
    {
        SceneManager.LoadScene("OpeningCutscene");
    }
}
