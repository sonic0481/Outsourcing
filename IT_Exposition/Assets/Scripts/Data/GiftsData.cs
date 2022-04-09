using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftsData 
{
    #region Gift
    const string TOTAL = "_Total";
    const string GIVE = "_Give";
    #endregion

    #region User Setting
    public bool UserReceipt { get; set; }

    public GIFTS WinningGift { get; set; }

    public bool CheatEnable { get; set; }
    public GIFTS CheatGift { get; set; }
    #endregion    

    public string GetUserReceiptText()
    {
        return UserReceipt ? "Y" : "N";
    }

    public void SetDefaultGiftsData(GIFTS gift)
    {
        for(GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        {
            if (false == PlayerPrefs.HasKey($"{g}{TOTAL}"))
            {
                switch (g)
                {
                    case GIFTS.STARBUCKS: Update_Total(g, 100); break;
                    case GIFTS.HUMIDIFIER: Update_Total(g, 100); break;
                    case GIFTS.POSTIT: Update_Total(g, 100); break;
                    case GIFTS.MEMO: Update_Total(g, 100); break;
                    case GIFTS.MEGASTUDY: Update_Total(g, 100); break;
                }

                Update_Give(g, 0);
            }
        }
    }


    public void AwakeInit()
    {
        for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        {
            SetDefaultGiftsData(g);
        }
    }
    public void OnInit()
    {
        UserReceipt = false;
        WinningGift = GIFTS.NONE;
        CheatEnable = false;
        CheatGift = GIFTS.NONE;
    }

    public GIFTS RateOfGifts()
    {
        int allRemain = GetAll_Remain();

        if (0 >= allRemain)
            return GIFTS.NONE;

        int winPoint = Random.Range(0, allRemain);
        int check = 0;
        GIFTS winGift = GIFTS.NONE;

        for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        {
            check += Get_Remain(g);

            if (check > winPoint)
            {
                winGift = g;
                break;
            }
        }

        return winGift;
    }

    #region Get Method
    public int Get_Total(GIFTS gift)
    {
        return PlayerPrefs.GetInt($"{gift}{TOTAL}", 0);
    }

    public int Get_Give(GIFTS gift)
    {
        return PlayerPrefs.GetInt($"{gift}{GIVE}", 0);
    }

    public int Get_Remain(GIFTS gift)
    {
        return Get_Total(gift) - Get_Give(gift);
    }

    public int GetAll_Total()
    {
        int allTotalCount = 0;
        for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        {
            allTotalCount += Get_Total(g);
        }

        return allTotalCount;
    }

    public int GetAll_Give()
    {
        int allGiveCount = 0;
        for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        {
            allGiveCount += Get_Give(g);
        }

        return allGiveCount;
    }

    public int GetAll_Remain()
    {
        return GetAll_Total() - GetAll_Give();
    }

    public float GetOdds_F(GIFTS gift)
    {
        int allRemain = GetAll_Remain();
        int giftRemain = Get_Remain(gift);
        if (0 >= allRemain)
            return 0f;

        float odds = giftRemain / (float)allRemain;

        return Mathf.Round(odds * 100) / 100;
    }
    #endregion

    #region Set Total Gift
    public void Update_Total(GIFTS gift, int totalCount)
    {
        string key = $"{gift}{TOTAL}";

        PlayerPrefs.SetInt(key, totalCount);
        PlayerPrefs.Save();
    }

    public void Add_Total(GIFTS gift, int addTotalCount)
    {
        string key = $"{gift}{TOTAL}";
        int currentTotal = PlayerPrefs.GetInt(key, 0);

        PlayerPrefs.SetInt(key, currentTotal + addTotalCount);
        PlayerPrefs.Save();
    }
    #endregion

    #region Set Give Gift 
    public void Update_Give(GIFTS gift, int giveCount)
    {
        string key = $"{gift}{GIVE}";
        int totalCount = Get_Total(gift);
        int resultGive = giveCount <= totalCount ? giveCount : totalCount;

        PlayerPrefs.SetInt(key, resultGive);
        PlayerPrefs.Save();
    }

    public void Add_Give(GIFTS gift, int addGiveCount = 1)
    {
        string key = $"{gift}{GIVE}";
        int totalCount = Get_Total(gift);
        int currentGive = PlayerPrefs.GetInt(key, 0);
        int resultGive = (currentGive + addGiveCount <= totalCount) ? currentGive + addGiveCount : totalCount;

        PlayerPrefs.SetInt(key, resultGive);
        PlayerPrefs.Save();
    }
    #endregion

    #region Gifts Text
    public string GetName(GIFTS gift)
    {
        var table = CSVTableManager.Instance.GetTable<DefGiftsListTable>();

        var data = table.GetData(gift);

        return data?.Name ?? "NONE";
    }

    public string GetFullName(GIFTS gift)
    {
        var table = CSVTableManager.Instance.GetTable<DefGiftsListTable>();

        var data = table.GetData(gift);

        return data?.FullName ?? "NONE";
    }

    public string GetUserGiftText()
    {
        return GetName(WinningGift);
    }

    public int[] GetGiftWins(int winCount)
    {
        var table = CSVTableManager.Instance.GetTable<DefGiftsListTable>();
        var dataList = table.GetDataList();

        int[] wins = new int[(int)GIFTS.END];

        while (dataList.MoveNext())
        {
            var data = dataList.Current;
            GIFTS key = (GIFTS)data.Index;
            int win = 0;            

            if(0 < Get_Remain(key))
            {
                switch (winCount)
                {
                    case 0:
                        win = data.Win_0;
                        break;
                    case 1:
                        win = data.Win_1;
                        break;
                    case 2:
                        win = data.Win_2;
                        break;
                    case 3:
                        win = data.Win_3;
                        break;
                }
            }           

            wins[data.Index] = win;
        }

        return wins;
    }
    #endregion
}
