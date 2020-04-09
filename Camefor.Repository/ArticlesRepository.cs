using Camefor.IRepository.BASE;
using Camefor.Model;
using Camefor.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Camefor.Repository {
    public class ArticlesRepository:BaseRepository<Articles>, IRepository.BASE.IBaseRepository<Articles> {
    }
}
