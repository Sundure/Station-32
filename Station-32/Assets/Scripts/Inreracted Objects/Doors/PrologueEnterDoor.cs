using UnityEngine;

public class PrologueEnterDoor : MonoBehaviour ,IInteracted
{
    public void Interact()
    {
        SceneManager.LoadScene(SceneList.Station32);
    }
}
