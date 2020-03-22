using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Context
{
    public class MobileMovieManagerDbContext : DbContext
    {
        private string databasePath;

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        public MobileMovieManagerDbContext(string databasePath)
        {
            this.databasePath = databasePath;

            if (!File.Exists(databasePath))
            {
                Database.EnsureCreated();

                // Create default filetypes
                this.FileTypes.Add(new FileType() { Id = 1, TypeName = "avi", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 2, TypeName = "mp4", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 3, TypeName = "mkv", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 4, TypeName = "M2TS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 5, TypeName = "MTS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 6, TypeName = "TS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 7, TypeName = "wmv", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 8, TypeName = "mpg", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 9, TypeName = "mpeg", IsChecked = true });
                // Create default server setting
                this.Settings.Add(new Setting()
                {
                    Id = 1,
                    ConnectedPhone = "???",
                    ServerIP = "192.168.1.106",
                    Port = 3245,
                    IsStartWithWindows = false,
                    IsSearchForSubtitle = false,
                    UserName = "Felhasználói név",
                    MoviesPath = "",
                    FileTypes = setFileTypes().Result,
                    // Create default display setting
                    SelectedPalette = "Windows Phone",
                    SelectedTheme = "light",
                    SelectedAccentColor = new byte[] { 0xa2, 0x00, 0xff }
                });

                this.SaveChangesAsync();
            }
        }

        private async Task<List<FileType>> setFileTypes()
        {
            List<FileType> fileTypes = new List<FileType>();
            foreach (FileType fileType in await this.FileTypes.ToListAsync())
            {
                if (fileType.IsChecked) fileTypes.Add(fileType);
            }

            return fileTypes;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
