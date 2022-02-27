﻿using Ant0nRocket.Lib.Std20.IO;

using NUnit.Framework;

using System;
using System.IO;
using System.Reflection;

namespace Ant0nRocket.Lib.Std20.Tests
{
    public class FileSystemUtilsTests
    {
        [Test]
        public void T001_AppName()
        {
            var appName = FileSystemUtils.AppName;
            Assert.AreEqual(appName, "testhost");
        }

        [Test]
        public void T002_IsPortable()
        {
            var isPortable = FileSystemUtils.IsPortableMode;
            Assert.AreEqual(isPortable, true);
        }

        [Test]
        public void T003_DefaultAppDataFolderPath()
        {
            var defaultAppDataFolder = FileSystemUtils.DefaultSpecialFolder;
            Assert.AreEqual(Environment.SpecialFolder.LocalApplicationData, defaultAppDataFolder);
        }

        [Test]
        public void T004_AppDataLocalPath()
        {
            var appDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var calculatedAppDataLocalPath = Path.GetDirectoryName(FileSystemUtils.GetAppDataLocalFolderPath()); // ".."
            Assert.AreEqual(appDataLocalPath, calculatedAppDataLocalPath);
        }

        [Test]
        public void T005_AppDataRoamingPath()
        {
            var appDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var calculatedAppDataLocalPath = Path.GetDirectoryName(FileSystemUtils.GetAppDataRoamingFolderPath()); // ".."
            Assert.AreEqual(appDataLocalPath, calculatedAppDataLocalPath);
        }

        [Test]
        public void T005_GetAppDataPathForTestsArePortable()
        {
            const string FILENAME = "somefile.dat";
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            rootPath = Path.Combine(rootPath, FILENAME);
            var libResult = FileSystemUtils.GetDefaultAppDataFolderPathFor(fileName: FILENAME);

            Assert.AreEqual(rootPath, libResult);
        }

        [Test]
        public void T006_GetAppDataPathForTestsArePortableWithSubDirectory()
        {
            const string FILENAME = "somefile.dat";
            const string SUBDIRECTORY = "Data";

            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            rootPath = Path.Combine(rootPath, SUBDIRECTORY, FILENAME);
            var libResult = FileSystemUtils.GetDefaultAppDataFolderPathFor(fileName: FILENAME, subDirectory: SUBDIRECTORY);

            Assert.AreEqual(rootPath, libResult);
        }

        [Test]
        public void T007_GetAppDataPathForTestsAreNotPortable()
        {
            FileSystemUtils.IsPortableMode = false;

            const string FILENAME = "somefile.dat";
            var rootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var assemblyName = Assembly.GetEntryAssembly()?.GetName()?.Name ?? string.Empty;
            rootPath = Path.Combine(rootPath, assemblyName, FILENAME);

            var libResult = FileSystemUtils.GetDefaultAppDataFolderPathFor(fileName: FILENAME);

            Assert.AreEqual(rootPath, libResult);
        }
    }
}
