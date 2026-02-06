using System.Collections;
using UnityEngine;
using Core.Singleton;
using UnityEngine.Rendering.PostProcessing;


public class EffectsManager : Singleton<EffectsManager>
{
   public PostProcessVolume postProcessVolume;
   public float duration = 1.0f;
   [SerializeField] private Vignette vignette;

   [NaughtyAttributes.Button]
   public void ChangeVignette()
   {
      StartCoroutine(FlashColorVignette());
   }
   
   IEnumerator FlashColorVignette()
   {
      Vignette tmp;

      if (postProcessVolume.profile.TryGetSettings(out tmp))
      {
         vignette = tmp;
      }
      
      ColorParameter c = new ColorParameter();
      FloatParameter intensityParam = new FloatParameter();
      float time = 0;
      while (time < duration)
      {
         time += Time.deltaTime;
         c.value = Color.Lerp(Color.black, Color.red, time / duration);
         intensityParam.value = Mathf.Lerp(0f, 0.55f, time / duration);
         vignette.intensity.Override(intensityParam);
         vignette.color.Override(c);
         yield return new WaitForEndOfFrame();
      }
      
      time = 0;
      while (time < duration)
      {
         time += Time.deltaTime;
         intensityParam.value = Mathf.Lerp(0.55f, 0f, time / duration);
         c.value = Color.Lerp(Color.red, Color.black, time / duration);
         vignette.intensity.Override(intensityParam);
         vignette.color.Override(c);
         yield return new WaitForEndOfFrame();
      }
   }
}
