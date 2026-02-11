using UnityEngine;
using DG.Tweening;
using Player;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerName = "OpenChest";
    
    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .3f;
    public Ease tweenEase = Ease.OutBack;
    private float _startScale;
    
    void Start()
    {
        _startScale = notification.transform.localScale.x;
        HideNotification();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenChest()
    {
        animator.SetTrigger(triggerName);
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.transform.GetComponent<PlayerController>();
        if (p != null)
        {
            ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerController p = other.transform.GetComponent<PlayerController>();
        if (p != null)
        {
            HideNotification();
        }
    }

    void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, tweenDuration).SetEase(tweenEase);
    }
    
    void HideNotification()
    {
        notification.SetActive(false);
    }
}
