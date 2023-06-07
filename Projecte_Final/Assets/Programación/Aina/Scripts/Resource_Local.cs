using UnityEngine;

public class Resource_Local : MonoBehaviour
{
    [SerializeField] private Animator Villager;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip soundEND;
    public PoolingItemsEnum recolect_vfx;

    public void Reprocucsound()
    {
        BackGround_Music.instance.AudioClip(sound);
    }

    public void ReprocucsoundEND()
    {
        BackGround_Music.instance.AudioClip(soundEND);
    }
    public void VillagerIdle()
    {
        Villager.SetBool("Work", false);
    }
    
    public void VillagerWork()
    {
        GameObject recolectVFX = PoolingManager.Instance.GetPooledObject((int)recolect_vfx);
        GameObject point = gameObject.transform.GetChild(1).gameObject;
        recolectVFX.transform.position = point.transform.position;
        recolectVFX.transform.rotation = point.transform.rotation;
        recolectVFX.gameObject.SetActive(true);
        
        Villager.SetBool("Work", true);
    }
}
