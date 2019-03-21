using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCod.Models;

namespace WebAppCod.Contracts
{
    public interface IPaymentDetailRepository
    {

        IEnumerable<PaymentDetail> GetAll();    //GetPaymentDetails

        Task <PaymentDetail> Find(int id);    //GetPaymentDetail(int id)

        Task <PaymentDetail> Add(PaymentDetail paymentDetail);  //PostPaymentDetail(PaymentDetail paymentDetail)

        Task <PaymentDetail> Update(PaymentDetail paymentDetail);          //PutPaymentDetail(PaymentDetail paymentDetail)

        Task <PaymentDetail> Remove(int id);    //eletePaymentDetail(int id)

        Task<bool> Exist(int id);   //PaymentDetailExists(int id)

    }
}
