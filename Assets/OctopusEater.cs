using UnityEngine;
public class OctopusEater : MonoBehaviour
{
    public float growthFactor = 0.1f;
    public AudioClip eatSound; // 新增音效变量
    private AudioSource audioSource; // 新增音频源变量

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查物体是否比小章鱼小
        if (other.transform.localScale.magnitude < transform.localScale.magnitude)
        {
            // 吃掉物体
            Destroy(other.gameObject);

            // 播放声音
            if (eatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(eatSound);
            }

            // 增大小章鱼，同时提高Y轴位置
            transform.localScale += Vector3.one * growthFactor;
            transform.position += Vector3.up * growthFactor;
        }
    }
}