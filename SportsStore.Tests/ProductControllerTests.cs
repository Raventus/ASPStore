using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controlles;
using SportStore.Models;
using SportStore.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            });

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert

            Product[] prodArray = result.Products.ToArray();
            
            Assert.True(prodArray.Length == 2);
            
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            });

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            PagingInfo pageInfo = result.PagingInfo;

            // Assert
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);

        }
    }
}
