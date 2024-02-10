import { Component } from '@angular/core';
import { Currency } from 'src/app/shared/domain/currency';
import { ModalService } from 'src/app/shared/modal/modal.service';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
  selector: 'app-currency-list',
  templateUrl: './currency-list.component.html',
  styleUrls: ['./currency-list.component.css']
})
export class CurrencyListComponent {
  public modalId: string = 'DeleteCurrencyModalId';
  public deletingCurrency: Currency = {
    id: '00000000-0000-0000-0000-000000000000',
    name: '',
    value: 0,
    symbol: ''
  };

  constructor(
    public currenciesService: CurrenciesService,
    public modal: ModalService) { 
      this.currenciesService.updateSet(false).subscribe();
    }

  public removeAsQuestion(currency: Currency) {
    this.modal.toggleModal(this.modalId);
    this.deletingCurrency = currency;
  }
  public createRemoveAction(): () => void {
    return () => {
      this.currenciesService.remove(this.deletingCurrency.id).subscribe();
    }
  }
}
