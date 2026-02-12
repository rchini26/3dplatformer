using UnityEngine;
using DG.Tweening;
using Player;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    
    [Header("Open Chest Setup")]
    public string triggerName = "OpenChest";
    public KeyCode keyCode = KeyCode.LeftControl;
    public ChestItemBase chestItem;
    
    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .3f;
    public Ease tweenEase = Ease.OutBack;
    private float _startScale;

    private bool _openChest;
    
    void Start()
    {
        _startScale = notification.transform.localScale.x;
        HideNotification();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf) OpenChest();
    }

    void OpenChest()
    {
        if (_openChest) return;
        animator.SetTrigger(triggerName);
        _openChest = true;
        HideNotification();
        chestItem.ShowItem();
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
