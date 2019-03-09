using SaminProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaminProject.Library
{
    public class UnitOfWork: IDisposable
    {
        private DBContext context = new DBContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<BaseInformation> baseInformationRepository;
        private GenericRepository<News> newsRepository;
        private GenericRepository<Service> serviceRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<ProductImage> productImageRepository;
        private GenericRepository<Project> projectRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<BaseInformation> BaseInformationRepository
        {
            get
            {

                if (this.baseInformationRepository == null)
                {
                    this.baseInformationRepository = new GenericRepository<BaseInformation>(context);
                }
                return baseInformationRepository;
            }
        }
        public GenericRepository<News> NewsRepository
        {
            get
            {

                if (this.newsRepository == null)
                {
                    this.newsRepository = new GenericRepository<News>(context);
                }
                return newsRepository;
            }
        }
        public GenericRepository<Service> ServiceRepository
        {
            get
            {

                if (this.serviceRepository == null)
                {
                    this.serviceRepository = new GenericRepository<Service>(context);
                }
                return serviceRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<ProductImage> ProductImageRepository
        {
            get
            {

                if (this.productImageRepository == null)
                {
                    this.productImageRepository = new GenericRepository<ProductImage>(context);
                }
                return productImageRepository;
            }
        }

        public GenericRepository<Project> ProjectRepository
        {
            get
            {

                if (this.projectRepository == null)
                {
                    this.projectRepository = new GenericRepository<Project>(context);
                }
                return projectRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}