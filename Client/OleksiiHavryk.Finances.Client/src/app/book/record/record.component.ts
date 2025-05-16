import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Record } from 'src/app/shared/domain/record';
import $ from 'jquery';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Entry } from 'src/app/shared/domain/entry';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';
import { RecordsService } from 'src/app/shared/services/records.service';
import { Currency } from 'src/app/shared/domain/currency';
import { RecordHelperService } from 'src/app/shared/services/record-helper.service';

@Component({
    selector: 'app-record',
    templateUrl: './record.component.html',
    styleUrls: ['./record.component.css'],
    standalone: false
})
export class RecordComponent {
  @Input() public record: Record = {
    id: '00000000-0000-0000-0000-000000000000',
    title: '',
    entries: []
  }
  @Input() public opened: boolean = false;
  @Output() public removeEntry: EventEmitter<Entry> = new EventEmitter<Entry>();
  @Output() public removeRecord: EventEmitter<Record> = new EventEmitter<Record>();
  @Output() public open: EventEmitter<string> = new EventEmitter<string>();
  @Output() public close: EventEmitter<string> = new EventEmitter<string>();
  public get debit(): Entry[] {
    return this.record.entries.filter(r => r.type == 1);
  }
  public get credit(): Entry[] {
    return this.record.entries.filter(r => r.type == 2);
  }

  public resultCurrency: Currency = {
    id: 0,
    name: 'NONEXIST',
    value: 0,
    symbol: '|NONEXIST|'
  }

  public nameDebit: FormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(128)
  ]);
  public countDebit: FormControl = new FormControl('', [
    Validators.required,
    Validators.min(1),
    this.valueIsIntegerValidator
  ]);
  public costDebit: FormControl = new FormControl('', [
    Validators.required,
    Validators.min(0)
  ]);
  public currencyDebit: FormControl = new FormControl('', [
    Validators.required
  ]);
  public nameCredit: FormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(128)
  ]);
  public countCredit: FormControl = new FormControl('', [
    Validators.required,
    Validators.min(0),
    this.valueIsIntegerValidator
  ]);
  public costCredit: FormControl = new FormControl('', [
    Validators.required,
    Validators.min(0)
  ]);
  public currencyCredit: FormControl = new FormControl('', [
    Validators.required
  ]);

  public debitForm: FormGroup = new FormGroup({
    nameDebit: this.nameDebit,
    costDebit: this.costDebit,
    countDebit: this.countDebit,
    currencyDebit: this.currencyDebit
  });
  public creditForm: FormGroup = new FormGroup({
    nameCredit: this.nameCredit,
    costCredit: this.costCredit,
    countCredit: this.countCredit,
    currencyCredit: this.currencyCredit
  });

  constructor(
    public currenciesService: CurrenciesService,
    public recordsService: RecordsService,
    public recordHelper: RecordHelperService) {
      this.resultCurrency = currenciesService.currencies[0];
    }

  public toggle(panel: HTMLElement, chevron: HTMLElement): void {
    $(panel).slideToggle();
    let chevronEl = $(chevron);
    chevronEl.animate({
      'rotate': this.opened ? '0deg' : '180deg'
    }, () => {
      if (this.opened) {
        this.close.emit(this.record.id);
      } else {
        this.open.emit(this.record.id);
      }
    })
  }
  public addDebit(): boolean {
    if (this.debitForm.invalid) 
    {
      this.debitForm.markAllAsTouched();
      this.nameDebit.markAsDirty();
      this.costDebit.markAsDirty();
      this.currencyDebit.markAsDirty();

      return false;
    }

    const debitEntry: Entry = {
      id: '00000000-0000-0000-0000-000000000000',
      recordId: this.record.id,
      currencyId: this.currenciesService.currencies.filter(c => c.name === this.currencyDebit.value)[0].id,
      name: this.nameDebit.value,
      count: this.countDebit.value,
      itemPrice: this.costDebit.value,
      type: 1
    };

    this.recordsService.addEntry(debitEntry).subscribe();

    this.clearDebit();

    return false;
  }
  public clearDebit(): boolean {
    this.nameDebit.setValue('');
    this.nameDebit.markAsUntouched();

    this.costDebit.setValue('');
    this.costDebit.markAsUntouched();

    this.countDebit.setValue('');
    this.countDebit.markAsUntouched();

    this.currencyDebit.setValue('');
    this.currencyDebit.markAsUntouched();

    return false;
  }
  public addCredit(): boolean {
    if (this.creditForm.invalid) 
    {
      this.creditForm.markAllAsTouched();
      this.nameCredit.markAsDirty();
      this.costCredit.markAsDirty();
      this.currencyCredit.markAsDirty();

      return false;
    }

    const creditEntry: Entry = {
      id: '00000000-0000-0000-0000-000000000000',
      recordId: this.record.id,
      currencyId: this.currenciesService.currencies.filter(c => c.name === this.currencyCredit.value)[0].id,
      name: this.nameCredit.value,
      count: this.countCredit.value,
      itemPrice: this.costCredit.value,
      type: 2
    };
    
    this.recordsService.addEntry(creditEntry).subscribe();
    
    this.clearCredit();

    return false;
  }
  public clearCredit(): boolean {
    this.nameCredit.setValue('');
    this.nameCredit.markAsUntouched();

    this.costCredit.setValue('');
    this.costCredit.markAsUntouched();

    this.countCredit.setValue('');
    this.countCredit.markAsUntouched();


    this.currencyCredit.setValue('');
    this.currencyCredit.markAsUntouched();

    return false;
  }
  public deleteCredit(credit: Entry) {
    this.removeEntry.emit(credit);
  }
  public deleteDebit(debit: Entry) {
    this.removeEntry.emit(debit);
  }
  public deleteRecord($event: MouseEvent) {
    $event.cancelBubble = true;
    this.removeRecord.emit(this.record);
  }
  public changeCurrency(resultCurrencyElement: HTMLSelectElement) {
    this.resultCurrency = this.currenciesService.currencyById(Number.parseFloat(resultCurrencyElement.value));
  }
  public calculateEntryTotalPrice(entry: Entry) {
    return entry.itemPrice * entry.count;
  }
  public calculateResult(): number {
    const debit = this.recordHelper.getAvailableDebit(this.record);
    const credit = this.recordHelper.getAvailableCredit(this.record);
    const resCurr = this.resultCurrency;
  
    const debitMap = debit.map(v => {
      var price = this.calculateEntryTotalPrice(v);

      if (resCurr.id == v.currencyId) {
        return price * 1;
      } else {
        var currency = this.currenciesService.currencyById(v.currencyId);
        return price * (currency.value / resCurr.value)
      }
    });
    const debitTotal = debitMap.length > 0 ? debitMap.reduce((s, c) => s + c) : 0;
    
    const creditMap = credit.map(v => {
      var price = this.calculateEntryTotalPrice(v);

      if (resCurr.id == v.currencyId) {
        return price * 1;
      } else {
        var currency = this.currenciesService.currencyById(v.currencyId);
        return price * (currency.value / resCurr.value)
      }
    });
    const creditTotal = creditMap.length > 0 ? creditMap.reduce((s, c) => s + c) : 0;

    var res = debitTotal - creditTotal;
    return Number.parseFloat(res.toFixed(2));
  }
  public toggleEntryVisibility(entry: Entry): boolean {
    this.recordHelper.toggleEntryVisibility(entry);
    return false;
  }
  public isHidden(entry: Entry) {
    return this.recordHelper.isHidden(entry);
  }
  private valueIsIntegerValidator(control: AbstractControl): ValidationErrors | null {
    const val = Number.parseFloat(control.value as string);
    const roundedVal = Math.round(val);
    const isForbidden = roundedVal !== val;

    return isForbidden ? { valueIsInteger: { actualValue: control.value as string } } : null;
  }
}
