using System.ComponentModel.DataAnnotations;
namespace CustomerAPI.Domain
{
    public class Customer
    {
        public Customer()
        {}
        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [MaxLength(50,ErrorMessage = "Este campo deve conter entre  3 e 50 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre  3 e 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre  3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre  3 e 100 caracteres")]                
        public string Email { get; set; }

 
    }
}
