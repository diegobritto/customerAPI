using CustomerAPI.Domain;
using CustomerAPI.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moq;
using CustomerAPI.Data.Context;
using CustomerAPI.Service.Service;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Data.Repositories;
using System.Threading.Tasks;

namespace CustomerAPITest.Domain
{

    public class Tests
    {

        [SetUp]
        public void Setup()
        {

        }

        #region Util
        [Test]
        public void IsValidEmail()
        {
            bool fromCall = Util.IsValidEmail("a@a.a");
            Assert.IsTrue(fromCall);
            fromCall = Util.IsValidEmail("diego@gmail.com");
            Assert.IsTrue(fromCall);
        }

        [Test]
        public void IsInvalidEmail()
        {
            bool fromCall = Util.IsValidEmail("teste");
            Assert.IsFalse(fromCall);
            fromCall = Util.IsValidEmail("teste");
            Assert.IsFalse(fromCall);
        }
        [Test]
        public void IsValidMessage()
        {
            Assert.AreEqual(Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS), "Cliente salvo com sucesso!");
            Assert.AreEqual(Message.Text(Message.REGISTERED_CUSTOMER_ERROR), "Já existe um cliente com o mesmo email cadastrado!");
            Assert.AreEqual(Message.Text(Message.INVALID_EMAIL), "Email inválido!");
            Assert.AreEqual(Message.Text(Message.INVALID_NAME), "Nome inválido");
            Assert.AreEqual(Message.Text(Message.CUSTOMER_NOT_FOUND), "Cliente não encontrado!");
            Assert.AreEqual(Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS), "Cliente salvo com sucesso!");
            Assert.AreEqual(Message.Text(Message.REGISTERED_CUSTOMER_ERROR), "Já existe um cliente com o mesmo email cadastrado!");
            Assert.AreEqual(Message.Text(Message.REGISTERED_CUSTOMER_CONCURRENCY_ERROR), "Não foi possível salvar cliente, pois o mesmo já foi atualizado.");
            Assert.AreEqual(Message.Text(Message.DELETED_CUSTOMER_SUCCESS), "Cliente deletado com sucesso!");
            Assert.AreEqual(Message.Text(Message.DELETED_CUSTOMER_ERROR), "Não foi possível deletar cliente!");
            Assert.AreEqual(Message.Text(999), null);


        }
        #endregion

        #region Customer
        [Test]
        public void IsInvalidCustomer()
        {
            Customer customerEmailNull = new Customer { Email = null, Name = "teste", Id = 1 };
            Customer customerEmailInvalid = new Customer { Email = "teste", Name = "teste", Id = 1 };
            Customer customerNameNull = new Customer { Email = "a@a.a", Name = null, Id = 1 };
            Customer customerNameInvalid = new Customer { Email = "a@a.a", Name = "te", Id = 1 };
            Assert.IsTrue(TestUtil.ValidateModel(customerEmailNull).Any(v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Este campo é obrigatório")));
            Assert.IsTrue(TestUtil.ValidateModel(customerEmailInvalid).Any(v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Email inválido!")));
            Assert.IsTrue(TestUtil.ValidateModel(customerNameNull).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Este campo é obrigatório")));
            Assert.IsTrue(TestUtil.ValidateModel(customerNameInvalid).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Este campo deve conter entre  3 e 50 caracteres")));



        }
        #endregion              
    }
    public static class TestUtil
    {
        public static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();

            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}