using System;
using Xunit;
using SistemaVendas.Models;
using System.Collections.Generic;

namespace ProjetoTestes
{
    public class UnitTestModels
    {
        [Fact]
        public void TestLoginOk()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "pedro@gmail.com";
            objLogin.Senha = "123456";
            bool result = objLogin.ValidarLogin();
            Assert.True(result);
        }

        [Fact]
        public void TestLoginFalse()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "pedro@gmail.com";
            objLogin.Senha = "teste";
            bool result = objLogin.ValidarLogin();
            Assert.True(result);
        }

        [Fact]
        public void CheckTypeListProdutos()
        {
            ProdutoModel produtoModel = new ProdutoModel();
            var lista = produtoModel.ListarTodosProdutos();
            Assert.IsType<List<ProdutoModel>>(lista);
        }
    }
}
