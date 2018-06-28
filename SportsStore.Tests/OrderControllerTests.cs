using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Ceckout_Empty_Cart()
        {
            //Assign - create imitation of a repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Assign - create empty cart
            Cart cart = new Cart();
            //Assign - create order
            Order order = new Order();
            //Assign - create controller
            OrderController target = new OrderController(mock.Object, cart);

            //Act
            ViewResult result = target.CheckOut(order) as ViewResult;

            //Assert - check if an order was placed in repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Assert - check if the method returns default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Assert - check if correct model is hand to view
            Assert.False(result.ViewData.ModelState.IsValid);  
        }

        [Fact]
        public void Cannot_Check_Invalid_ShippingDetails()
        {
            //Assign - create imitation of the repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Assign - create cart with product
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Assign - create controller
            OrderController target = new OrderController(mock.Object, cart);
            //Assign - add error to a model
            target.ModelState.AddModelError("error", "error");

            //Act - attemp to finish an order
            ViewResult result = target.CheckOut(new Order()) as ViewResult;

            //Assert - check if order wasn't hand to the repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Assert - check if method returns default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Assert - check if wrong model is handed to a view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Assign - create imitation of the repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Assign - create cart with a product
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Assign - create controller
            OrderController target = new OrderController(mock.Object, cart);

            //Act - attemp to finish an order
            RedirectToActionResult result = target.CheckOut(new Order()) as RedirectToActionResult;

            //Assert - check if an order wasn't handed to the repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once); //???? Once?
            //Assert - check if a method redirects to an action method Colpeted()
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
