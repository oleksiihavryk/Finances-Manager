import { Component, NgModule } from '@angular/core';
import { Currency } from 'src/app/shared/domain/currency';
import { ModalService } from 'src/app/shared/modal/modal.service';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
    selector: 'app-currency-list',
    templateUrl: './currency-list.component.html',
    styleUrls: ['./currency-list.component.css'],
    standalone: false
})
export class CurrencyListComponent {
  public modalId: string = 'DeleteCurrencyModalId';
  public deletingCurrency: Currency = {
    id: 0,
    name: '',
    value: 0,
    symbol: ''
  };

  constructor(
    public currenciesService: CurrenciesService,
    public modal: ModalService) { 
      this.currenciesService.updateSet(false).subscribe();
    }
}
