using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Catalog : MonoBehaviour
{
    public const string CATALOG_PREFAB_PATH = "Prefabs/Catalog";

    public MenuPage seriesPage;
    public MenuPage booksPage;
    public MenuPage chaptersPage;

    public CustomButton buttomTemplate;

    private void Start()
    {   
        CustomButton seriesButton;
        Dictionary<string, object> seriesArgs;
        List<SeriesData> sortedSeriesData = new List<SeriesData>();

        for (int i = 0; i < DataCache.sortedSeriesData.Count(); i++)
        {
            seriesButton = Instantiate(buttomTemplate, seriesPage.gridLayoutParent.transform);
            seriesArgs = new Dictionary<string, object>();
            seriesArgs.Add("data", DataCache.sortedSeriesData[i]);
            seriesButton.addCallback(onClickSeries, seriesArgs);
            seriesButton.text.text = DataCache.sortedSeriesData[i].name;
        }

        int roundedVal = DataCache.seriesDataCache.Count / seriesPage.grid.constraintCount;
        roundedVal += DataCache.seriesDataCache.Count % seriesPage.grid.constraintCount > 0 ? 1 : 0;
        Debug.LogError("New size is " + ((roundedVal * seriesPage.grid.cellSize.y * 2) - 100));
        seriesPage.scrollContent.sizeDelta = new Vector2(0, (roundedVal * seriesPage.grid.cellSize.y * 2) - 100);

        seriesPage.gameObject.SetActive(true);
        booksPage.gameObject.SetActive(false);
        chaptersPage.gameObject.SetActive(false);
    }

    private void onClickSeries(CustomButton sender, Dictionary<string, object> args)
    {
        foreach(Transform trans in booksPage.gridLayoutParent.transform)
        {
            Destroy(trans.gameObject);
        }

        SeriesData passedData = args["data"] as SeriesData;

        CustomButton bookButton;
        Dictionary<string, object> bookArgs;
        for (int i = 0; i < DataCache.bookDataCache[passedData.seriesId].Count(); i++)
        {
            bookButton = Instantiate(buttomTemplate, booksPage.gridLayoutParent.transform);
            bookArgs = new Dictionary<string, object>();
            bookArgs.Add("data", DataCache.bookDataCache[passedData.seriesId][i]);
            bookButton.addCallback(onClickBook, bookArgs);

            bookButton.text.text = DataCache.bookDataCache[passedData.seriesId][i].name;
        }

        seriesPage.gameObject.SetActive(false);
        booksPage.gameObject.SetActive(true);
        chaptersPage.gameObject.SetActive(false);

        int roundedVal = DataCache.bookDataCache[passedData.seriesId].Count() / booksPage.grid.constraintCount;
        roundedVal += DataCache.bookDataCache[passedData.seriesId].Count() % booksPage.grid.constraintCount > 0 ? 1 : 0;
        booksPage.scrollContent.sizeDelta = new Vector2(0, (roundedVal * booksPage.grid.cellSize.y * 2) - 100);

        Manager.instance.swipeManager.onSwipeLeft += showSeries;
    }

    private void onClickBook(CustomButton sender, Dictionary<string, object> args)
    {
        foreach (Transform trans in chaptersPage.gridLayoutParent.transform)
        {
            Destroy(trans.gameObject);
        }

        BookData passedData = args["data"] as BookData;

        CustomButton chapterButton;
        Dictionary<string, object> chapterArgs;
        for (int i = 0; i < DataCache.chapterDataCache[passedData.bookId].Count(); i++)
        {
            chapterButton = Instantiate(buttomTemplate, chaptersPage.gridLayoutParent.transform);
            chapterArgs = new Dictionary<string, object>();
            chapterArgs.Add("data", DataCache.chapterDataCache[passedData.bookId][i]);
            chapterButton.addCallback(onClickChapter, chapterArgs);
            chapterButton.text.text = DataCache.chapterDataCache[passedData.bookId][i].name;
        }

        int roundedVal = DataCache.chapterDataCache[passedData.bookId].Count() / chaptersPage.grid.constraintCount;
        roundedVal += DataCache.chapterDataCache[passedData.bookId].Count() % chaptersPage.grid.constraintCount > 0 ? 1 : 0;
        chaptersPage.scrollContent.sizeDelta = new Vector2(0, (roundedVal * chaptersPage.grid.cellSize.y * 2) - 100);

        seriesPage.gameObject.SetActive(false);
        booksPage.gameObject.SetActive(false);
        chaptersPage.gameObject.SetActive(true);
        Manager.instance.swipeManager.onSwipeLeft += showBooks;
    }

    private void showSeries()
    {
        seriesPage.gameObject.SetActive(true);
        booksPage.gameObject.SetActive(false);
        chaptersPage.gameObject.SetActive(false);
        Manager.instance.swipeManager.onSwipeLeft -= showSeries;
    }

    private void showBooks()
    {
        seriesPage.gameObject.SetActive(false);
        booksPage.gameObject.SetActive(true);
        chaptersPage.gameObject.SetActive(false);
        Manager.instance.swipeManager.onSwipeLeft += showSeries;
        Manager.instance.swipeManager.onSwipeLeft -= showBooks;
    }


    private void onClickChapter(CustomButton sender, Dictionary<string, object> args)
    {

        Debug.LogError("Hey you clicked a chapter!");
    }
}

