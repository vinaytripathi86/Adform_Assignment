using Homework_Adform.CommonLibrary.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Homework_Adform.DAL.DBContexts
{
    /// <summary>
    /// Db context.
    /// </summary>
    public class HomeworkDBContext : DbContext
    {
        /// <summary>
        /// Create new instance of <see cref="HomeworkDBContext"/> class.
        /// </summary>
        /// <param name="options">Db context options.</param>
        public HomeworkDBContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<TodoListDbModel> TodoLists { get; set; }
        public DbSet<TodoItemDBModel> TodoItems { get; set; }
        public DbSet<LabelDbModel> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbModel>().HasData(new UserDbModel
            {
                FirstName = "Vinay",
                LastName = "Tripathi",
                Password = "123",
                Username = "vinay",
                Id = 1
            }, new UserDbModel
            {
                FirstName = "Ashish",
                LastName = "Tripathi",
                Password = "123",
                Username = "ashish",
                Id = 2
            });

            modelBuilder.Entity<LabelDbModel>().HasData(new LabelDbModel
            {
                Id = 1,
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow,
                Description = "Label 1"
            });

            modelBuilder.Entity<TodoListDbModel>().HasData(new TodoListDbModel
            {
                Description = "List 1",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                LabelId = 1,
                Id=1
            },
            new TodoListDbModel
            {
                Description = "List 2",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1,
                Id=2
            },
            new TodoListDbModel
            {
                Description = "List 3",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 2,
                Id=3
            });
            modelBuilder.Entity<TodoItemDBModel>().HasData(new TodoItemDBModel
            {
                Id = 1,
                LabelId = 1,
                ListId = 1,
                Notes = "Item 1",
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            });
        }
    }
}
