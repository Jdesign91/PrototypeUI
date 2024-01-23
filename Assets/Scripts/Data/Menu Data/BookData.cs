using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BookData
{
    // So we can link back to the series this book is related to and so chapters can be linked to this.
    public string seriesId;
    public string bookId;

    public string imagePath;
    public string name;
    public string description;
}