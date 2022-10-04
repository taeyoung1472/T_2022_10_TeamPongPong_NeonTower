using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class JsonData
{
    public int data;
    public List<GoogleSheetData> sheetData = new List<GoogleSheetData>();
}

[Serializable]
public class GoogleSheetData
{
    public List<string> cell = new List<string>();
}