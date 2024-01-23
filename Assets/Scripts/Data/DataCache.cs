using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DataCache
{
    // When we pull data for the menu, we can cache things here as needed...
    
    // Key'd by series ID
    public static Dictionary<string, SeriesData> seriesDataCache = new Dictionary<string, SeriesData>();
    public static List<SeriesData> sortedSeriesData = new List<SeriesData>();
    // Key'd by series ID
    public static Dictionary<string, List<BookData>> bookDataCache = new Dictionary<string, List <BookData>>();

    // Key'd by book ID
    public static Dictionary<string, List<ChapterData>> chapterDataCache = new Dictionary<string, List<ChapterData>>();

    public static void bustCache()
    {
        sortedSeriesData.Clear();
        seriesDataCache.Clear();
        bookDataCache.Clear();
        chapterDataCache.Clear();
    }
   
}
