import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from 'src/app/shared/payment-detail.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-payment-detail',
  templateUrl: './payment-detail.component.html',
  styles: []
})
export class PaymentDetailComponent implements OnInit {

  constructor(private service: PaymentDetailService,     // передаем в конструктор переменную типа сервис (содержит поле formData типа класс PaymentDetail- для приема json обьекта)
  private toastr: ToastrService)  {  }                    // передаем в конструктор переменную - для всплывающих сообщений о выполнении операций

  ngOnInit() {
   this.resetForm();
  }

  resetForm(form?: NgForm) {    // метод для очистки формы с записями PaymentDetail - вызывается из представления
   
    if (form != null) form.form.reset(); //очистка полей в форме если был передан обьект формы

    this.service.formData = {   // установка пустых значений для полей
      PMId: 0,
      CardOwnerName: '',
      CardNumber: '',
      ExpirationDate: '',
      CVV: ''
    }
  } 

  onSubmit(form: NgForm) {
      if (this.service.formData.PMId == 0) this.insertRecord(form); // добавление нового обьекта в бд если скрытое поле PMId =0 в представлении (ввод нового пользователя)
      else this.updateRecord(form);                     // обновление выбранного обьекта в бд в противном случае

   
  }

  insertRecord(form: NgForm){   // добавление нового обьекта в бд

    this.service.postPaymentDetail().subscribe(
      res => { 
        this.resetForm(form);                           // если все удачно отправлено, то очищаем поля временного класса
        this.toastr.success('Успешно сохранено', 'Регистрация нового владельца карты');     // и высвечиваем сообщение о удачном сохранении
        this.service.refreshList();   // обновление списка
      },
      
      err => {console.log(err)}    // если ошибки, выводим в консоль
   ) 
  }

  updateRecord(form: NgForm){   // обновление выбранного обьекта в бд

    this.service.putPaymentDetail().subscribe(
      res => { 
        this.resetForm(form);                           // если все удачно отправлено, то очищаем поля временного класса
        this.toastr.info('Изменение сохранено', 'Изменения данных владельца карты'); // и высвечиваем сообщение о удачном сохранении
        this.service.refreshList();   // обновление списка      
      },     
      
      err => {console.log(err)}    // если ошибки, выводим в консоль
   ) 
  }


}
