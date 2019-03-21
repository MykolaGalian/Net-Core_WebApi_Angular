import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from 'src/app/shared/payment-detail.service';
import { PaymentDetail } from 'src/app/shared/payment-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-detail-list',
  templateUrl: './payment-detail-list.component.html',
  styles: []
})
export class PaymentDetailListComponent implements OnInit {

  constructor(private service: PaymentDetailService, 
    private toastr: ToastrService) { }

  ngOnInit() {
    this.service.refreshList();  // при обращении к компоненту - вызов метода - запрос GET - получение списка обьектов из бд 
  }

  populateForm(pd: PaymentDetail) {       // метод обновляет данные во временном обьекте (service.formData) типа PaymentDetail на основании обьекта выделенного из списка pd в представлении
    this.service.formData = Object.assign({}, pd);   // Object.assign - предотвращает корректировку полей в списке обьектов PaymentDetailService list
  }

  onDelete(PMId) {   // удаление выбранного в списке обьекта из бд

    if (confirm('Вы хотите удалить данные владельца карты?')) {

      this.service.deletePaymentDetail(PMId)
        .subscribe(
          
          res => {          
          this.service.refreshList();
          this.toastr.warning('Удалено успешно', 'Данные владельца карты');
        },

          err => {            
            console.log(err);
          })
    }
  }

}
