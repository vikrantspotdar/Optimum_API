using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optimum_API.Models;
using System.Text;

namespace Optimum_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomersController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IEnumerable<CustomerModel>> Get()
        {
            String URL = _configuration.GetValue<string>("GET_CUSTOMERS_URL");

            IEnumerable<CustomerModel> customersList;
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(URL);
                customersList = JsonConvert.DeserializeObject<IEnumerable<CustomerModel>>(result);
            }
            return customersList;
        }
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<HttpResponseMessage> Create(CustomerModel payload)
        {
            String URL = _configuration.GetValue<string>("CREATE_CUSTOMER_URL");
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.PostAsync(URL, jsonContent);
                    httpResponse = new HttpResponseMessage(result.StatusCode);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                httpResponse.Content = new StringContent(ex.Message);
                return httpResponse;
            }
        }

        [Route("GetCustomer/{id}")]
        [HttpGet]
        public async Task<CustomerModel> GetCustomerById([FromRoute] string id)
        {
            String URL = _configuration.GetValue<string>("GET_CUSTOMER_BY_ID");

            CustomerModel customer;
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(URL + "/" + id);
                customer = JsonConvert.DeserializeObject<CustomerModel>(result);
            }
            return customer;
        }
        [HttpDelete]
        [Route("Delete/{customerId}")]
        public async Task<HttpResponseMessage> Delete([FromRoute] String customerId)
        {
            String URL = _configuration.GetValue<string>("DELETE_CUSTOMER");
            HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.DeleteAsync(URL + "/" + customerId);
                    httpResponse = new HttpResponseMessage(result.StatusCode);
                    return httpResponse;
                }
            }

            catch (Exception ex)
            {
                httpResponse.Content = new StringContent(ex.Message);
                return httpResponse;
            }
        }
        [HttpPut]
        [Route("Update/{customerId}")]
        public async Task<HttpResponseMessage> Update([FromRoute] String customerId, [FromBody] CustomerModel payload)
        {
            String URL = _configuration.GetValue<string>("UPDATE_CUSTOMER");
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.PostAsync(URL + "/" + customerId, jsonContent);
                    httpResponse = new HttpResponseMessage(result.StatusCode);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                httpResponse.Content = new StringContent(ex.Message);
                return httpResponse;
            }

        }
    }
}
