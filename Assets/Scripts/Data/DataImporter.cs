using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataImporter
{
    public const string SERIES_DATA_PATH = "JSON/series";
    public const string BOOK_DATA_PATH = "JSON/books";
    public const string CHAPTER_DATA_PATH = "JSON/chapters";

    public void requestAllData(bool shouldBustCache = false)
    {
        if (shouldBustCache)
        {
            DataCache.bustCache();
        }
        
        // Under different conditions we'd request this from a server with either unitys WWW class or whatever.
        // For now lets just pull from resources. 

        TextAsset seriesText = Resources.Load<TextAsset>(SERIES_DATA_PATH);
        TextAsset bookText = Resources.Load<TextAsset>(BOOK_DATA_PATH);
        TextAsset chapterText = Resources.Load<TextAsset>(CHAPTER_DATA_PATH);

        if (seriesText == null)
        {
            Debug.LogError("NULL TEXT??");
        }

        SeriesList series = JsonUtility.FromJson<SeriesList>(seriesText.text);
        ChapterList chapters = JsonUtility.FromJson<ChapterList>(chapterText.text);
        BookList books = JsonUtility.FromJson<BookList>(bookText.text);

        cacheData(series, chapters, books);
        
    }

    private void cacheData(SeriesList seriesData, ChapterList chapterData, BookList bookData)
    {
        // Sort series
        seriesData.aribtraryList.Sort((a, b) => a.name.CompareTo(b.name));
        DataCache.sortedSeriesData = seriesData.aribtraryList;

        for (int i = 0; i < seriesData.aribtraryList.Count; i++)
        {
            DataCache.seriesDataCache.Add(seriesData.aribtraryList[i].seriesId, seriesData.aribtraryList[i]);
        }

        for (int i = 0; i < chapterData.aribtraryList.Count; i++)
        {
            if (!DataCache.chapterDataCache.ContainsKey(chapterData.aribtraryList[i].bookId))
            {
                DataCache.chapterDataCache.Add(chapterData.aribtraryList[i].bookId, new List<ChapterData>());
            }

            DataCache.chapterDataCache[chapterData.aribtraryList[i].bookId].Add(chapterData.aribtraryList[i]);
        }

        for (int i = 0; i < bookData.aribtraryList.Count; i++)
        {
            if (!DataCache.bookDataCache.ContainsKey(bookData.aribtraryList[i].seriesId))
            {
                DataCache.bookDataCache.Add(bookData.aribtraryList[i].seriesId, new List<BookData>());
            }

            DataCache.bookDataCache[bookData.aribtraryList[i].seriesId].Add(bookData.aribtraryList[i]);
        }

        // Sort after we setup our dictionarys...
        foreach(KeyValuePair<string, List<ChapterData>> data in DataCache.chapterDataCache)
        {
            data.Value.Sort((a, b) => a.name.CompareTo(b.name));
        }

        foreach (KeyValuePair<string, List<BookData>> data in DataCache.bookDataCache)
        {
            data.Value.Sort((a, b) => a.name.CompareTo(b.name));
        }
    }


    // Maybe we have a lot of data and we dont want it all at once?
    /*
    public void requestSeriesData()
    {

    }

    public void requestBookData()
    {

    }

    public void requestChapterData()
    {

    }
    */
}
