using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GiftInfo
{
    public GIFTS type;
    public string name;    
    public Vector2[] angle;

    public GiftInfo(GIFTS _type, string _name, Vector2[] _angle)
    {
        type = _type;
        name = _name;
        angle = _angle;
    }
}

public class GiftsData 
{
    #region Gift
    const string TOTAL = "_Total";
    const string GIVE = "_Give";

    Dictionary<GIFTS, GiftInfo> _dicGiftInfos = new Dictionary<GIFTS, GiftInfo>();
    #endregion

    #region User Setting
    public bool UserReceipt { get; set; }

    public GIFTS WinningGift { get; set; }

    public bool CheatEnable { get; set; }
    public GIFTS CheatGift { get; set; }
    #endregion

    public string WinningGiftName()
    {
        if (GIFTS.START > WinningGift || GIFTS.END <= WinningGift)
            return string.Empty;        
        else
            return _dicGiftInfos[WinningGift].name;        
    }

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
                    case GIFTS.GUNBAM: Update_Total(g, 100); break;
                    case GIFTS.STARBUKS: Update_Total(g, 100); break;
                    case GIFTS.BBASAK: Update_Total(g, 100); break;
                    case GIFTS.MAGNET: Update_Total(g, 200); break;
                    case GIFTS.FISHCAKE: Update_Total(g, 200); break;
                    case GIFTS.BUDS: Update_Total(g, 8); break;
                }

                Update_Give(g, 0);
            }
        }
    }


    public void AwakeInit()
    {
        _dicGiftInfos.Add(GIFTS.GUNBAM, new GiftInfo(GIFTS.GUNBAM, "군밤 1봉지\n교환권", new Vector2[1] { new Vector2(-25f, 3f) } ));
        _dicGiftInfos.Add(GIFTS.STARBUKS, new GiftInfo(GIFTS.STARBUKS, "스타벅스\n5천원권", new Vector2[1] { new Vector2(50f, 72f) } ));
        _dicGiftInfos.Add(GIFTS.BBASAK, new GiftInfo(GIFTS.BBASAK, "빠삭이 1개\n교환권", new Vector2[1] { new Vector2(115f, 140f) }));
        _dicGiftInfos.Add(GIFTS.MAGNET, new GiftInfo(GIFTS.MAGNET, "태종대\n마그넷", new Vector2[1] { new Vector2(188f, 212f) }));
        _dicGiftInfos.Add(GIFTS.FISHCAKE, new GiftInfo(GIFTS.FISHCAKE, "어묵/물떡 1꼬지\n교환권", new Vector2[1] { new Vector2(260f, 285f) }));
        _dicGiftInfos.Add(GIFTS.BUDS, new GiftInfo(GIFTS.BUDS, "버즈\n라이프", new Vector2[5] {
            new Vector2(18f, 32f), new Vector2(90f, 98f), new Vector2(157f, 171f), new Vector2(230f, 242f), new Vector2(302f, 318f)
        }) );

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

    public Vector2[] GetAngle(GIFTS gift)
    {
        if (GIFTS.START > gift || GIFTS.END <= gift)
            return new Vector2[1] { new Vector2(0f, 0f) };

        return _dicGiftInfos[gift].angle;        
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
        switch (gift)
        {
            case GIFTS.GUNBAM: return "군밤";
            case GIFTS.STARBUKS: return "스타벅스";
            case GIFTS.BBASAK: return "빠삭이";
            case GIFTS.MAGNET: return "마그넷";
            case GIFTS.FISHCAKE: return "어묵/떡";
            case GIFTS.BUDS: return "버즈";
        }
        return "NONE";
    }

    public string GetUserGiftText()
    {
        return GetName(WinningGift);
    }
    #endregion
}
