using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour, IPointerEnterHandler
{
    public void OnStartGameButtonPressed()
    {
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.ButtonSelect);
        SceneManager.LoadSceneAsync("MainLevel01");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.ButtonHover);
    }

}
