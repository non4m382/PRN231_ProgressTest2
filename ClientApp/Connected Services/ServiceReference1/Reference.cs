﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Category", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
    public partial class Category : object
    {
        
        private int CategoryIDField;
        
        private string CategoryNameField;
        
        private ServiceReference1.Product[] ProductsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CategoryID
        {
            get
            {
                return this.CategoryIDField;
            }
            set
            {
                this.CategoryIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CategoryName
        {
            get
            {
                return this.CategoryNameField;
            }
            set
            {
                this.CategoryNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.Product[] Products
        {
            get
            {
                return this.ProductsField;
            }
            set
            {
                this.ProductsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Product", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
    public partial class Product : object
    {
        
        private ServiceReference1.Category CategoryField;
        
        private System.Nullable<int> CategoryIDField;
        
        private string ImageField;
        
        private int ProductIDField;
        
        private string ProductNameField;
        
        private System.Nullable<decimal> UnitPriceField;
        
        private System.Nullable<int> UnitsInStockField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.Category Category
        {
            get
            {
                return this.CategoryField;
            }
            set
            {
                this.CategoryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> CategoryID
        {
            get
            {
                return this.CategoryIDField;
            }
            set
            {
                this.CategoryIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image
        {
            get
            {
                return this.ImageField;
            }
            set
            {
                this.ImageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProductID
        {
            get
            {
                return this.ProductIDField;
            }
            set
            {
                this.ProductIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductName
        {
            get
            {
                return this.ProductNameField;
            }
            set
            {
                this.ProductNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> UnitPrice
        {
            get
            {
                return this.UnitPriceField;
            }
            set
            {
                this.UnitPriceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> UnitsInStock
        {
            get
            {
                return this.UnitsInStockField;
            }
            set
            {
                this.UnitsInStockField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ICategoryService")]
    public interface ICategoryService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/DoWork", ReplyAction="http://tempuri.org/ICategoryService/DoWorkResponse")]
        System.Threading.Tasks.Task<string> DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/GetCategories", ReplyAction="http://tempuri.org/ICategoryService/GetCategoriesResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Category[]> GetCategoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/GetCategoryById", ReplyAction="http://tempuri.org/ICategoryService/GetCategoryByIdResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Category> GetCategoryByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/Create", ReplyAction="http://tempuri.org/ICategoryService/CreateResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Category> CreateAsync(ServiceReference1.Category category);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/Update", ReplyAction="http://tempuri.org/ICategoryService/UpdateResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Category> UpdateAsync(ServiceReference1.Category category);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICategoryService/Delete", ReplyAction="http://tempuri.org/ICategoryService/DeleteResponse")]
        System.Threading.Tasks.Task<string> DeleteAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface ICategoryServiceChannel : ServiceReference1.ICategoryService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class CategoryServiceClient : System.ServiceModel.ClientBase<ServiceReference1.ICategoryService>, ServiceReference1.ICategoryService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CategoryServiceClient() : 
                base(CategoryServiceClient.GetDefaultBinding(), CategoryServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ICategoryService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CategoryServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(CategoryServiceClient.GetBindingForEndpoint(endpointConfiguration), CategoryServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CategoryServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CategoryServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CategoryServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CategoryServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CategoryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> DoWorkAsync()
        {
            return base.Channel.DoWorkAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Category[]> GetCategoriesAsync()
        {
            return base.Channel.GetCategoriesAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Category> GetCategoryByIdAsync(int id)
        {
            return base.Channel.GetCategoryByIdAsync(id);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Category> CreateAsync(ServiceReference1.Category category)
        {
            return base.Channel.CreateAsync(category);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Category> UpdateAsync(ServiceReference1.Category category)
        {
            return base.Channel.UpdateAsync(category);
        }
        
        public System.Threading.Tasks.Task<string> DeleteAsync(int id)
        {
            return base.Channel.DeleteAsync(id);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICategoryService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICategoryService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:54894/Service/CategoryService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return CategoryServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ICategoryService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return CategoryServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ICategoryService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ICategoryService,
        }
    }
}
