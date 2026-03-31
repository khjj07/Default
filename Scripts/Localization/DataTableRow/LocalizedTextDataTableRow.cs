using System;
using KCoreKit;
using UnityEngine;

public class LocalizedTextDataTableRow : LocalizedDataTableRowBase<string>
{
    public string EN;
    public string KR;
    public string JP;
    public string CN;

    public override string Get(Language language)
    {
        return language switch
        {
            Language.EN => EN,
            Language.KR => KR,
            Language.JP => JP,
            Language.CN => CN,
            _ => EN
        };
    }
}