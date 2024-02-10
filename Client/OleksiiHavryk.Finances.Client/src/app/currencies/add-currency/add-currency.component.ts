import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Currency } from 'src/app/shared/domain/currency';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
  selector: 'app-add-currency',
  templateUrl: './add-currency.component.html',
  styleUrls: ['./add-currency.component.css']
})
export class AddCurrencyComponent {
  public name: FormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(128)
  ]);
  public value: FormControl = new FormControl('', [
    Validators.required,
    Validators.min(0.0000001)
  ]);
  public symbol: FormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(1)
  ]);
  
  public form: FormGroup = new FormGroup({
    name: this.name,
    value: this.value,
    symbol: this.symbol
  });

  constructor(private currenciesService: CurrenciesService) { }

  public create(): boolean {
    if (this.form.invalid) 
    {
      this.form.markAllAsTouched();
      this.name.markAsDirty();
      this.value.markAsDirty();
      this.symbol.markAsDirty();

      return false;
    }

    const currency: Currency = {
      id: '00000000-0000-0000-0000-000000000000',
      name: this.name.value,
      value: this.value.value,
      symbol: this.symbol.value
    }

    this.currenciesService.add(currency).subscribe();
    this.clear();

    return false;
  }
  public clear(): boolean {
    this.name.setValue('');
    this.name.markAsUntouched();

    this.value.setValue('');
    this.value.markAsUntouched();

    this.symbol.setValue('');
    this.symbol.markAsUntouched();

    return false;
  }
}
