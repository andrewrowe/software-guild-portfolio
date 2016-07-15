using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TravelBlogCapstone.Data;
using TravelBlogCapstone.Data.DapperRepositories;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Data.TestRepositories;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.BLL
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            //Hard-coded repositories for running tests w/o affecting database
            //Bind<IPostsRepository>().To<TestPostsRepository>();
            //Bind<ICategoriesRepository>().To<TestCategoriesRepository>();
            //Bind<ITagsRepository>().To<TestTagsRepository>();
            //Bind<IMessagesRepository>().To<TestMessagesRepository>();

            //Dapper repositories to access database
            Bind<IPostsRepository>().To<DapperPostsRepository>();
            Bind<ICategoriesRepository>().To<DapperCategoriesRepository>();
            Bind<ITagsRepository>().To<DapperTagsRepository>();
            Bind<IMessagesRepository>().To<DapperMessagesRepository>();
        }
    }
}
