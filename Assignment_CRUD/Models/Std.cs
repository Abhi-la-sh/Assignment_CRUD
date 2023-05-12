using System.ComponentModel.DataAnnotations;
namespace Assignment_CRUD.Models
{
    public class Std
    {
        [Key]
        public int ID { get; set; } //ID of the student
        [Required]
        public string? FirstName { get;set; }//first name

        public string? LastName { get; set; }//last name

        public string? Email { get;set;}// email ID

    }
}
