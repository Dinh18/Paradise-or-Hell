using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController instance;
    [Header("UI References")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private GameObject upgradeButtonPrefab;
    [Header("Data")]
    [SerializeField] private List<UpgradeData> allUpgrades;
    [SerializeField] private GameObject[] upgradeButtons;
    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        this.gameObject.SetActive(false);
        upgradeButtons = new GameObject[3];
    }

    public void ShowUpgradePanel()
    {
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        if (optionsContainer.childCount > 0)
        {
            for (int i = optionsContainer.childCount - 1; i >= 0; i--)
            {
                GameObject child = optionsContainer.GetChild(i).gameObject;
                child.SetActive(false); // Tắt ngay để không click nhầm
            }
        }

        List<UpgradeData> randomOptions = GetRandomUpgrades(3);

        for (int i = 0; i < 3; i++)
        {
            UpgradeData data = randomOptions[i];
            upgradeButtons[i] = Instantiate(upgradeButtonPrefab, optionsContainer);
            
            upgradeButtons[i].transform.localScale = Vector3.one; 

            // Biến tạm để fix lỗi Closure (Bạn đã làm đúng, giữ nguyên)
            // UpgradeData tempUpgradeData = data;

            // Gán UI Text
            // Lưu ý: Kiểm tra null để tránh lỗi đỏ nếu Prefab chưa gắn đủ
            var nameTxt = upgradeButtons[i].transform.Find("Name");
            var descTxt = upgradeButtons[i].transform.Find("Description");
            if (nameTxt) nameTxt.GetComponent<TMP_Text>().text = data.GetUpgradeName();
            if (descTxt) descTxt.GetComponent<TMP_Text>().text = data.GetDescription();

            // --- QUAN TRỌNG NHẤT: XỬ LÝ SỰ KIỆN CLICK ---
            Button btn = upgradeButtons[i].GetComponent<Button>();
            
            // 1. Xóa sạch mọi sự kiện cũ (tránh Prefab bị dính)
            btn.onClick.RemoveAllListeners(); 
            
            // 2. Thêm sự kiện mới
            btn.onClick.AddListener(() => 
            {
                // Debug ngay tại thời điểm click để bắt quả tang lỗi
                // Debug.Log($"CLICKED: Tên hiển thị là [{data.GetUpgradeName()}] - Nhưng Enum thật là [{data.GetStat()}]");
                SelectUpgrade(data);
            });


        }


        // --- BƯỚC 3: ÉP UI VẼ LẠI NGAY LẬP TỨC ---
        // Cái này sửa lỗi nút bị chồng lên nhau
        // LayoutRebuilder.ForceRebuildLayoutImmediate(optionsContainer.GetComponent<RectTransform>());

    }

    public void SelectUpgrade(UpgradeData data)
    {
        ApplyUpgrade(data);

        // Debug.Log("Đã chọn nâng cấp: " + data.GetStat().ToString());

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ApplyUpgrade(UpgradeData data)
    {
        // Logic cộng chỉ số hoặc thêm vũ khí
        if (data.GetUpgradeType() == UpgradeType.StatUpgrade)
        {
            PlayerStats.instance.IncreaseStat(data.GetStat(), data.GetAmount());
            Debug.Log(data.GetStat() );
        }
        else if (data.GetUpgradeType() == UpgradeType.StatSkillUpgrade)
        {
            PlayerSkills.instant.UpgradeStat(data.GetUpgradeName(), data.GetStatSkillType(), data.GetAmount());
            Debug.Log(data.GetUpgradeName() + " " + data.GetStatSkillType().ToString());
        }
        else if(data.GetUpgradeType() == UpgradeType.NewSkill)
        {
            PlayerSkills.instant.AddSkill(data.GetUpgradeName(), data.GetSkillPrefab());
            Debug.Log("Thêm kỹ năng mới: " + data.GetUpgradeName());
        }
    }

    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        List<UpgradeData> tempList = new List<UpgradeData>(allUpgrades);
        List<UpgradeData> result = new List<UpgradeData>();

        for(int i = 0; i < count; i++)
        {
            if(tempList.Count == 0) break;

            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            // Debug.Log("Chon ngau nhien: " + tempList[randomIndex].GetUpgradeName());
            tempList.RemoveAt(randomIndex);
        }
        return result;
    }
}
