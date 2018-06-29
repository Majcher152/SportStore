using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            //Assign - create repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            //Assign - create controller
            AdminController target = new AdminController(mock.Object);

            //Act
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            //Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void Can_Edit_Product()
        {
            //Assign - create imitation of the repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
            new Product { ProductID = 1, Name = "P1"},
            new Product { ProductID = 2, Name = "P2"},
            new Product { ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            //Assign - create con troller
            AdminController target = new AdminController(mock.Object);

            //Act 
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //Assert 
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            //Assign - create imitation of the repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            //Assign - create controller
            AdminController target = new AdminController(mock.Object);

            //Act 
            Product result = GetViewModel<Product>(target.Edit(4));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //Assign - create imitation of the repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Assign - create imitation of a container TempData
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            //Assign - create controller
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            //Assign - create product
            Product product = new Product { Name = "Test" };

            //Act - attemp to save changes in product
            IActionResult result = target.Edit(product);

            //Assert - check if repository has been called
            mock.Verify(m => m.SaveProduct(product));
            //Assert - check type from return method
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            //Assign - create imitation of the repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Assign - create controller
            AdminController target = new AdminController(mock.Object);
            //Assign - create product
            Product product = new Product { Name = "Test" };
            //Assign - add error to a model's state
            target.ModelState.AddModelError("error", "error");

            //Act - attemp to save changes in prodcut
            IActionResult result = target.Edit(product);

            //Assert - check if respository hasn't been called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);
            //Assert - check type from return method
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            //Assign - create product
            Product prod = new Product { ProductID = 2, Name = "Test" };
            //Assign - create imitation of the repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1" },
                prod,
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());
            //Assign - create controller
            AdminController target = new AdminController(mock.Object);

            //Act - delete product
            target.Delete(prod.ProductID);

            //Assert - check if method has been called with correct product
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
