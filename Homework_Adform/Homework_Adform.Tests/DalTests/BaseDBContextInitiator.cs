using Homework_Adform.CommonLibrary.Models.DBModels;
using Homework_Adform.DAL.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Homework_Adform.Tests.DalTests
{
    /// <summary>
    /// DB context initiator.
    /// </summary>
    public class BaseDBContextInitiator : MapperInitiator
    {
        public HomeworkDBContext DBContext { get; }
        public BaseDBContextInitiator()
        {
            var builder = new DbContextOptionsBuilder<HomeworkDBContext>()
            .UseInMemoryDatabase(databaseName: "HomeworkDB");

            var context = new HomeworkDBContext(builder.Options);
            var labels = Enumerable.Range(1, 10)
                .Select(i => new LabelDbModel { Id = i, Description = $"Sample{i}", CreatedBy = 1, CreatedDate = DateTime.Now });
            context.Labels.AddRange(labels);
            int changed = context.SaveChanges();
            DBContext = context;
        }
    }
}
