using System;
using UnityEngine;
using UnityEditor;
using System.IO;


public class GenerateData
{
    private const string SERIES_DATA_PATH = "/Resources/JSON/series.JSON";
    private const string BOOK_DATA_PATH = "/Resources/JSON/books.JSON";
    private const string CHAPTER_DATA_PATH = "/Resources/JSON/chapters.JSON";

    const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvqxyz";

    SeriesList seriesList = new SeriesList();
    BookList bookList = new BookList();
    ChapterList chapterList = new ChapterList();

    public void generateRandomSeriesData()
    {
        seriesList = new SeriesList();
        bookList = new BookList();
        chapterList = new ChapterList();

        SeriesData data = null;
        for (int i = 0; i < UnityEngine.Random.RandomRange(5,15); i++)
        {
            data = new SeriesData();
            data.description = "Random description " + UnityEngine.Random.RandomRange(0, 1000000) + " " + i;
            data.seriesId = (i + UnityEngine.Random.RandomRange(0, 1000000)).ToString();
            data.name = "Series: " + generateRandomName();
            data.imagePath = "Some image path..";

            generateRandomBookData(data);

            seriesList.aribtraryList.Add(data);
        }


        writeDataToFiles();
    }

    private void generateRandomBookData(SeriesData parentData)
    {
      
        BookData data = null;
        for (int i = 0; i < UnityEngine.Random.RandomRange(5, 15); i++)
        {
            data = new BookData();

            data.description = "Random description " + UnityEngine.Random.RandomRange(0, 1000000) + " " + i;
            data.bookId = (i + UnityEngine.Random.RandomRange(0, 1000000)).ToString();
            data.seriesId = parentData.seriesId;
            generateRandomChapterData(data);
            data.name = "Book: " + generateRandomName();
            data.imagePath = "Some image path..";
            bookList.aribtraryList.Add(data);
        }
    }

    private void generateRandomChapterData(BookData parentData)
    {
        SeriesList list = new SeriesList();
        ChapterData data = null;
        for (int i = 0; i < UnityEngine.Random.RandomRange(5, 15); i++)
        {
            data = new ChapterData();
            data.description = "Random description " + UnityEngine.Random.RandomRange(0, 1000000) + " " + i;
            data.seriesId = parentData.seriesId;
            data.bookId = parentData.bookId;
            data.chapterId = (i + UnityEngine.Random.RandomRange(0, 1000000)).ToString();

            data.name = "Chapter: " + generateRandomName();
            data.imagePath = "Some image path..";

            chapterList.aribtraryList.Add(data);
        }
    }

    private string generateRandomName()
    {
        string returnString = "";
        for (int i = 0; i < UnityEngine.Random.RandomRange(5, 10); i++)
        {
            returnString += LETTERS[UnityEngine.Random.RandomRange(0, LETTERS.Length - 1)];
        }
        return returnString;
    }

    private void writeDataToFiles()
    {
        string seriesJSON = JsonUtility.ToJson(seriesList);
        string bookJSON = JsonUtility.ToJson(bookList);
        string chapterJSON = JsonUtility.ToJson(chapterList);
        
        File.WriteAllText(Application.dataPath + SERIES_DATA_PATH, seriesJSON);
        File.WriteAllText(Application.dataPath + BOOK_DATA_PATH, bookJSON);
        File.WriteAllText(Application.dataPath + CHAPTER_DATA_PATH, chapterJSON);
    }
}
