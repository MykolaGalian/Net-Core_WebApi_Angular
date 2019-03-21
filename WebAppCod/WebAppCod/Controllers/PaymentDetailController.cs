using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCod.Contracts;
using WebAppCod.Models;

namespace WebAppCod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailRepository _paymentDetailRepository; // вместо ccылки на контекст БД, ссылка на интерфейс репозитория


        //private readonly PaymentDetailContext _context;

        public PaymentDetailController(IPaymentDetailRepository paymentDetailRepository)  //DI
        {
            _paymentDetailRepository = paymentDetailRepository;

        }

        // GET: api/PaymentDetail
        [HttpGet]
        public  ActionResult<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return  _paymentDetailRepository.GetAll().ToList();  //
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _paymentDetailRepository.Find(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }

        //PUT: api/PaymentDetail/5   - обновить
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)  //paymentDetail в запроса body  PUT (обновить)
        {
            if (id != paymentDetail.PMId)
            {
                return BadRequest();
            }            

            try
            {
                await _paymentDetailRepository.Update(paymentDetail);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaymentDetail   - добавить
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //возврат ошибки в случае неправильного заполнения полей в обьекте customer
            }

          

            await _paymentDetailRepository.Add(paymentDetail);

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await PaymentDetailExists(id)) 
            {
                return NotFound();
            }

            var paymentDetail_= await _paymentDetailRepository.Remove(id);

            return paymentDetail_;
        }

        public async Task<bool> PaymentDetailExists(int id)
        {
            return await _paymentDetailRepository.Exist(id);
        }
    }
}
