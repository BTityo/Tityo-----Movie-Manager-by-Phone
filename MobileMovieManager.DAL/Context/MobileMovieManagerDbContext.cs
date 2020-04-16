using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MobileMovieManager.DAL.Context
{
    public class MobileMovieManagerDbContext : DbContext
    {
        private string databasePath;

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<FileSize> FileSizes { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Logger> Loggers { get; set; }

        public MobileMovieManagerDbContext(string databasePath)
        {
            this.databasePath = databasePath;

            if (!File.Exists(databasePath))
            {
                Database.EnsureCreated();

                // Create default file sizes
                this.FileSizes.Add(new FileSize() { Id = 1, SizeName = "Válassz file méretet" });
                this.FileSizes.Add(new FileSize() { Id = 2, SizeName = "< 1,00 GB" });
                this.FileSizes.Add(new FileSize() { Id = 3, SizeName = "1,00 GB - 2,00 GB" });
                this.FileSizes.Add(new FileSize() { Id = 4, SizeName = "2,00 GB - 5,00 GB" });
                this.FileSizes.Add(new FileSize() { Id = 5, SizeName = ">= 5,00 GB" });

                // Create default filetypes
                this.FileTypes.Add(new FileType() { Id = 1, TypeName = "Válassz file típust", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 2, TypeName = "avi", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 3, TypeName = "mp4", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 4, TypeName = "mkv", IsChecked = true });
                this.FileTypes.Add(new FileType() { Id = 5, TypeName = "M2TS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 6, TypeName = "MTS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 7, TypeName = "TS", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 8, TypeName = "wmv", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 9, TypeName = "mpg", IsChecked = false });
                this.FileTypes.Add(new FileType() { Id = 10, TypeName = "mpeg", IsChecked = true });

                // Create default filters
                this.Filters.Add(new Filter()
                {
                    Id = 1,
                    IsSeries = false,
                    IsFavorite = false,
                    IsMoreCD = false,
                    IsAllMovies = true,
                    DateFrom = new DateTime(2010, 1, 1),
                    DateTo = DateTime.Now,
                    FileSizeId = 1,
                    FileTypeId = 1,
                    FileSize = this.FileSizes.Local.FirstOrDefault(s => s.Id == 1),
                    FileType = this.FileTypes.Local.FirstOrDefault(t => t.Id == 1)
                }); ;

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
                    FileTypes = this.FileTypes.Local.Where(f => f.Id != 1).ToList(),
                    // Create default display setting
                    SelectedPalette = "Windows Phone",
                    SelectedTheme = "light",
                    SelectedAccentColor = new byte[] { 0xa2, 0x00, 0xff }
                });

                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
