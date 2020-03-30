using MobileMovieManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.FileServer
{
    public static class MovieServer
    {
        // .nfo files
        private static List<string> nfoFiles = new List<string>();

        // Get movie titles
        public static async Task<List<Movie>> GetMovieTitles(string movieFolder, ICollection<FileType> fileTypes)
        {
            // Movie model list
            List<Movie> movies = new List<Movie>();
            // Movie titles
            List<string> movieFullFiles = new List<string>();

            // .nfo files
            nfoFiles = Directory.GetFiles(movieFolder, "*.nfo", SearchOption.AllDirectories).ToList();

            // Search movie files in root directory and subdirectories filter by filetypes
            var fileTypesList = fileTypes.Select(f => f.TypeName);
            movieFullFiles = Directory.GetFiles(movieFolder, "*.*", SearchOption.AllDirectories).Where(f => fileTypesList.Any(e => f.ToLower().EndsWith(e))).ToList();

            string lastMoreCdFolderName = "";
            byte cdCount = 1;

            List<MoreCD> moreCDs = new List<MoreCD>();

            // Get FileInfo
            for (int i = 0; i < movieFullFiles.Count; i++)
            {
                // Info from movie files
                FileInfo movieFileInfo = new FileInfo(movieFullFiles[i]);

                // Skip samples
                if (movieFileInfo.Name.ToLower().Contains("sample"))
                {
                    continue;
                }

                string movieTitle = "";
                string folderTitle = movieFileInfo.Directory.Name;
                bool isMoreCD = false;

                // Check movie is in CD1, CD2... folder and store cd-s
                if (movieFileInfo.Directory.Name.ToUpper() == "CD1" || movieFileInfo.Directory.Name.ToUpper() == "CD2" || movieFileInfo.Directory.Name.ToUpper() == "CD3" || movieFileInfo.Directory.Name.ToUpper() == "CD4")
                {
                    if (lastMoreCdFolderName == movieFileInfo.Directory.Parent.Name)
                    {
                        cdCount++;
                        moreCDs.Add(new MoreCD() { Title = movieFileInfo.FullName, CdCount = cdCount });
                        //moreCDs.FirstOrDefault(c => c.Title == movieFileInfo.Directory.Parent.Name).CdCount++;
                    }
                    else
                    {
                        cdCount = 1;
                        moreCDs.Add(new MoreCD() { Title = movieFileInfo.FullName, CdCount = cdCount });
                    }

                    movieTitle = movieFileInfo.Directory.Parent.Name;
                    folderTitle = movieTitle;
                    isMoreCD = true;
                    lastMoreCdFolderName = movieFileInfo.Directory.Parent.Name;
                }
                // Check the name of the movie is contains CD1, CD2... and store cd-s
                else if (movieFileInfo.Name.ToUpper().Contains("CD1") || movieFileInfo.Name.ToUpper().Contains("CD2") || movieFileInfo.Name.ToUpper().Contains("CD3") || movieFileInfo.Name.ToUpper().Contains("CD4") || movieFileInfo.Name.ToUpper().Contains("CD01") || movieFileInfo.Name.ToUpper().Contains("CD02") || movieFileInfo.Name.ToUpper().Contains("CD03") || movieFileInfo.Name.ToUpper().Contains("CD04"))
                {
                    if (lastMoreCdFolderName == movieFileInfo.Directory.Name)
                    {
                        cdCount++;
                        moreCDs.Add(new MoreCD() { Title = movieFileInfo.FullName, CdCount = cdCount });
                        //moreCDs.FirstOrDefault(c => c.Title == movieFileInfo.Directory.Name).CdCount++;
                    }
                    else
                    {
                        cdCount = 1;
                        moreCDs.Add(new MoreCD() { Title = movieFileInfo.FullName, CdCount = cdCount });
                    }

                    movieTitle = Path.GetFileNameWithoutExtension(movieFileInfo.Name);
                    isMoreCD = true;
                    lastMoreCdFolderName = movieFileInfo.Directory.Name;
                }
                // Get title from directory
                else if (movieFileInfo.DirectoryName != movieFolder)
                {
                    movieTitle = movieFileInfo.Directory.Name;
                }
                // Get title from filename
                else
                {
                    movieTitle = Path.GetFileNameWithoutExtension(movieFileInfo.Name);
                }

                // Set file size
                double fileSize = (double)movieFileInfo.Length / 1073741823;

                // Create Movie model
                Movie movie = new Movie()
                {
                    Title = movieTitle,
                    FolderTitle = folderTitle,
                    FullPath = movieFileInfo.FullName,
                    FileType = fileTypes.FirstOrDefault(f => f.TypeName == movieFileInfo.Extension.Remove(0, 1).ToLower()),
                    FileTypeId = fileTypes.FirstOrDefault(f => f.TypeName == movieFileInfo.Extension.Remove(0, 1).ToLower()).Id,
                    Size = String.Format("{0:0.##} GB", fileSize),
                    CreationTime = movieFileInfo.CreationTime,
                    ImdbId = await setIMDBfromNFOFiles(movieFileInfo),
                    IsFavorite = false,
                    IsMoreCD = isMoreCD,
                    IsSeries = false
                };

                // Add to movie model list
                movies.Add(movie);
            }

            // Clear dots and other tags from titles
            movies = await clearTitles(movies, moreCDs);
            // Sort movies
            movies.Sort((a, b) => a.Title.CompareTo(b.Title));

            return await Task.FromResult(movies).ConfigureAwait(false);
        }

        // Clear titles
        private static async Task<List<Movie>> clearTitles(List<Movie> movies, List<MoreCD> moreCDs)
        {
            List<string> separatorTags = new List<string> { ".19", ".20", "DVDRIP", "XVID", "BDRIP", " HUN", ".HUN", "(BLUERAY", "720P", " 20", " 19", "-20", "-19", "(20", "(19", "X264", ".S0", ".S1", ".S2", ".S3", ".S4" };

            for (int i = 0; i < movies.Count; i++)
            {
                // Split title by separatortags
                string[] splittedTitle = movies[i].Title.ToUpper().Split(separatorTags.ToArray<string>(), StringSplitOptions.None);
                movies[i].Title = splittedTitle[0];

                // Replace dot to space
                movies[i].Title = movies[i].Title.Replace('.', ' ').Replace('_', ' ').ToUpper();

                // Remove last space character
                if (' ' == movies[i].Title.Last()) movies[i].Title = movies[i].Title.Remove(movies[i].Title.Length - 1);

                if (movies[i].IsMoreCD)
                {
                    movies[i].Title += " -> CD" + moreCDs.FirstOrDefault(m => m.Title == movies[i].FullPath).CdCount;
                }
            }

            //return movies.GroupBy(x => x.Title).Select(g => g.First()).ToList();
            return await Task.FromResult(movies).ConfigureAwait(false);
        }

        // Get .nfo files to current movie and get IMDB id if exists
        private static async Task<string> setIMDBfromNFOFiles(FileInfo movieFileInfo)
        {
            string imdbId = "";

            foreach (string nfoFile in nfoFiles)
            {
                FileInfo nfoFileInfo = new FileInfo(nfoFile);
                string pathMovie = Path.GetFileNameWithoutExtension(movieFileInfo.Name);
                string pathNfo = Path.GetFileNameWithoutExtension(nfoFileInfo.Name);

                // Find correct .nfo
                if (pathMovie.ToUpper() == pathNfo.ToUpper())
                {
                    string imdbRow = File.ReadAllLines(nfoFile).FirstOrDefault(i => i.Contains("imdb.com/title/"));
                    if (imdbRow != null)
                    {
                        string imdb = imdbRow.Split(new string[] { "/title/" }, StringSplitOptions.None).LastOrDefault();
                        if (imdb.IndexOf('/') > 0)
                        {
                            imdbId = imdb.Remove(imdb.IndexOf('/'));
                        }
                    }

                    break;
                }
            }

            return await Task.FromResult(imdbId);
        }

        // Model for more cd
        private class MoreCD
        {
            public string Title { get; set; }
            public byte CdCount { get; set; }
        }
    }
}
