using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Helpers;
using VillianSearcher.DAL.Models;

namespace VillianSearcher.DAL.Data
{
    public class JsonDataProvider : DataProviderBase
    {       
        #region Properties        

        public override bool IsDataLoaded { get => Villains != null && Villains.Count > 0; }

        public List<Villain> Villains { get; set; }

        #endregion

        #region Ctor
        public JsonDataProvider()
        {
            Villains = new List<Villain>();

            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                var str = IOHelper.ReadFromFile(FilePath);

                if (string.IsNullOrEmpty(str))
                    return;

                Villains = JsonHelper.Deserialize(str, Villains);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Fail to Load Data! Error: " + ex.Message);
#endif
            }
        }

        public override void SaveData()
        {
            try
            {
                var str = JsonHelper.Serialize(Villains);

                IOHelper.WriteToFile(str, FilePath);
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Fail to save data! Error: " + e.Message);
#endif
            }
        }
        #endregion
    }
}
