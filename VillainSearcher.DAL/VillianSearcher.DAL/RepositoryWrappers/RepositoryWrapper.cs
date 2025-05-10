using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Data;
using VillianSearcher.DAL.Repositories;

namespace VillianSearcher.DAL.RepositoryWrappers
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private JsonDataProvider m_dataProvider;

        private IVillainRepository m_VoterRepository;

        public IVillainRepository VillainRepository
        {
            get
            {
                if (m_VoterRepository == null)
                    m_VoterRepository = new VillainRepository(m_dataProvider);

                return m_VoterRepository;
            }
        }

        public RepositoryWrapper(IDataProvider dataContext)
        {
            m_dataProvider = dataContext as JsonDataProvider;
        }

        public void LoadData()
        {
            m_dataProvider.LoadData();
        }

        public void SaveData()
        {
            m_dataProvider.SaveData();
        }
    }
}
