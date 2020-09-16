using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_Adform.CommonLibrary.Models.DBModels
{
    public class UserDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<TodoListDbModel> Lists { get; set; }
        public List<LabelDbModel> Labels { get; set; }
    }
}
