using System;
using UnityEngine;
using OrgDay.Util;

[Serializable]
public class RuntimeConfig : BaseAsset
{
    public bool saveToFile;

    public override string ToString()
    {
        return StringUtil.CombineKVP(
            "saveToFile", saveToFile
            );
    }
}
