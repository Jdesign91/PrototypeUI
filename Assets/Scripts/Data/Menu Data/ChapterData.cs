using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChapterData
{
    // So we can link chapters to the books in the series. Series Id may be uneeded here... might be helpful later
    // if we wanted to do things like leave a chapter to go back to the correct series page or book page.
    public string seriesId;
    public string bookId;
    public string chapterId;

    public string imagePath;
    public string name;
    public string description;
}