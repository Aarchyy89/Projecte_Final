using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness_Manager : MonoBehaviour
{
    public Slider SliderBrightness;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    public AutoExposure exposure;
    // Start is called before the first frame update

    private void Awake()
    {
        if (PlayerPrefs.HasKey("brillo"))
        {
            SliderBrightness.value = PlayerPrefs.GetFloat("brillo");
            AdjustBrightness();
        }
    }

    void Start()
    {
        brightness.TryGetSettings(out exposure);
        //AdjustBrightness();
    }

    // Update is called once per frame
    public void AdjustBrightness()
    {
        if (exposure != null)
        {
            exposure.keyValue.value = SliderBrightness.value;
        }

        PlayerPrefs.SetFloat("brillo", SliderBrightness.value);
    }

}
