using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.ViewModels;

namespace SportStore.Controlles
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;
        public ProductController (IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1)
            => View(new ProductsListViewModel
            {
                Products = _repository.Products.OrderBy(x => x.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            });
    }
}
