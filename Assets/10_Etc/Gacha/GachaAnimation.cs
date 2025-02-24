using UnityEngine;
using System.Collections;

public class GachaAnimation : MonoBehaviour
{
    public RectTransform skillFilm;
    public float animationDuration = 2f;
    public float startPositionY = 300f;
    public float endPositionY = -300f;

    public IEnumerator AnimatePachinko()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = skillFilm.anchoredPosition;
        startPosition.y = startPositionY;
        skillFilm.anchoredPosition = startPosition;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            Vector3 position = skillFilm.anchoredPosition;
            position.y = Mathf.Lerp(startPositionY, endPositionY, t);
            skillFilm.anchoredPosition = position;

            yield return null;
        }
    }
}
