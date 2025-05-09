using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Helpers;

namespace VillianSearcher.DAL.Data
{
    public abstract class DataProviderBase : IDataProvider
    {
        #region Fields

        private string m_filePath;

        #endregion

        #region Properties

        public string FilePath { get=>m_filePath; }

        public abstract bool IsDataLoaded { get; }

        #endregion

        #region Ctor
        protected DataProviderBase()
        {
            string DataBaseFolder = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar + "Database";

            IOHelper.CreateDirectoryIfNotExists(DataBaseFolder);

            m_filePath = DataBaseFolder + Path.DirectorySeparatorChar + "Voters.json";

            IOHelper.CreateFileIfNotExists(m_filePath);
        }
        #endregion

        #region Methods

        public abstract void LoadData();

        public abstract void SaveData();

        #endregion
    }
}
