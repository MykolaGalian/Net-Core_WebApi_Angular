using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCod.Contracts;
using WebAppCod.Models;


namespace WebAppCod.Repositories
{
    public class PaymentDetailRepository : IPaymentDetailRepository
    {
        private PaymentDetailContext _context;  // ccылка на контекст БД

        public PaymentDetailRepository(PaymentDetailContext context) // DI
        {
            _context = context;
        }

        //GET
        public  IEnumerable<PaymentDetail> GetAll()
        {
            return _context.PaymentDetails;
        }

        //GET c аргументом
        public async Task<PaymentDetail> Find(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        //POST - добавить
        public async Task<PaymentDetail> Add(PaymentDetail paymentDetail)
        {
            await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();
            return paymentDetail;
        }

        //PUT - обновить
        public async Task<PaymentDetail> Update(PaymentDetail paymentDetail)
        {
            _context.Entry(paymentDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return paymentDetail;
        }


        // DELETE  - удалить
        public async Task<PaymentDetail> Remove(int id)
        {

            var paymentDetail = await _context.PaymentDetails.FindAsync(id); //возврат сущности обьекта

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
            return paymentDetail;
        }



        // проверка существования обьекта
        public async Task<bool> Exist(int id)
        {
            return await _context.PaymentDetails.AnyAsync(e => e.PMId == id);
        }

    } 
       
    
}
