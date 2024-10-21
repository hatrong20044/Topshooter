using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager: Singleton<GUIManager>
{
    [SerializeField] private GameObject m_homeGUI;
    [SerializeField] private GameObject m_gameGUI;
    [SerializeField] private Transform m_lifeGrid;
    [SerializeField] private GameObject m_lifeIconPrefab;
    [SerializeField] private ImageFilled m_levelProgressBar;
    [SerializeField] private ImageFilled m_hpProgressBar;

    [SerializeField] private Text m_levelCountingTxt;
    [SerializeField] private Text m_xpCountingTxt;
    [SerializeField] private Text m_hpCountingTxt;
    [SerializeField] private Text m_coinCountingTxt;
    [SerializeField] private Text m_reloadStateTxt;

    [SerializeField] private Dialog m_gunUpgradeDialog;
    [SerializeField] private Dialog m_gameoverDialog;
    private Dialog m_activeDialog;
    public Dialog ActiveDialog1 { get => m_activeDialog;private  set => m_activeDialog = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if (m_gameGUI !=null)
        {
            m_gameGUI.SetActive(isShow);
        }
        if(m_homeGUI !=null)
        {
            m_homeGUI.SetActive(!isShow);
        }
    }

    private void ShowDialog(Dialog dialog)
    {
        if (dialog == null) return;

        m_activeDialog = dialog;
        m_activeDialog.Show(true);
        
    }

    public void ShowGunUpGradeDialog()
    {
        ShowDialog(m_gunUpgradeDialog);
    }
    public void ShowGameOverDialog() {  ShowDialog(m_gameoverDialog);}

    public void UpDateLifeInfo(int life)
    {
       
        ClearLifeGrid();
        DrawnLifeGrid(life);
    }

    private void DrawnLifeGrid(int life)
    {
        if(m_lifeGrid==null||m_lifeIconPrefab==null) return;    
        for(int i = 0; i < life; i++)
        {
            var lifeIconClone = Instantiate(m_lifeIconPrefab, Vector3.zero, Quaternion.identity);
            lifeIconClone.transform.SetParent(m_lifeGrid);
            lifeIconClone.transform.localPosition = Vector3.zero;
            lifeIconClone.transform.localScale = Vector3.one;

        }

    }

    private void ClearLifeGrid()
    {
        if (m_lifeGrid==null) return;
        int lifeItemCounting = m_lifeGrid.childCount;
        for (int i = 0;i < lifeItemCounting; i++){
            var lifeItem = m_lifeGrid.GetChild(i);
            if(lifeItem==null) continue;
            Destroy(lifeItem.gameObject );
        }
    }

    public void UpdateLevelInfo(int currentLevel,float curXp,float levelUpXpRequired)
    {
        m_levelProgressBar?.UpdateValue(curXp,levelUpXpRequired);

        if (m_levelCountingTxt != null) m_levelCountingTxt.text = $"LEVEL{currentLevel.ToString("00")}";
        if (m_xpCountingTxt != null) m_xpCountingTxt.text = $"{curXp.ToString("00")}/{levelUpXpRequired.ToString("00")}";
        
    }
    public void UpdateHpInfo(float curHp,float maxHp)
    {
        m_hpProgressBar?.UpdateValue(curHp,maxHp);
        if (m_hpCountingTxt != null) m_hpCountingTxt.text = $"{curHp.ToString("00")}/{maxHp.ToString("00")}";

    }

    public void UpdateCoinCounting(int coins)
    {
        if(m_coinCountingTxt != null)m_coinCountingTxt.text=coins.ToString("n0");
    }

    public void ShowReloadTxt(bool isShow)
    {
        if(m_reloadStateTxt!=null) m_reloadStateTxt.gameObject.SetActive(isShow);
    }
}
