using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows;

namespace DesktopServer.Service
{
    public static class StartupService
    {
        // Current application
        private static Assembly currentAssembly = Assembly.GetExecutingAssembly();

        // Set this application to windows startup
        public static void SetAppToStartUp()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue(currentAssembly.GetName().Name, currentAssembly.Location);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage("Nem lehet a programot az indítópulthoz adni:" + Environment.NewLine + ex.Message, "Indítópult hiba", MessageBoxButton.OK);
            }
        }

        // Remove this application from windows startup
        public static void RemoveAppFromStartUp()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue(currentAssembly.GetName().Name, false);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage("Nem lehet a programot az indítópultból törölni:" + Environment.NewLine + ex.Message, "Indítópult hiba", MessageBoxButton.OK);
            }
        }
    }
}
