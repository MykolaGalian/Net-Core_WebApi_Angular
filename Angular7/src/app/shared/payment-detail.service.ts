import { Injectable } from '@angular/core';
import { PaymentDetail } from './payment-detail.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {

  formData: PaymentDetail;   // переменная типа класс PaymentDetail (из папки shared), содержит (временно) все поля для приема json из webApi  
  readonly rootURL = 'http://localhost:61007/api';   // путь к АPI web core
  list : PaymentDetail[];                  // список обьектов PaymentDetail - для сохранения списка обьектов от запроса GET
  
  constructor(private http: HttpClient) { }

  postPaymentDetail() {     // метод передает запрос POST (добавление обьекта в бд) к АPI web core
    return this.http.post(this.rootURL + '/PaymentDetail', this.formData);   // this.formData - поле из текущего сервиса, т.е. не нужно передавать в методе обьект
  }

  putPaymentDetail() {                               // метод передает запрос PUT (обновление обьекта в бд) к АPI web core
    return this.http.put(this.rootURL + '/PaymentDetail/'+ this.formData.PMId, this.formData);
  }
  deletePaymentDetail(id) {                               // метод передает запрос DELETE (удаление обьекта из бд) к АPI web core
    return this.http.delete(this.rootURL + '/PaymentDetail/'+ id);
  }

  refreshList(){                                     // метод передает запрос GET (возврат лист обьектов из бд) к АPI web core
    this.http.get(this.rootURL + '/PaymentDetail')
    .toPromise()
    .then(res => this.list = res as PaymentDetail[]);
  }
}

