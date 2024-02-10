import { Component } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Record } from 'src/app/shared/domain/record';
import { RecordsService } from 'src/app/shared/services/records.service';

@Component({
  selector: 'app-create-record',
  templateUrl: './create-record.component.html',
  styleUrls: ['./create-record.component.css']
})
export class CreateRecordComponent {
  public title: FormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(128)
  ]);
  
  public form: FormGroup = new FormGroup({
    title: this.title,
  });

  constructor(
    private recordsService: RecordsService) { }

  public create(): boolean {
    if (this.form.invalid) 
    {
      this.form.markAllAsTouched();
      this.title.markAsDirty();

      return false;
    }

    const record: Record = {
      id: '00000000-0000-0000-0000-000000000000',
      title: this.title.value,
      entries: []
    }

    this.recordsService.add(record).subscribe();
    this.clear();

    return false;
  }
  public clear(): boolean {
    this.title.setValue('');
    this.title.markAsUntouched();

    return false;
  }
}
