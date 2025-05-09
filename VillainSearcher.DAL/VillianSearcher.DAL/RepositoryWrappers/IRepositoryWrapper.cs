using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Repositories;

namespace VillianSearcher.DAL.RepositoryWrappers
{
    public interface IRepositoryWrapper
    {
        IVillainRepository VoterRepository { get; }

        void LoadData();

        void SaveData();
    }
}
